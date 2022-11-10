using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BulletLogic : MonoBehaviour
{
    [SerializeField] public float bulletSpeed;
    [SerializeField] float playerBulletSpreadMin;
    [SerializeField] float playerBulletSpreadMax;
    [SerializeField] float bulletDestroyTimer;
    [SerializeField] int destroyOnLoopNumberCounter;
    //private bool canHurtPlayer;
    private bool bulletOutOfView;
    private float camHeight;
    private float camWidth;
    [SerializeField] int bulletLoops;

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    Camera cam;

    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //canHurtPlayer = false;
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();

        camHeight = 2f * cam.orthographicSize;
        camWidth = (camHeight * cam.aspect) / 2;
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
}
