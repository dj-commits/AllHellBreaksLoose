using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            Cursor.visible = true;
        }
    }

    public void BossSpawnTrigger()
    {
        audioManager.Stop("levelMusic");
        
        audioManager.getSoundByName("bossMusic").source.pitch = 0.8f;
        audioManager.Play("bossMusic");
        em.SpawnBoss();
    }


}
