using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyManager : MonoBehaviour
{
    
    List<GameObject> enemyObjects;
    Camera cam;
    [SerializeField] int maxEnemies;
    Grid grid;
    Component[] tilemaps;
    List<Vector3> spawnPositions;
    private int enemyCount;

    private void Start()
    {
        enemyObjects = new List<GameObject>();
        grid = GameObject.Find("Grid").GetComponent<Grid>();
        tilemaps = grid.GetComponentsInChildren<Tilemap>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        GameObject impEnemy = Resources.Load("Prefabs/Enemies/imp/impEnemy", typeof(GameObject)) as GameObject;
        GameObject skullEnemy = Resources.Load("Prefabs/Enemies/skull/skullEnemy", typeof(GameObject)) as GameObject;
        enemyObjects.Add(impEnemy);
        enemyObjects.Add(skullEnemy);
        enemyCount = 0;
        spawnPositions = InitSpawnPositions();
    }

    private void Update()
    {
        if (enemyCount < maxEnemies)
        {
            SpawnEnemy();
            
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

        /*while (spawnPositions.Count < 1)
        {
            float xSpawnPos = Random.Range(cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).x, cam.ViewportToWorldPoint(new Vector3(1, 0, 0)).x);
            float ySpawnPos = Random.Range(cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).y, cam.ViewportToWorldPoint(new Vector3(0, 1, 0)).y);
            Vector3 spawnPos = new Vector3(xSpawnPos, ySpawnPos, 0);
            spawnPositions.Add(spawnPos);
        }

        foreach (Vector3 spawnPos in spawnPositions)
        {
            Vector3Int cellPosition = grid.WorldToCell(spawnPos);
            foreach(Tilemap tilemap in tilemaps)
            {
                if (tilemap.name == "Floor")
                {
                    foreach (Vector3Int coordinates in tilemap.cellBounds.allPositionsWithin)
                    {
                        if (cellPosition == coordinates)
                        {
                            
                            
                        }
                    }
                }
                
            }*/  
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
}
