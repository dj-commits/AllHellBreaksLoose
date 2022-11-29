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
    float summonRange;
    [SerializeField]
    float timeUntilNextSummon;
    [SerializeField]
    bool canSummon;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        StartCoroutine(SummonIntervalTimer());
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (canSummon)
        {
            Summon(skullEnemy);
            canSummon = false;
        }
    }

    public void Summon(GameObject summon)
    {
        float randomSummonFloat = Random.Range(-1, summonRange);
        Vector3 summonSpawnPos = this.transform.position * randomSummonFloat;
        animator.SetBool("IsSummoning", true);
        GameObject summonClone = Instantiate(summon, summonSpawnPos, Quaternion.identity);
        StartCoroutine(SummonIntervalTimer());
    }

    IEnumerator SummonIntervalTimer()
    {
        yield return new WaitForSeconds(timeUntilNextSummon);
        canSummon = true;
        
    }
    

    
}
