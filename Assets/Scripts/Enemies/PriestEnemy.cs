using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriestEnemy : Enemy
{
    [SerializeField]
    GameObject skullEnemy;
    [SerializeField]
    List<Vector3> summonSpawnPos;
    [SerializeField]
    SpriteRenderer spawnArea;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        spawnArea = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void Summon(GameObject summon)
    {
        summonSpawnPos = new List<Vector3>();
        animator.SetBool("IsSummoning", true);
        //GameObject bulletClone = Instantiate(bullet, this.transform.position, Quaternion.identity);
        //StartCoroutine(ShotAnimTimer(bulletClone, direction));
    }
    

    
}
