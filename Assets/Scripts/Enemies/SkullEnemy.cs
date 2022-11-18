using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullEnemy : Enemy
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        GameObject otherPowerUp = Resources.Load("Prefabs/Powerups/otherPowerup", typeof(GameObject)) as GameObject;
        lootTable.Add(otherPowerUp);

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
