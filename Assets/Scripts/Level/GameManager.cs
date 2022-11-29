using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Door bossDoor;
    EnemyManager em;
    PlayerController playerController;
    BoxCollider2D bossSpawnCollider;

    AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        bossSpawnCollider = GameObject.Find("BossSpawnTrigger").GetComponent<BoxCollider2D>();
        bossDoor = GameObject.Find("BossDoor").GetComponent<Door>();
        em = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    public void OpenBossDoor()
    {
        audioManager.Play("doorOpen");
        audioManager.Stop("levelMusic");
        audioManager.Play("bossMusic");
        bossDoor.SetDoorLockStatus(false);
    }

    public void BossSpawnTrigger()
    {
        em.SpawnBoss();
    }


}
