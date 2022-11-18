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
    [SerializeField] float canDashTime;

    // Booleans
    [SerializeField] bool canDash;
    [SerializeField] public bool isAlive;

    // GameObjects
    [SerializeField]
    public GameObject powerUp;

    // Components
    private Rigidbody2D rb;
    private Animator playerAnimator;

    // Vectors
    Vector2 playerPosition;
    Vector3 mousePosition;

    Gun gun;

    const float DEFAULT_MOVE_SPEED = 1f;
    const float DEFAULT_DASH_SPEED = 3f;
    const float DEFAULT_CAN_DASH_TIME = 2f;
    const float DEFAULT_DASH_TIME = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        gun = GameObject.Find("gun").GetComponent<Gun>();
        playerAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        canDash = false;
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
            Destroy(gameObject);
        }

        // Movement
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            moveSpeedMultiplier = dashSpeed;
            canDash = false;
            StartCoroutine(setDashTimer());
            StartCoroutine(setCanDashTimer());
        }

        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(inputX * moveSpeed * moveSpeedMultiplier, inputY * moveSpeed * moveSpeedMultiplier);
        movement *= Time.deltaTime;
        if (movement != Vector2.zero)
        {
            playerAnimator.SetBool("IsMoving", true);
            transform.Translate(movement, Space.World);
        } else
        {
            playerAnimator.SetBool("IsMoving", false);
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

    public float GetPlayerHealth()
    {
        return playerHealth;
    }

    IEnumerator setDashTimer()
    {
        yield return new WaitForSeconds(dashTime);
        moveSpeedMultiplier = DEFAULT_MOVE_SPEED;
    }

    IEnumerator setCanDashTimer()
    {
        yield return new WaitForSeconds(canDashTime);
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

    public void setCanDashTime(float canDashTime)
    {
        if (canDashTime < 0)
        {
            canDashTime = DEFAULT_CAN_DASH_TIME;
        }
        this.canDashTime = canDashTime;
        
    }

    public void setDashSpeed(float dashSpeed)
    {
        if (dashSpeed < 0)
        {
            dashSpeed = DEFAULT_DASH_SPEED;
        }
            
        this.dashSpeed = dashSpeed;
    }

    public void setMoveSpeed(float moveSpeed)
    {
        if (moveSpeed < 0)
        {
            moveSpeed = DEFAULT_MOVE_SPEED;
        }
        this.moveSpeed = moveSpeed;
    }

    public void setDashTime(float dashTime)
    {
        if(dashTime < 0)
        {
            dashTime = DEFAULT_DASH_TIME;
        }
        this.dashTime = dashTime;
    }

}
