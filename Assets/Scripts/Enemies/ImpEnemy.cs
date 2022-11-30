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
    float shootAttackRange;



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

    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void Death()
    {
        base.Death();

    }

    public override void Attack(GameObject other)
    {
        if (CheckForShootingRange(other))
        {
            Shoot(other);
        }else
        {
            base.Attack(other);
        }


    }

    public virtual void Shoot(GameObject other)
    {
        Vector2 direction = other.transform.position - transform.position;
        animator.SetBool("IsShooting", true);
        GameObject bulletClone = Instantiate(bullet, this.transform.position, Quaternion.identity);
        StartCoroutine(ShotAnimTimer(bulletClone, direction));
    }

    IEnumerator ShotAnimTimer(GameObject bullet, Vector2 direction)
    {
        yield return new WaitForSeconds(timeUntilShotAnimFinished);
        animator.SetBool("IsShooting", false);
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bullet.GetComponent<ImpShot>().GetBulletSpeed();
    }

    public bool CheckForShootingRange(GameObject other)
    {
        // direction = heading / distance
        Vector2 heading = other.transform.position - transform.position;
        float distance = heading.magnitude;

        if (distance < shootAttackRange)
        {
            return true;
        }
        return false;
    }

}
