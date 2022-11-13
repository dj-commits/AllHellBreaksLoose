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

    // Components
    private Rigidbody2D rb;

    // Vectors
    Vector2 playerPosition;
    Vector3 mousePosition;

    Gun gun;

    const float DEFAULT_MOVE_SPEED = 1;


    // Start is called before the first frame update
    void Start()
    {
        gun = GameObject.Find("gun").GetComponent<Gun>();
        
        rb = GetComponent<Rigidbody2D>();
        canDash = true;
        moveSpeedMultiplier = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        // check for playerDeath
        if (playerHealth <= 0)
        {
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


        transform.Translate(movement, Space.World);

        if (Input.GetButton("Shoot"))
        {
            gun.Shoot();
        }

    }

    private void FixedUpdate()
    {
        


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
}
