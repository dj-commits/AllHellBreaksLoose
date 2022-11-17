using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpEnemy : Enemy
{
    private GameObject dashPowerUp;
    private List<GameObject> impLootTable;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        this.health = 20;
        this.damage = 15;
        this.moveSpeed = 1;
        impLootTable = new List<GameObject>();
        dashPowerUp = Resources.Load("Prefabs/Powerups/dashPowerup", typeof(GameObject)) as GameObject;
        impLootTable.Add(dashPowerUp);

    }

    // Update is called once per frame
    public override void Update()
    {
        base.StalkPlayer();
        base.CheckForDeath();
        if (!isAlive)
        {
            base.LootDrop(impLootTable[0]);
        }
    }
}
