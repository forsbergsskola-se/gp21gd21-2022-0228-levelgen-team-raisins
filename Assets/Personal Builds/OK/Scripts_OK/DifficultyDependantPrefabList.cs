using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Prefab List", menuName = "Lists/Difficulty Dependant Prefab List")]
public class DifficultyDependantPrefabList : ScriptableObject{
    [Tooltip("Inserted lists will get combined in a new list")][SerializeField] public List<PrefabListSO> prefabLists;

    [Tooltip("This gets set automatically, disregard.")]public List<GameObject>combinedPrefabList;

    public virtual void OnEnable(){
        combinedPrefabList = new List<GameObject>();
        if (prefabLists != null){
            CombinePrefabLists();
        }

    }

    public virtual void OnDisable(){
        combinedPrefabList = new List<GameObject>();
    }

    void CombinePrefabLists(){
        foreach (var prefabList in prefabLists){
            for (var i = 0; i < prefabList.prefabs.Count; i++){
                combinedPrefabList.Add(prefabList.prefabs[i]);
            }
        }
    }
}
