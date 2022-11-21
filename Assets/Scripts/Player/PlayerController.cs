using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement vars
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    float moveSpeedMultiplier;
    [SerializeField]
    float playerHealth;

    [SerializeField] public bool isAlive;

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

    // Start is called before the first frame update
    void Start()
    {
        gun = GameObject.Find("gun").GetComponent<Gun>();
        playerAnimator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();

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

    public float GetPlayerHealth()
    {
        return playerHealth;
    }

    public void TakeDamage(float damage)
    {
        playerHealth -= damage;
    }

    public void setPowerup(GameObject powerUp)
    {
        this.powerUp = powerUp;
    }

    public float getMoveSpeed()
    {
        return moveSpeed;
    }

    public void setMoveSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }

    public float getMoveSpeedMultiplier()
    {
        return moveSpeedMultiplier;
    }

    public void setMoveSpeedMultiplier(float moveSpeedMultiplier)
    {
        this.moveSpeedMultiplier = moveSpeedMultiplier;
    }

    public Gun getGun()
    {
        return gun;
    }

}
