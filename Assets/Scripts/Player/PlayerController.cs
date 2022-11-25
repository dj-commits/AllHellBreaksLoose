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
    float defaultMoveSpeedMultiplier;
    [SerializeField]
    float playerHealth;
    [SerializeField]
    float dashSpeed;
    [SerializeField]
    bool isShielded;

    public ContactFilter2D movementFilter;
    [SerializeField]
    public float collisionOffset;

    [SerializeField] public bool isAlive;

    // GameObjects
    [SerializeField]
    Powerup powerUp;
    GameObject shield;
    Healthbar healthbar;

    // Components
    private Rigidbody2D rb;
    private Animator playerAnimator;
    private BoxCollider2D collider;

    // Vectors
    Vector2 playerPosition;
    Vector3 mousePosition;

    Gun gun;

    // Start is called before the first frame update
    void Start()
    {
        gun = GameObject.Find("gun").GetComponent<Gun>();
        playerAnimator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        healthbar = GameObject.Find("PlayerHealthbar").GetComponent<Healthbar>();

        moveSpeedMultiplier = 1f;
        isAlive = true;
    }

    // Update is called once per frame
    void FixedUpdate()
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
        Vector2 movementInput = new Vector2(inputX, inputY);

        List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

        if (Input.GetKey(KeyCode.LeftShift))
        {
            setMoveSpeedMultiplier(dashSpeed);
        }
        else
        {
            setMoveSpeedMultiplier(getDefaultMoveSpeedMultiplier());
        }


        int count = rb.Cast(
            movementInput, // X and Y values between -1 and 1 that represent the direction from the body to look for collisions
            movementFilter, // the settings that determine where a collision can occur on such as layers to collide with
            castCollisions, // List of collisions to store the found collisions into after the Cast has finished
            moveSpeed * Time.deltaTime + collisionOffset); //The amount to cast equal to the movement plus an offset
        if (count == 1)
        {
            movementInput = Vector2.zero;
        }
        else
        { 
            rb.MovePosition(rb.position + movementInput * moveSpeed * Time.deltaTime); 
        }
            

            //Vector2 movement = new Vector2(inputX * moveSpeed * moveSpeedMultiplier, inputY * moveSpeed * moveSpeedMultiplier);
            //movement *= Time.deltaTime;



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

    public float GetPlayerHealth()
    {
        return playerHealth;
    }


    public void TakeDamage(float damage)
    {

        if (isShielded)
        {
            damage = 0;
        }

        playerHealth -= damage;
        healthbar.SetHealth(playerHealth);
    }

    public void setPowerup(Powerup powerUp)
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

    public float getDefaultMoveSpeedMultiplier()
    {
        return defaultMoveSpeedMultiplier;
    }

    public void setShielded(bool status)
    {
        isShielded = status;
    }

    public Gun getGun()
    {
        return gun;
    }

    public BoxCollider2D getCollider()
    {
        return this.collider;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
    }
}
