using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeStop : MonoBehaviour
{

    //static HashSet<Vector3> positionSet = new HashSet<Vector3>();

    //public static HashSet<Vector3> AllPositions
    //{
    //    get => new HashSet<Vector3>(collection: positionSet);
    //}


    //private void OnEnable()
    //{
    //    positionSet.Add(this.transform.position);

    //}

    //private void OnDisable()
    //{
    //    positionSet.Remove(this.transform.position);
    //}

    static HashSet<Vector3> positionSet = new HashSet<Vector3>();

    public static HashSet<Vector3> AllPositions
    {
        get => new HashSet<Vector3>(collection: positionSet);

    }

    private void OnEnable()
    {
        positionSet.Add(this.transform.position);
    }

    private void OnDisable()
    {
        positionSet.Remove(this.transform.position);
    }

}

