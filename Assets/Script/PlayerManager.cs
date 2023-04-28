using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] Grass grassPrefab;

    [SerializeField] Road roadPrefab;

    [SerializeField] int initialGrassCount = 5;

    [SerializeField] int horizontalSize;

    private void Start()
    {
        for(int i =0; i < 5; i++)
        {
            var go = Instantiate(original: grassPrefab);
            grass.transform.localPosition = new Vector3(x: 0, y: 0, z: 0);
            grass.generate(size: horizontalSize);
        }
    }


}
