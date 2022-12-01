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
    [SerializeField]
    protected float timeBetweenStalk;
    [SerializeField]
    protected float timeUntilNextStalk;
    [SerializeField]
    protected float timeUntilNextAttack;
    [SerializeField]
    protected bool canStalk;
    [SerializeField]
    protected bool canAttack;

    protected bool isStopped;

    protected ContactFilter2D movementFilter;
    protected int wallLayer;
    protected Rigidbody2D rb;
    protected SpriteRenderer spriteRenderer;
    [SerializeField]
    public float collisionOffset;

    protected UIManager uiManager;
    protected GameObject player;
    protected EnemyManager em;
    [SerializeField]
    protected List<GameObject> lootTable;
    protected Animator animator;
    [SerializeField]
    float timeUntilAttackFinished;


    AudioManager audioManager;

    // Start is called before the first frame update
    public virtual void Start()
    {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        movementFilter.useLayerMask = true;
        movementFilter.layerMask = LayerMask.NameToLayer("Walls");
        em = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        isAlive = true;
        canStalk = false;
        canAttack = true;
        lootTable = new List<GameObject>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        StartCoroutine(TimeUntilNextStalkTimer());
    }

    // Update is called once per frame
    public virtual void Update()
    {

        if (CheckForDeath())
        {
            Death();
        }

        if (this.transform.position.x > player.transform.position.x)
        {
            // player to the left
            spriteRenderer.flipX = true;
        }
        else
        {
            // player to the right
            spriteRenderer.flipX = false;
        }

        if (CheckPlayerDeath())
        {
            //Debug.Log("Player died");
        }
        else
        {
            if (canStalk)
            {
                Stalk(player);
                
            }
            
            
            if (CheckAttackRange(player) && canAttack)
            {
                Attack(player);
                StartCoroutine(TimeUntilNextAttack());
            }
        }
    }

    public virtual void Stalk(GameObject other)
    {        
        if (!CheckAttackRange(other))
        {
            isStopped = false;
            transform.position = Vector3.MoveTowards(transform.position, other.transform.position, moveSpeed * Time.deltaTime);
            animator.SetBool("IsMoving", true);
            StartCoroutine(TimeBetweenStalkTimer());
        }
    }

    IEnumerator TimeUntilNextStalkTimer()
    {
        yield return new WaitForSeconds(timeUntilNextStalk);
        canStalk = true;
    }

    IEnumerator TimeBetweenStalkTimer()
    {
        yield return new WaitForSeconds(timeBetweenStalk);
        canStalk = false;
        animator.SetBool("IsMoving", false);
        isStopped = true;
        StartCoroutine(TimeUntilNextStalkTimer());
    }

    public virtual void TakeDamage(float damage)
    {
        health -= damage;
    }

    public virtual void Attack(GameObject other)
    {
        //Debug.Log("Attacking: " + gameObject.name);
        other.GetComponent<PlayerController>().TakeDamage(damage);
        animator.SetBool("IsAttacking", true);
        StartCoroutine(AttackAnimTimer());
        canAttack = false;
    }

    IEnumerator AttackAnimTimer()
    {
        yield return new WaitForSeconds(timeUntilAttackFinished);
        animator.SetBool("IsAttacking", false);
    }

    IEnumerator TimeUntilNextAttack()
    {
        yield return new WaitForSeconds(timeUntilNextAttack);
        canAttack = true;
    }

    public virtual void Death()
    {
        audioManager.Play("explosionDeath");
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

    public virtual bool CheckAttackRange(GameObject other)
    {
        // direction = heading / distance
        Vector2 heading = other.transform.position - transform.position;
        float distance = heading.magnitude;

        if (distance < attackRange)
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
        float chance = this.dropChance;
        float probability = Random.value;
        if (probability <= chance)
        {
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
            if (enemy != null)
            {
                Enemy enemyLogic = enemy.GetComponent<Enemy>();
                enemyLogic.IncrementDropChance();
            }
            
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
