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
    [SerializeField] float timeBetweenShots;
    [SerializeField] float timeBetweenDash;

    // Bullet vars
    [SerializeField] float bulletSpawnDistance;

    // Booleans
    [SerializeField] bool canShoot;
    [SerializeField] bool canDash;

    // Components
    Transform gunTransform;
    [SerializeField] GameObject bullet;
    private Quaternion bulletDirection;
    private Rigidbody2D rb;

    // Vectors
    Vector2 playerPosition;
    Vector3 mousePosition;

    // Scripts
    BulletLogic bulletLogic;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canShoot = true;
        canDash = true;
        gunTransform = GameObject.Find("gun").GetComponent<Transform>();
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

        // player sprite rotation around Mouse

        // Get mouse position (in Vector 3)
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Get direction of the angle between mousePosition and player
        Vector2 gunDirection = mousePosition - gunTransform.transform.position;

        // Get the angle between the "forward position" of the object (up in the case of this sprite), and direction (angle between mousePosition and player). Signed Angle instead of reg. Angle because it allows for full 360* of rotation, reg Angle only 180
        float angle = Vector2.SignedAngle(Vector2.up, gunDirection);

        // Actually change the transform to rotate, locking x and y because that can cause sprite to disappear.
        gunTransform.transform.eulerAngles = new Vector3(0, 0, angle);

        // Movement

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            moveSpeedMultiplier = dashSpeed;
            canDash = false;
            StartCoroutine(dashWaitTimer());
        }

        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(inputX * moveSpeed * moveSpeedMultiplier, inputY * moveSpeed * moveSpeedMultiplier);
        movement *= Time.deltaTime;


        transform.Translate(movement, Space.World);

        // Shooting

        if (Input.GetButton("Shoot"))
        {
            if (canShoot)
            {

                CreateBullet();
                canShoot = false;
                StartCoroutine(shootWaitTimer());
                
            }

        }
        
    }

    private void FixedUpdate()
    {
        


    }

    IEnumerator shootWaitTimer()
    {
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    IEnumerator dashWaitTimer()
    {
        yield return new WaitForSeconds(timeBetweenDash);
        moveSpeedMultiplier = 1;
        canDash = true;
    }

    private void CreateBullet()
    {

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        Vector2 gunPos = gunTransform.position;
        Vector2 gunDirection = mousePos - gunPos;
        gunDirection.Normalize();
        gunDirection += (Vector2)gunTransform.up;

        float bulletAngle = Vector2.SignedAngle(Vector2.up, gunDirection);
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, bulletAngle));

        GameObject bulletClone = Instantiate(bullet, gunPos + gunDirection, rotation);

        bulletClone.GetComponent<Rigidbody2D>().velocity = gunDirection * bulletClone.GetComponent<BulletLogic>().bulletSpeed;
        
    }

    public void TakeDamage(float damage)
    {
        playerHealth -= damage;
    }
}
