using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    protected float moveSpeed;
    [SerializeField]
    protected float moveSpeedMultiplier;
    [SerializeField]
    protected float health;
    [SerializeField]
    protected float damage;
    [SerializeField]
    protected bool isAlive;

    [SerializeField]
    protected float attackRange;

    [SerializeField]
    protected float dropChance;
    [SerializeField]
    protected float defaultDropChance;
    [SerializeField]
    protected float dropChanceIncrementValue;

    GameObject player;
    EnemyManager em;
    protected List<GameObject> lootTable;
    Animator animator;

    const float DEFAULT_MOVE_SPEED = 3f;
    const float DEFAULT_ENEMY_MOVESPEEDMULTIPLIER = 0.1f;
    const float DEFAULT_ENEMY_HEALTH = 10;
    const float DEFAULT_ENEMY_DAMAGE = 2;

    // Start is called before the first frame update
    public virtual void Start()
    {
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        em = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        isAlive = true;
        lootTable = new List<GameObject>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (CheckForDeath())
        {
            Death();
        }

        if (CheckPlayerDeath())
        {
            // do something
        }
        else
        {
            Stalk(player);
            if (CheckAttackRange())
            {
                Attack(player);
            }
        }
    }

    public virtual void Stalk(GameObject other)
    {
        animator.SetBool("IsMoving", true);
        transform.position = Vector2.MoveTowards(transform.position, other.transform.position, moveSpeed * Time.deltaTime);
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
        LootDrop();
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

    public virtual bool CheckAttackRange()
    {
        if (Vector2.Distance(this.transform.position, player.transform.position) <= attackRange)
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

    public virtual void LootDrop()
    {
        Debug.Log("GameObject: " + gameObject.name);
        //float chance = em.GetDropChance(gameObject);
        float chance = this.dropChance;
        Debug.Log("Chance: " + chance);
        float probability = Random.value;
        Debug.Log("Probability = " + probability);
        if (probability <= chance)
        {
            Debug.Log("Probability less than or equal to chance");

            //Reset the drop chance for this particular enemy type
            em.getEnemyObject(gameObject.name).GetComponent<Enemy>().ResetDropChance();

            GameObject powerUp = DetermineLoot();
            if (powerUp != null)
            {
                GameObject powerUpClone = Instantiate(powerUp, this.transform.position, this.transform.rotation);
            }
        }else
        {
            //Increment drop chance for default enemy object that gets cloned from
            //So that each enemy that gets cloned gets a slightly higher drop chance
            GameObject enemy = em.getEnemyObject(gameObject.name);
            Enemy enemyLogic = enemy.GetComponent<Enemy>();
            enemyLogic.IncrementDropChance();
            Debug.Log("Incrementing Drop Chance for: " + gameObject.name);
        }
        
    }

    public GameObject DetermineLoot()
    {
        //Calculate total value of powerup chances in loot table
        float totalPowerupChance = 0;
        foreach (GameObject p in lootTable)
        {
            totalPowerupChance += p.GetComponent<Powerup>().getChanceToDrop();
        }

        float probability = Random.Range(0f, totalPowerupChance);
        //Randomize order of lootTable
        this.lootTable = Shuffle(lootTable);

        //Based on random probablility, determine which powerup
        foreach (GameObject p in lootTable)
        {
            float chance = p.GetComponent<Powerup>().getChanceToDrop();
            if (chance >= probability)
            {
                return p;
            }
            probability -= chance;
        }
        return null;
    }

    public List<GameObject> Shuffle(List<GameObject> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            GameObject temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }

        return list;
    }

    public void IncrementDropChance()
    {
        if (dropChance >= 1)
        {
            ResetDropChance();
        }
        else
        {
            this.dropChance += dropChanceIncrementValue;
        }
    }

    public void ResetDropChance()
    {
        this.dropChance = defaultDropChance;
    }
}
