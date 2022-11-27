using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpShot : MonoBehaviour
{

    [SerializeField] float bulletSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
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
        Destroy(gameObject);
    }
}
