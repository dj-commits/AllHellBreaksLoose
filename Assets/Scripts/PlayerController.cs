using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float moveSpeedMultiplier;
    [SerializeField] float timeBetweenShots;
    [SerializeField] float bulletSpeed;
    [SerializeField] float bulletDestroyTimer;
    [SerializeField] bool canShoot;

    private Quaternion bulletDirection;
    [SerializeField] GameObject bullet;
    Vector2 playerPosition;
    Vector3 mousePosition;

    BulletLogic bulletLogic;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        // player sprite rotation around Mouse

        // Get mouse position (in Vector 3)
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Get direction of the angle between mousePosition and player
        Vector2 direction = mousePosition - transform.position;

        // Get the angle between the "forward position" of the object (up in the case of this sprite), and direction (angle between mousePosition and player). Signed Angle instead of reg. Angle because it allows for full 360* of rotation, reg Angle only 180
        float angle = Vector2.SignedAngle(Vector2.up, direction);

        // Actually change the transform to rotate, locking x and y because that can cause sprite to disappear.
        transform.eulerAngles = new Vector3(0, 0, angle);

        // Movement
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(inputX * moveSpeed, inputY * moveSpeed);
        movement *= Time.deltaTime;


        transform.Translate(movement);

        // Shooting

        if (Input.GetButton("Shoot"))
        {
            if (canShoot)
            {
                
                CreateBullet(mousePosition, direction);
                
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

    private void CreateBullet(Vector3 mousePos, Vector2 angleDirection)
    {
        angleDirection.Normalize();
        float bulletAngle = Vector2.SignedAngle(Vector2.up, angleDirection);
        GameObject bulletClone = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, bulletAngle)));
        Destroy(bulletClone, bulletDestroyTimer);
    }
}
