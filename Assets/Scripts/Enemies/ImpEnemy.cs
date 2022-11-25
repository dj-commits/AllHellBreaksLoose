using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpEnemy : Enemy
{

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
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        
            
    }

    public override void Death()
    {
        base.Death();

    }
    
}
