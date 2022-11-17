using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected float moveSpeed;
    protected float moveSpeedMultiplier;
    protected float health;
    protected float damage;
    protected float dropChance;
    protected bool isAlive;
    

    GameObject player;
    EnemyManager em;

    [SerializeField]
    const float DEFAULT_MOVE_SPEED = 3f;
    [SerializeField]
    const float DEFAULT_ENEMY_MOVESPEEDMULTIPLIER = 0.1f;
    [SerializeField]
    const float DEFAULT_ENEMY_HEALTH = 10;
    [SerializeField]
    const float DEFAULT_ENEMY_DAMAGE = 2;
    [SerializeField]
    const float DEFAULT_DROP_CHANCE = 2;

    // Start is called before the first frame update
    public virtual void Start()
    {
        player = GameObject.Find("Player");
        em = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        isAlive = true;

    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (CheckPlayerDeath())
        {
            // do something
        }
        else
        {
            if (CheckForDeath())
            {
                Death();
            }
            else
            {
                StalkPlayer();
            }
        }
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
        em.KillEnemy();
        Destroy(gameObject);

    }

    public virtual bool CheckForDeath()
    {
        if (health <= 0)
        {
            return true;
        }
        return false;
    }

    public virtual bool CheckPlayerDeath()
    {
        if (player.GetComponent<PlayerController>().GetPlayerHealth() <= 0)
        {
            return true;
        }
        return false;
    }

    public virtual void LootDrop(GameObject powerUp)
    {
        Debug.Log("GameObject: " + gameObject.name);
        float chance = em.GetDropChance(gameObject);
        Debug.Log("Chance: " + chance);
        float probability = Random.value;
        Debug.Log("Probability = " + probability);
        if (probability <= chance)
        {
            Debug.Log("Probability less than or equal to chance");
            em.ResetDropChance();
            Quaternion rotation = this.transform.rotation;
            GameObject powerUpClone = Instantiate(powerUp, this.transform.position, rotation);
        }else
        {
            em.IncrementDropChance(gameObject, .1f);
            Debug.Log("Incrementing Drop Chance for: " + gameObject.name);
        }
        
    }
}
