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
    private bool canHurtPlayer;
    private bool bulletOutOfView;
    private float camHeight;
    private float camWidth;

    Rigidbody2D rb;

    Camera cam;

    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canHurtPlayer = false;
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

        // Check position of bullet
        Vector3 bulletPos = this.transform.position;
        //Debug.Log("bulletPos: " + bulletPos);
        Vector3 bulletView = cam.WorldToViewportPoint(bulletPos);
        if (bulletOutOfView == false)
        {
            if (!(bulletView.x > 0))
            {
                // bullet exited left of screen

            }
            else if (!(bulletView.x < 1))
            {
                // bullet exited right of screen
            }
            else if (!(bulletView.y > 0))
            {
                // bullet exited below of screen
            }
            else if (!(bulletView.y < 1))
            {
                // bullet exited top of screen
                FlipBullet(bulletPos, bulletView);

            }

        }
        
    }
    public void SetPosition(Vector3 bulletPosition) 
    {
        this.transform.position = bulletPosition;
    }

    public void FlipBullet(Vector3 bulletPos, Vector3 bulletView)
    {
        // This works, but it's manually coded to work with the bullet exiting the top of the screen (in that instance, you'd want to apply the algorithm for the y position, but not for the x position.
        // Can manually code it this way, but that seems gross. Also, bullets following the player (not heat seeking, but with a margin of error) might solve this issue entirely.


        Debug.Log("Camera Height = " + camHeight);
        Debug.Log("Camera Width = " + camWidth);
        Debug.Log("Prior to the reset of position.");
        Debug.Log("bulletPos: " + bulletPos);
        Debug.Log("bulletView: " + bulletView);
        float currentBulletX = cam.ViewportToWorldPoint(bulletView).x; // 1
        Debug.Log("currentBulletX = " + currentBulletX);
        float currentBulletY = cam.ViewportToWorldPoint(bulletView).y; // 18
        Debug.Log("currentBulletY = " + currentBulletY);
        float bulletXOffset = currentBulletX - camWidth;
        Debug.Log("bulletXOffset = " + bulletXOffset);
        float bulletYOffset = currentBulletY - camHeight;
        Debug.Log("bulletYOffset = " + bulletYOffset);
        bulletPos.x = -cam.ViewportToWorldPoint(bulletView).x;
        bulletPos.y = bulletYOffset;
        SetPosition(bulletPos);
        Debug.Log("Post position reset.");
        Debug.Log("bulletPos: " + bulletPos);
        Debug.Log("bulletView: " + bulletView);
    }

}
