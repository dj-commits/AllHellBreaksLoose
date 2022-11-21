using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullEnemy : Enemy
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        GameObject shieldPowerup = Resources.Load("Prefabs/Powerups/shieldPowerup", typeof(GameObject)) as GameObject;
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
}
