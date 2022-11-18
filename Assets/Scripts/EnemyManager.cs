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
    private int enemyCount;

    private void Start()
    {
        enemyObjects = new List<GameObject>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        impEnemy = Resources.Load("Prefabs/Enemies/impEnemy", typeof(GameObject)) as GameObject;
        skullEnemy = Resources.Load("Prefabs/Enemies/skullEnemy", typeof(GameObject)) as GameObject;
        enemyObjects.Add(impEnemy);
        enemyObjects.Add(skullEnemy);
        enemyCount = 0;
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

    public GameObject getEnemyObject(string name)
    {
        foreach (GameObject e in enemyObjects)
        {
            if (name.Contains(e.name))
            {
                Debug.Log("IT IS!");
                return e;
            }
        }
        return null;
    }
}
