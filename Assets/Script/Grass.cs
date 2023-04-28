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
        var treecount = Mathf.FloorToInt(f:(float)size * treeProbability);

        //membuat daftar posisi yang masih kosong
        List<int> emptyPosition = new List<int>();
        for (int i = -limit; i <= limit; i++)
        {
            emptyPosition.Add(item: i);
        }

        for(int i = 0; i < treecount; i++)
        {
            //memilih posisi kosong secara random
            var randomIndex = emptyPosition[index: Random.Range(minInclusive: 0, maxExclusive: emptyPosition.Count - 1)];
            var pos = emptyPosition[index:  randomIndex];

            //posisi yang terpilih hapus dari daftar posisi kosong
            emptyPosition.RemoveAt(index: randomIndex);

            //pilih prefab pohon secara random
            randomIndex = Random.Range(minInclusive: 0, maxExclusive: treePrefabList.Count);
            var prefab = treePrefabList[index: randomIndex];

            //set pohon ke posisi yang terpilih
            var tree = Instantiate(original: prefab, parent: transform);
            tree.transform.localPosition = new Vector3(x: pos, y: 0, z: 0);
            
              
        }

        // selalu ada pohon di ujung
        var leftBoundaryTree = Instantiate(treePrefabList[0], transform);
    }

}
