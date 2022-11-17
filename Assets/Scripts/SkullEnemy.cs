using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullEnemy : Enemy
{
    private GameObject otherPowerUp;
    private List<GameObject> skullLootTable;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        this.health = 5;
        this.damage = 5;
        this.moveSpeed = 3;
        skullLootTable = new List<GameObject>();
        otherPowerUp = Resources.Load("Prefabs/Powerups/otherPowerup", typeof(GameObject)) as GameObject;
        skullLootTable.Add(otherPowerUp);

    }

    // Update is called once per frame
    public override void Update()
    {
        base.StalkPlayer();
        base.CheckForDeath();
        if (!isAlive)
        {
            base.LootDrop(skullLootTable[0]);
        }
    }
}
