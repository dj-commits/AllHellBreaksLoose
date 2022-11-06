using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float moveSpeedMultiplier;
    [SerializeField] float timeBetweenShots;
    [SerializeField] float bulletDestroyTimer;
    [SerializeField] float bulletSpawnDistance;
    [SerializeField] bool canShoot;
    Transform gunTransform;

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
        gunTransform = GameObject.Find("gun").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
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
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(inputX * moveSpeed, inputY * moveSpeed);
        movement *= Time.deltaTime;


        transform.Translate(movement, Space.World);

        // Shooting

        if (Input.GetButton("Shoot"))
        {
            if (canShoot)
            {

                CreateBullet();
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
}
