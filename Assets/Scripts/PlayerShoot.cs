using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bullet;
    public float bulletSpeed = 5.0f;

    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetButton("Shoot"))
        {
            Vector2 target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            Vector2 myPos = new Vector2(transform.position.x, transform.position.y + 1);
            Vector2 direction = target - myPos;
            direction.Normalize();

            float bulletAngle = Vector2.SignedAngle(Vector2.up, direction);
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, bulletAngle));


            GameObject projectile = (GameObject)Instantiate(bullet, myPos, rotation);
            projectile.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        }
    }
}