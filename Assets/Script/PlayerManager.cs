using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    [SerializeField] Movement box;

    [SerializeField]  List<Terrain> terrainList;
    
    [SerializeField] int initialGrassCount = 5;

    [SerializeField] int horizontalSize;

    [SerializeField] int backViewDistance = -4;

    [SerializeField] int forwardViewDistance = 15;

    [SerializeField, Range(0,1)] float treeProbability;

    Dictionary<int, Terrain> activeTerrain = new Dictionary<int, Terrain>(capacity: 20);

   [SerializeField] private int travelDistance;

    private void Start()
    {

        //spawn terrain pos -4 --- 4
        for(int zPos = backViewDistance; zPos < initialGrassCount; zPos++)
        {
            var terrain = Instantiate(original: terrainList[0] );

            terrain.transform.localPosition = new Vector3(x: 0, y: 0, z: zPos);

            if(terrain is Grass grass)
                grass.SetTreePercentage(newProbability: zPos < -1 ? 1 : 0);

            terrain.Generate(size: horizontalSize);

            activeTerrain[zPos] = terrain;
        }

        // 
        for (int zPos = initialGrassCount; zPos < forwardViewDistance; zPos++)
        {

            SpawnRandomTerrain(zPos);
           
        }
    }

    private Terrain SpawnRandomTerrain(int zPos)
    {
        Terrain terraincheck = null;
        int randomIndex;
         
        for (int z = -1; z >= -3; z--)
        {
            var checkPos = zPos + z;

            if (terraincheck == null)
            { 
            terraincheck = activeTerrain[checkPos];

            continue;

            }
            else if (terraincheck.GetType() != activeTerrain[checkPos].GetType())
            {
                randomIndex = Random.Range(0, terrainList.Count);

                return SpawnTerrain(terrainList[randomIndex], zPos);
            }
            else
            {
                continue;
            }

        }

        var candidateTerrain = new List<Terrain>(terrainList);
            for (int i =0; i < candidateTerrain.Count; i++)
        {
            if(terraincheck.GetType() == candidateTerrain[i].GetType())
            {
                candidateTerrain.Remove(candidateTerrain[i]);
                break;
            }
        }

        randomIndex = Random.Range(0, candidateTerrain.Count);

        return SpawnTerrain(candidateTerrain[randomIndex], zPos);

    }

    public Terrain SpawnTerrain(Terrain terrain, int zPos)
    {
        terrain = Instantiate(terrain);

        terrain.transform.localPosition = new Vector3(x: 0, y: 0, z: zPos);

        terrain.Generate(size: horizontalSize);

        activeTerrain[zPos] = terrain;

        return terrain;
    }

    public void UpdateTravelDistance(Vector3 targetPosition)
    {
        if (box.transform.position.z > travelDistance)
        {
            travelDistance = Mathf.CeilToInt(targetPosition.z);
            UpdateTerrain();
        }
    }

    public void UpdateTerrain()
    {
        var destroyPos = travelDistance - 1 + backViewDistance;

        Destroy(activeTerrain[destroyPos].gameObject);

        activeTerrain.Remove(destroyPos);

        var spawnPosition = travelDistance - 1 + forwardViewDistance;

        SpawnRandomTerrain(spawnPosition);
    }

}
