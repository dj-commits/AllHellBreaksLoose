using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpEnemy : Enemy
{
    [SerializeField]
    GameObject bullet;
    [SerializeField]
    float timeUntilShotAnimFinished;
    [SerializeField]
    float timeUntilNextShot;
    [SerializeField]
    float shootAttackRange;
    [SerializeField]
    bool canShoot;



    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        // Why load the loot table in the subclass and not base class?
        //GameObject dashPowerUp = Resources.Load("Prefabs/Powerups/dashPowerup", typeof(GameObject)) as GameObject;
        GameObject gunPowerUp = Resources.Load("Prefabs/Powerups/gunPowerup", typeof(GameObject)) as GameObject;
        GameObject shieldPowerup = Resources.Load("Prefabs/Powerups/shieldPowerup", typeof(GameObject)) as GameObject;
        //lootTable.Add(dashPowerUp);
        lootTable.Add(gunPowerUp);
        lootTable.Add(shieldPowerup);
        canShoot = true;

    }

    // Update is called once per frame
    public override void Update()
    {
        if (CheckForShootingRange(player) && canShoot && isStopped)
        {
            Shoot(player);
            canShoot = false;
        }
        base.Update();


    }

    public override void Death()
    {
        base.Death();

    }

    public override void Attack(GameObject other)
    {
        base.Attack(other);


    }

    public virtual void Shoot(GameObject other)
    {
        Vector2 direction = other.transform.position - transform.position;
        animator.SetBool("IsShooting", true);
        GameObject bulletClone = Instantiate(bullet, this.transform.position, Quaternion.identity);
        float bulletCloneSpeed = bulletClone.GetComponent<ImpShot>().GetBulletSpeed();
        StartCoroutine(ShotAnimTimer(bulletClone, direction, bulletCloneSpeed));
    }

    IEnumerator ShotAnimTimer(GameObject bullet, Vector2 direction, float speed)
    {
        yield return new WaitForSeconds(timeUntilShotAnimFinished);
        animator.SetBool("IsShooting", false);
        bullet.GetComponent<Rigidbody2D>().velocity = direction * speed;
        StartCoroutine(TimeUntilNextShotTimer());
        
    }

    IEnumerator TimeUntilNextShotTimer()
    {
        yield return new WaitForSeconds(timeUntilNextShot);
        canShoot = true;
    }

    public bool CheckForShootingRange(GameObject other)
    {
        // direction = heading / distance
        Vector2 heading = other.transform.position - transform.position;
        float distance = heading.magnitude;

        if (distance <= shootAttackRange && distance >= attackRange)
        {
            return true;

        }
        return false;
    }

}
