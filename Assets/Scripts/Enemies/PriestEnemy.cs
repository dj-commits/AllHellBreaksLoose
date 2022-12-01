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
    float timeUntilSummonAnimFinished;
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

    public override void Death()
    {
        base.Death();
        uiManager.ActivateVictoryScreen();
        
    }

    public void Summon(GameObject summon)
    {
        float randomXSummonFloat = Random.Range(-1, summonRange);
        float randomYSummonFloat = Random.Range(-1, summonRange);
        Vector3 summonSpawnPos = new Vector3(transform.position.x + randomXSummonFloat, transform.position.y + randomYSummonFloat, transform.position.z);
        animator.SetBool("IsSummoning", true);
        GameObject summonClone = Instantiate(summon, summonSpawnPos, Quaternion.identity);
        StartCoroutine(SummonIntervalTimer());
    }
    IEnumerator SummonAnimTimer()
    {
        yield return new WaitForSeconds(timeUntilSummonAnimFinished);
    }
    IEnumerator SummonIntervalTimer()
    {
        yield return new WaitForSeconds(timeUntilNextSummon);
        animator.SetBool("IsSummoning", false);
        canSummon = true;
        
    }

    
}
