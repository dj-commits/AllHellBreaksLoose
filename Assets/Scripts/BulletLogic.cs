using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{

    float playerX;
    float playerY;
    float cameraX;
    float cameraY;
    float cameraH;
    float cameraW;
    Camera cam;

    [SerializeField] float bulletSpeed;

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

        Vector2 bulletVelocity = this.transform.position * bulletSpeed * Time.deltaTime;
        rb.velocity = bulletVelocity;



    }
}
