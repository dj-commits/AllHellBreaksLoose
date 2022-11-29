using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyManager : MonoBehaviour
{
    
    List<GameObject> enemyObjects;
    public List<GameObject> bossEnemyObjects;
    Camera cam;
    [SerializeField] int maxEnemies;
    Grid grid;
    Component[] tilemaps;
    List<Vector3> spawnPositions;
    private int enemyCount;
    Transform bossSpawnPos;
    GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        bossSpawnPos = GameObject.Find("BossSpawnLocation").GetComponent<Transform>();
        enemyObjects = new List<GameObject>();
        bossEnemyObjects = new List<GameObject>();
        grid = GameObject.Find("Grid").GetComponent<Grid>();
        tilemaps = grid.GetComponentsInChildren<Tilemap>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        GameObject impEnemy = Resources.Load("Prefabs/Enemies/imp/impEnemy", typeof(GameObject)) as GameObject;
        GameObject skullEnemy = Resources.Load("Prefabs/Enemies/skull/skullEnemy", typeof(GameObject)) as GameObject;
        GameObject bossEnemy = Resources.Load("Prefabs/Enemies/priest/priestEnemy", typeof(GameObject)) as GameObject;
        bossEnemyObjects.Add(bossEnemy);
        enemyObjects.Add(impEnemy);
        enemyObjects.Add(skullEnemy);
        enemyCount = 0;
        spawnPositions = InitSpawnPositions();
        while (enemyCount < maxEnemies)
        {
            SpawnEnemy();

        }
    }

    private void Update()
    {
        if(enemyCount <= 0)
        {
            gameManager.OpenBossDoor();
        }
    }

    private List<Vector3> InitSpawnPositions()
    {
        List<Vector3> spawnPositions = new List<Vector3>();

        foreach (Tilemap tilemap in tilemaps)
        {
            if (tilemap.name == "Floor")
            {
                foreach (Vector3Int coordinates in tilemap.cellBounds.allPositionsWithin)
                {
                    if (tilemap.HasTile(coordinates))
                    {
                        spawnPositions.Add(tilemap.CellToWorld(coordinates));
                    }
                }
            }
        }

        return spawnPositions;
    }

    private void SpawnEnemy()
    {   

        int randomIndex = Random.Range(0, enemyObjects.Count);
        int spawnPosIndex = Random.Range(0, spawnPositions.Count);
        Vector3 spawnPos = spawnPositions[spawnPosIndex];
        Instantiate(enemyObjects[randomIndex], spawnPos, Quaternion.identity);
        enemyCount++;
    }

    public void SpawnBoss()
    {
        Instantiate(bossEnemyObjects[0], bossSpawnPos.position, Quaternion.identity);
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
                return e;
            }
        }
        return null;
    }

    public int GetEnemyCount()
    {
        return enemyCount;
    }
}
