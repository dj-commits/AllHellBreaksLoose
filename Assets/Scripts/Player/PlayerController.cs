using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement vars
    [SerializeField] float moveSpeed;
    private float moveSpeedMultiplier;
    [SerializeField] float dashSpeed;
    [SerializeField] float playerHealth;

    // Timer vars
    [SerializeField] float dashTime;
    [SerializeField] float dashRechargeTime;

    // Booleans
    [SerializeField] bool canDash;
    [SerializeField] public bool isAlive;
    [SerializeField] public bool isShielded;

    // GameObjects
    [SerializeField]
    public GameObject powerUp;
    GameObject shield;

    // Components
    private Rigidbody2D rb;
    private Animator playerAnimator;
    private BoxCollider2D collider;

    // Vectors
    Vector2 playerPosition;
    Vector3 mousePosition;

    Gun gun;

    float DEFAULT_MOVE_SPEED = 20f;
    float DEFAULT_DASH_SPEED = 3f;
    float DEFAULT_DASH_RECHARGE_TIME = 0f;
    float DEFAULT_DASH_TIME = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        gun = GameObject.Find("gun").GetComponent<Gun>();
        playerAnimator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        GameObject shield = Resources.Load("Prefabs/Powerups/bubble_shield", typeof(GameObject)) as GameObject;
        isShielded = false;
        canDash = true;
        moveSpeedMultiplier = 1f;
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        // check for playerDeath
        if (playerHealth <= 0)
        {
            isAlive = false;
            //Destroy(gameObject);
        }

        // Movement
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            moveSpeedMultiplier = dashSpeed;
            StartCoroutine(setDashTimer());
            StartCoroutine(setDashRechargeTimer());
            canDash = false;
        }

        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(inputX * moveSpeed * moveSpeedMultiplier, inputY * moveSpeed * moveSpeedMultiplier);

        movement *= Time.deltaTime;
        if (movement != Vector2.zero)
        {
            playerAnimator.SetBool("isMoving", true);
            transform.Translate(movement, Space.World);
        } else
        {
            playerAnimator.SetBool("isMoving", false);
        }
        

        // Shooting
        if (Input.GetButton("Shoot"))
        {
            gun.Shoot();
        }

        // Use powerup
        if (Input.GetButton("Interact"))
        {
            if (this.powerUp != null)
            {
                powerUp.GetComponent<Powerup>().ActivatePower();
            }
        }

    }

    private void FixedUpdate()
    {
        


    }

    public GameObject CreateShield()
    {
        Vector2 shield_spawn = new Vector2(this.transform.position.x, this.transform.position.y - 1);
        GameObject playerShield = Instantiate(shield, shield_spawn, Quaternion.identity);
        return playerShield;
    }

    public float GetPlayerHealth()
    {
        return playerHealth;
    }

    //Adjusts movement speed back to normal after timer has counted down
    //The time a dash movement lasts for
    IEnumerator setDashTimer()
    {
        yield return new WaitForSeconds(dashTime);
        moveSpeedMultiplier = 1;
    }

    //Re-enables dashing after recharge timer has counted down
    //The time before you can dash again
    IEnumerator setDashRechargeTimer()
    {
        yield return new WaitForSeconds(dashRechargeTime);
        canDash = true;
    }

    public void TakeDamage(float damage)
    {
        playerHealth -= damage;
    }

    public void setPowerup(GameObject powerUp)
    {
        this.powerUp = powerUp;
    }

    public void setCanDash(bool canDash)
    {
        this.canDash = canDash;
    }

    public void setDashRechargeTime(float dashRechargeTime)
    {
        this.dashRechargeTime = dashRechargeTime;
        
    }
    public void resetDashRechargeTime()
    {
        this.dashRechargeTime = DEFAULT_DASH_RECHARGE_TIME;
    }

    public void setDashSpeed(float dashSpeed)
    { 
        this.dashSpeed = dashSpeed;
    }

    public void setMoveSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }

    public void setDashTime(float dashTime)
    {
        this.dashTime = dashTime;
    }

    public Gun getGun()
    {
        return gun;
    }

}
