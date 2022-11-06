using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    [SerializeField] public float bulletSpeed;
    [SerializeField] float playerBulletSpreadMin;
    [SerializeField] float playerBulletSpreadMax;
    [SerializeField] float bulletDestroyTimer;
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

    private void Awake()
    {
        Destroy(this.gameObject, bulletDestroyTimer);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetVelocity() 
    {
       
    }

}
