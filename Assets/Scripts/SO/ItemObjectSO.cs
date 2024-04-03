using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ItemObjectSO : ScriptableObject
{
    public string objectName;
    public Transform prefab;
    public Transform onPlacementPrefab;
    public Sprite sprite;
}
