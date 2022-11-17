using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    GameObject impEnemy;
    GameObject skullEnemy;
    GameObject bossEnemy;
    List<GameObject> enemyObjects;
    Camera cam;
    [SerializeField] int maxEnemies;
    private float impDropChance;
    private float skullDropChance;
    private int enemyCount;

    const float DEFAULT_IMP_DROP_CHANCE = .1f;
    const float DEFAULT_SKULL_DROP_CHANCE = .05f;

    private void Start()
    {
        enemyObjects = new List<GameObject>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        impEnemy = Resources.Load("Prefabs/Enemies/impEnemy", typeof(GameObject)) as GameObject;
        skullEnemy = Resources.Load("Prefabs/Enemies/skullEnemy", typeof(GameObject)) as GameObject;
        enemyObjects.Add(impEnemy);
        enemyObjects.Add(skullEnemy);
        enemyCount = 0;
        impDropChance = DEFAULT_IMP_DROP_CHANCE;
        skullDropChance = DEFAULT_SKULL_DROP_CHANCE;
    }

    private void Update()
    {
        if (enemyCount < maxEnemies)
        {
            SpawnEnemy();
            enemyCount++;
        }
    }

    private void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, enemyObjects.Count);
        float xSpawnPos = Random.Range(cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).x, cam.ViewportToWorldPoint(new Vector3(1, 0, 0)).x);
        float ySpawnPos = Random.Range(cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).y, cam.ViewportToWorldPoint(new Vector3(0, 1, 0)).y);
        Vector2 spawnPos = new Vector2(xSpawnPos, ySpawnPos);
        Instantiate(enemyObjects[randomIndex], spawnPos, Quaternion.identity);
    }

    public void KillEnemy()
    {
        enemyCount--;
    }

    public float GetDropChance(GameObject go)
    {
        if (go.name.Contains("imp"))
        {
            return impDropChance;
        } else if (go.name.Contains("skull"))
        {
            return skullDropChance;
        }
        else
        {
            return 1f;
        }
    }

    public void IncrementDropChance(GameObject go, float value)
    {
        if (go.name.Contains("imp"))
        {
            if (impDropChance >= 1)
            {
                ResetDropChance();
            }
            else
            {
                impDropChance += value;
            }

        }
        else if (go.name.Contains("skull"))
        {
            if (skullDropChance >= 1)
            {
                ResetDropChance();
            }
            else
            {
                skullDropChance += value;
            }
        }
    }

    public void ResetDropChance()
    {
        impDropChance = DEFAULT_IMP_DROP_CHANCE;
        skullDropChance = DEFAULT_SKULL_DROP_CHANCE;
    }
}
