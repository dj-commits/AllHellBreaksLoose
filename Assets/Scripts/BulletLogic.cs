using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
    [SerializeField] float playerBulletSpreadMin;
    [SerializeField] float playerBulletSpreadMax;
    float playerX;
    float playerY;
    float cameraX;
    float cameraY;
    float cameraH;
    float cameraW;
    Camera cam;

    

    Rigidbody2D rb;
    


    
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        cameraH = 2f * cam.orthographicSize;
        cameraW= cameraH * cam.aspect;
        rb = GetComponent<Rigidbody2D>(); 


    }

    // Update is called once per frame
    void Update()
    {
        Vector2 bulletVelocity = new Vector2(Random.Range(playerBulletSpreadMin, playerBulletSpreadMax), transform.position.y * bulletSpeed);
        rb.velocity = bulletVelocity;
    }
}
