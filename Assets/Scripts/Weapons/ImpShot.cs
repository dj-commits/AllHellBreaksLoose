using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpShot : MonoBehaviour
{
    [SerializeField]
    float damage;

    [SerializeField] float bulletSpeed;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public float GetBulletSpeed()
    {
        return bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Walls")
        {
            Destroy(gameObject);
        }
    }
}
