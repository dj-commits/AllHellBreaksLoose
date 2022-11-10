using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BulletLogic : MonoBehaviour
{
    // Layer variables
    private int bulletLayer;
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
    [SerializeField] bool canHurtPlayer;
    [SerializeField] bool canHurtEnemy;
    [SerializeField] bool bulletOutOfView;

    // Components
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Camera cam;
    EnemyLogic enemyLogic;
    PlayerController playerController;

    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        canHurtPlayer = false;
        canHurtEnemy = true;
        camHeight = 2f * cam.orthographicSize;
        camWidth = (camHeight * cam.aspect) / 2;
        bulletLayer = LayerMask.NameToLayer("Bullet");
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

        if (bulletLoops > 0)
        {
            canHurtEnemy = false;
            canHurtPlayer = true;
        }

        if (canHurtEnemy == false)
        {
            Physics2D.IgnoreLayerCollision(bulletLayer, enemyLayer, true);
        } else
        {
            Physics2D.IgnoreLayerCollision(bulletLayer, enemyLayer, false);
        }

        if (canHurtPlayer == false)
        {
            Physics2D.IgnoreLayerCollision(bulletLayer, playerLayer, true);
        } else
        {
            Physics2D.IgnoreLayerCollision(bulletLayer, playerLayer, false);
        }

        // Check position of bullet
        Vector3 bulletPos = this.transform.position;
        //Debug.Log("bulletPos: " + bulletPos);
        Vector3 bulletView = cam.WorldToViewportPoint(bulletPos);
        Debug.Log("bulletView: " + bulletView);
        if (bulletOutOfView == false)
        {
            if (!(bulletView.x > 0))
            {
                // bullet exited left of screen
                bulletLoops++;
                bulletOutOfView = true;
                FlipBullet(bulletPos, bulletView, 3);
            }
            else if (!(bulletView.x < 1))
            {
                // bullet exited right of screen
                bulletLoops++;
                bulletOutOfView = true;
                FlipBullet(bulletPos, bulletView, 2);
            }
            else if (!(bulletView.y > 0))
            {
                // bullet exited below of screen
                bulletLoops++;
                bulletOutOfView = true;
                FlipBullet(bulletPos, bulletView, 1);
            }
            else if (!(bulletView.y < 1))
            {
                // bullet exited top of screen
                bulletLoops++;
                bulletOutOfView = true;
                FlipBullet(bulletPos, bulletView, 0);

            }

        }
        
    }
    public void SetPosition(Vector3 bulletPosition) 
    {
        this.transform.position = bulletPosition;
        this.bulletOutOfView = false;
    }

    public void FlipBullet(Vector3 bulletPos, Vector3 bulletView, int switchCase)
    {
        Debug.Log("bulletPos: " + bulletPos);
        Debug.Log("bulletView: " + bulletView);

        // Mostly working, weird bug on corners of the screen
  
        if (switchCase == 0) // upper
        {
            float currentBulletX = cam.ViewportToWorldPoint(bulletView).x; // 1
            float currentBulletY = cam.ViewportToWorldPoint(bulletView).y; // -18
            float bulletXOffset = currentBulletX - camWidth;
            float bulletYOffset = currentBulletY - camHeight;
            bulletPos.x = -cam.ViewportToWorldPoint(bulletView).x;
            bulletPos.y = bulletYOffset;
            spriteRenderer.color = Color.red;
            SetPosition(bulletPos);
        }
        else if (switchCase == 1) // lower
        {
            float currentBulletX = cam.ViewportToWorldPoint(bulletView).x; // 1
            float currentBulletY = cam.ViewportToWorldPoint(bulletView).y; // -18
            float bulletXOffset = currentBulletX - camWidth;
            float bulletYOffset = currentBulletY + camHeight;
            bulletPos.x = -cam.ViewportToWorldPoint(bulletView).x;
            bulletPos.y = bulletYOffset;
            spriteRenderer.color = Color.red;
            SetPosition(bulletPos);
        }
        else if (switchCase == 2) // right
        {
            float currentBulletX = cam.ViewportToWorldPoint(bulletView).x;
            float currentBulletY = cam.ViewportToWorldPoint(bulletView).y;
            float bulletXOffset = currentBulletX - camWidth * 2;
            float bulletYOffset = currentBulletY + camHeight;
            bulletPos.x = bulletXOffset;
            bulletPos.y = -cam.ViewportToWorldPoint(bulletView).y;
            spriteRenderer.color = Color.red;
            SetPosition(bulletPos);

        }
        else if (switchCase == 3) // left
        {
            float currentBulletX = cam.ViewportToWorldPoint(bulletView).x;
            float currentBulletY = cam.ViewportToWorldPoint(bulletView).y;
            float bulletXOffset = currentBulletX + camWidth * 2;
            float bulletYOffset = currentBulletY + camHeight;
            bulletPos.x = bulletXOffset;
            bulletPos.y = -cam.ViewportToWorldPoint(bulletView).y;
            spriteRenderer.color = Color.red;
            SetPosition(bulletPos);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
     if (canHurtEnemy)
        {
            if (other.gameObject.tag == "Enemy")
            {
                enemyLogic = other.gameObject.GetComponent<EnemyLogic>();
                enemyLogic.TakeDamage(bulletDamage);
                Destroy(gameObject);

            }
        }

     if (canHurtPlayer)
        {
            if (other.gameObject.tag == "Player")
            {
                playerController = other.gameObject.GetComponent<PlayerController>();
                playerController.TakeDamage(bulletDamage);
                Destroy(gameObject);

            }
        }
    }

}
