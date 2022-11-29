using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Door bossDoor;
    EnemyManager em;
    PlayerController playerController;
    BoxCollider2D bossSpawnCollider;



    // Start is called before the first frame update
    void Start()
    {
        bossSpawnCollider = GameObject.Find("BossSpawnTrigger").GetComponent<BoxCollider2D>();
        bossDoor = GameObject.Find("BossDoor").GetComponent<Door>();
        em = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public void OpenBossDoor()
    {
        bossDoor.SetDoorLockStatus(false);
    }

    public void BossSpawnTrigger()
    {
        em.SpawnBoss();
    }


}
