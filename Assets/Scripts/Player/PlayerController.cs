using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    [SerializeField] public bool isAlive;
    [SerializeField] private bool isGod;

    Vector3 dmgTextSpawn;

    // GameObjects
    [SerializeField]
    Powerup powerUp;
    Healthbar healthbar;
    GameManager gameManager;

    [SerializeField]
    GameObject dmgText;

    // Components
    private Rigidbody2D rb;
    private Animator playerAnimator;

    // Vectors
    Vector2 playerPosition;
    Vector3 mousePosition;

    Gun gun;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gun = GameObject.Find("gun").GetComponent<Gun>();
        playerAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        healthbar = GameObject.Find("PlayerHealthbar").GetComponent<Healthbar>();
        if (isGod)
        {
            playerHealth = 1000000000000f;
        }
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

        // Movement
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        Vector2 movementInput = new Vector2(inputX, inputY);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            setMoveSpeedMultiplier(dashSpeed);
        }
        else
        {
            setMoveSpeedMultiplier(getDefaultMoveSpeedMultiplier());
        }


        Vector2 movement = new Vector2(inputX * moveSpeed * moveSpeedMultiplier, inputY * moveSpeed * moveSpeedMultiplier);
        movement *= Time.deltaTime;
        if (movement != Vector2.zero)
        {
            playerAnimator.SetBool("isMoving", true);
            transform.Translate(movement, Space.World);
        }
        else
        {
            playerAnimator.SetBool("isMoving", false);
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
        this.GetComponent<SpriteRenderer>().color = Color.red;
        StartCoroutine(DamagedAnimTimer());

        if (damage > 0) {
            float dmgTextSpawnXOffset = Random.Range(.001f, .5f);
            float dmgTextSpawnYOffset = Random.Range(.001f, .7f);
            dmgTextSpawn = new Vector3(this.transform.position.x + dmgTextSpawnXOffset, this.transform.position.y + dmgTextSpawnYOffset, this.transform.position.z);
            dmgText.GetComponent<DamageText>().SetText(damage);
            Instantiate(dmgText, dmgTextSpawn, Quaternion.identity);

        }
    }

    float dmgAnimTimer = 0.1f;

    IEnumerator DamagedAnimTimer()
    {
        yield return new WaitForSeconds(dmgAnimTimer);
        this.GetComponent<SpriteRenderer>().color = Color.white;
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
        return this.GetComponent<BoxCollider2D>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "BossSpawn")
        {
            gameManager.BossSpawnTrigger();
        }
    }
}
