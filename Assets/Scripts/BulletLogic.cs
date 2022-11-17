using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BulletLogic : MonoBehaviour
{
    // Layer variables
    private int initBulletLayer;
    private int secondBulletLayer;
    private int enemyLayer;
    private int playerLayer;
    // Bullet variables
    [SerializeField] public float bulletSpeed;
    [SerializeField] float playerBulletSpreadMin;
    [SerializeField] float playerBulletSpreadMax;
    [SerializeField] float bulletDamage;
    [SerializeField] int bulletLoops;

    // Camera variables
    private float camHeight;
    private float camWidth;

    // Timing variables
    [SerializeField] float bulletDestroyTimer;
    [SerializeField] int destroyOnLoopNumberCounter;

    // Booleans
    [SerializeField] bool bulletOutOfView;

    // Components
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Camera cam;
    Enemy enemy;
    PlayerController playerController;
    BoxCollider2D boxCollider2D;

    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        camHeight = 2f * cam.orthographicSize;
        camWidth = camHeight * cam.aspect;
        initBulletLayer = LayerMask.NameToLayer("initBullet");
        secondBulletLayer = LayerMask.NameToLayer("secondBullet");
        enemyLayer = LayerMask.NameToLayer("Enemy");
        playerLayer = LayerMask.NameToLayer("Player");
    }

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bulletLoops >= destroyOnLoopNumberCounter)
        {
            Destroy(gameObject);
        }

        // Check position of bullet
        Vector3 bulletPos = this.transform.position;
        //Debug.Log("bulletPos: " + bulletPos);
        Vector3 bulletView = cam.WorldToViewportPoint(bulletPos);
        //Debug.Log("bulletView: " + bulletView);
        if (bulletOutOfView == false)
        {
            if (!(bulletView.x > 0))
            {
                // bullet exited left of screen
                bulletLoops++;
                bulletOutOfView = true;
                FlipBullet(bulletPos, bulletView, 3);
                Debug.Log("left");
            }
            else if (!(bulletView.x < 1))
            {
                // bullet exited right of screen
                bulletLoops++;
                bulletOutOfView = true;
                FlipBullet(bulletPos, bulletView, 2);
                Debug.Log("right");
            }
            else if (!(bulletView.y > 0))
            {
                // bullet exited below of screen
                bulletLoops++;
                bulletOutOfView = true;
                FlipBullet(bulletPos, bulletView, 1);
                Debug.Log("down");
            }
            else if (!(bulletView.y < 1))
            {
                // bullet exited top of screen
                bulletLoops++;
                bulletOutOfView = true;
                FlipBullet(bulletPos, bulletView, 0);
                Debug.Log("top");
            }

        }
        
    }

    public void SetPosition(Vector3 bulletPosition) 
    {
        if (bulletLoops == 1)
        {
            rb.velocity /= Random.Range(1, bulletSpeed);
        }

        if (gameObject.layer != secondBulletLayer)
        {
            gameObject.layer = secondBulletLayer;
        }

        // this isn't working yet
        // rb.angularDrag += .2f;
        this.transform.position = bulletPosition;
        this.bulletOutOfView = false;
        

    }

    public void FlipBullet(Vector3 bulletPos, Vector3 bulletView, int switchCase)
    {
        //Debug.Log("bulletPos: " + bulletPos);
        //Debug.Log("bulletView: " + bulletView);

        // Mostly working, weird bug on corners of the screen
  
        if (switchCase == 0) // upper
        {
            float currentBulletX = cam.ViewportToWorldPoint(bulletView).x; // .067
            float currentBulletY = cam.ViewportToWorldPoint(bulletView).y; // 2.457

            float bulletXOffset = cam.transform.position.x - currentBulletX; // -0.067
            bulletPos.x = cam.transform.position.x + bulletXOffset; // -0.067

            bulletPos.y = currentBulletY - camHeight;

            spriteRenderer.color = Color.red;
            SetPosition(bulletPos);
        }
        else if (switchCase == 1) // lower
        {
            float currentBulletX = cam.ViewportToWorldPoint(bulletView).x; // 1
            float currentBulletY = cam.ViewportToWorldPoint(bulletView).y; // -18

            float bulletXOffset = cam.transform.position.x - currentBulletX;
            bulletPos.x = cam.transform.position.x + bulletXOffset;

            bulletPos.y = currentBulletY + camHeight;

            spriteRenderer.color = Color.red;
            SetPosition(bulletPos);
        }
        else if (switchCase == 2) // right
        {
            float currentBulletX = cam.ViewportToWorldPoint(bulletView).x;
            float currentBulletY = cam.ViewportToWorldPoint(bulletView).y;
            float bulletXOffset = currentBulletX - camWidth;
            bulletPos.x = bulletXOffset;

            float bulletYOffset = cam.transform.position.y - currentBulletY;
            bulletPos.y = cam.transform.position.y + bulletYOffset;

            spriteRenderer.color = Color.red;
            SetPosition(bulletPos);

        }
        else if (switchCase == 3) // left
        {
            float currentBulletX = cam.ViewportToWorldPoint(bulletView).x;
            float currentBulletY = cam.ViewportToWorldPoint(bulletView).y;
            float bulletXOffset = currentBulletX + camWidth*2;
            bulletPos.x = bulletXOffset;

            float bulletYOffset = cam.transform.position.y - currentBulletY;
            bulletPos.y = cam.transform.position.y + bulletYOffset;

            spriteRenderer.color = Color.red;
            SetPosition(bulletPos);
        }
    }

    private void SetIgnoreCollision(BoxCollider2D thisCollider, Collider2D otherCollider, bool disabled)
    {
        Physics2D.IgnoreCollision(thisCollider, otherCollider, disabled);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemy = other.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(bulletDamage);
            Destroy(gameObject);

        }


        if (other.gameObject.tag == "Player")
        {
            playerController = other.gameObject.GetComponent<PlayerController>();
            playerController.TakeDamage(bulletDamage);
            Destroy(gameObject);

        }

    }
}
