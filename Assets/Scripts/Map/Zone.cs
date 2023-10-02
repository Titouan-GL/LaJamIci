using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Zone : ScriptableObject
{
    public Vector2Int[] emptyTiles;
    public Vector2Int[] specialObjectsLocations;
    public GameObject[] specialObjectsInstances;

}
