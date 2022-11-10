using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    [SerializeField] float enemyMovementSpeed;
    [SerializeField] float enemyHealth;
    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
        transform.position = Vector2.MoveTowards(transform.position, player.position, enemyMovementSpeed * Time.deltaTime);
    }

    public void TakeDamage(float damage)
    {
        enemyHealth -= damage;
    }
}
