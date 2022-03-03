using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Prefab List", menuName = "Lists/Prefab List")]
public class PrefabListSO : ScriptableObject
{
    [SerializeField] public List<GameObject> prefabs;
}
