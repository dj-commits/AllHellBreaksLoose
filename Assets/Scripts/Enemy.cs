using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected float moveSpeed;
    protected float moveSpeedMultiplier;
    protected float health;
    protected float damage;
    protected bool isAlive;
    

    GameObject player;

    [SerializeField]
    const float DEFAULT_MOVE_SPEED = 3f;
    [SerializeField]
    const float DEFAULT_ENEMY_MOVESPEEDMULTIPLIER = 0.1f;
    [SerializeField]
    const float DEFAULT_ENEMY_HEALTH = 10;
    [SerializeField]
    const float DEFAULT_ENEMY_DAMAGE = 2;
    
    // Start is called before the first frame update
    public virtual void Start()
    {
        player = GameObject.Find("Player");
        isAlive = true;

    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public virtual void StalkPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
    }

    public virtual void TakeDamage(float damage)
    {
        health -= damage;
    }

    public virtual void Attack(GameObject other)
    {
        other.GetComponent<PlayerController>().TakeDamage(damage);
    }

    public virtual void Death()
    {
        isAlive = false;
        Destroy(gameObject);
    }

    public virtual void CheckForDeath()
    {
        if (health <= 0)
        {
            Death();
        }
    }

    public virtual void LootDrop(GameObject powerUp)
    {
        Quaternion rotation = this.transform.rotation;

        GameObject powerUpClone = Instantiate(powerUp, this.transform.position, rotation);
    }
}
