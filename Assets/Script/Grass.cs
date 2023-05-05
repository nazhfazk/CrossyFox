using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : Terrain
{
    [SerializeField] List<GameObject> treePrefabList;
    [SerializeField, Range(min: 0, max: 1)] float treeProbability;

    public void SetTreePercentage(float newProbability)
    {
       this. treeProbability = Mathf.Clamp01(newProbability);
    }

    public override void Generate(int size)
    {
        base.Generate(size: size);

        var limit = Mathf.FloorToInt(f: (float)size / 2);
        var treeCount = Mathf.FloorToInt(f:(float)size * treeProbability);

        //membuat daftar posisi yang masih kosong
        List<int> emptyPosition = new List<int>();
        for (int i = -limit; i <= limit; i++)
        {
            emptyPosition.Add(item: i);
        }

        for(int i = 0; i < treeCount; i++)
        {
            //memilih posisi kosong secara random
            var randomIndex = Random.Range(minInclusive: 0, maxExclusive: emptyPosition.Count);
            var pos = emptyPosition[index:  randomIndex];

            //posisi yang terpilih hapus dari daftar posisi kosong
            emptyPosition.RemoveAt(index: randomIndex);

            SpawnRandomTree(xPos: pos) ;
              
        }

        // selalu ada pohon di ujung
        SpawnRandomTree(xPos: -limit - 1);
        SpawnRandomTree(xPos: limit + 1);

    }

    private void SpawnRandomTree(int xPos)
    {
        //pilih prefab pohon secara random
        var randomIndex = Random.Range(minInclusive: 0, maxExclusive: treePrefabList.Count);
        var prefab = treePrefabList[index: randomIndex];

        //set pohon ke posisi yang terpilih
        var tree = Instantiate(
            original: prefab, 
            position: new Vector3(xPos, 0, this.transform.position.z), 
            rotation: Quaternion.identity, 
            parent: transform);

    }

}
