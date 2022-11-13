using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    [SerializeField] float enemyMovementSpeed;
    [SerializeField] float enemyHealth;

    [SerializeField]
    GameObject powerUp;

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
            CreatePowerup();
            Destroy(gameObject);
        }
        transform.position = Vector2.MoveTowards(transform.position, player.position, enemyMovementSpeed * Time.deltaTime);
    }

    public void TakeDamage(float damage)
    {
        enemyHealth -= damage;
    }

    public void CreatePowerup()
    {
        Quaternion rotation = this.transform.rotation;

        GameObject powerUpClone = Instantiate(powerUp, this.transform.position, rotation);
    }
}
