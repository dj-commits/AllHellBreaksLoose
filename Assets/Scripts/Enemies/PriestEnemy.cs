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

    [SerializeField]
    float summonEnemyCount;

    AudioManager audioManager;

    bool deathSound;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        StartCoroutine(SummonIntervalTimer());
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        deathSound = false;
    }

    // Update is called once per frame
    public override void Update()
    {
        if (isAlive)
        {
            base.Update();
            if (canSummon)
            {
                animator.SetBool("isSummoning", true);
                StartCoroutine(SummonAnimTimer());
                for (int i = 0; i < summonEnemyCount; i++)
                {
                    Summon(skullEnemy);
                }
                canSummon = false;
            }
        }
    }

    public override void Death()
    {
        //base.Death();
        if (!deathSound) audioManager.Play("explosionDeath");
        deathSound = true;
        isAlive = false;
        animator.SetBool("isDead", true);
        uiManager.ActivateVictoryScreen();
        
    }

    IEnumerator DeathAnimTimer()
    {
        yield return new WaitForSeconds(timeUntilSummonAnimFinished);
        Destroy(gameObject);
    }

    public void Summon(GameObject summon)
    {
        float randomXSummonFloat = Random.Range(-1, summonRange);
        float randomYSummonFloat = Random.Range(-1, summonRange);
        Vector3 summonSpawnPos = new Vector3(transform.position.x + randomXSummonFloat, transform.position.y + randomYSummonFloat, transform.position.z);
        GameObject summonClone = Instantiate(summon, summonSpawnPos, Quaternion.identity);
        StartCoroutine(SummonIntervalTimer());
    }
    IEnumerator SummonAnimTimer()
    {
        yield return new WaitForSeconds(timeUntilSummonAnimFinished);
        animator.SetBool("isSummoning", false);
    }
    IEnumerator SummonIntervalTimer()
    {
        yield return new WaitForSeconds(timeUntilNextSummon);
        canSummon = true;
    }

    
}
