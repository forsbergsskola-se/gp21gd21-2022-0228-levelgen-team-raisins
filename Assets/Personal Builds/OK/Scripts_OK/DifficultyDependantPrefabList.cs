using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Prefab List", menuName = "Lists/Difficulty Dependant Prefab List")]
public class DifficultyDependantPrefabList : ScriptableObject{
    [Tooltip("Inserted lists will get combined in a new list")][SerializeField] List<PrefabListSO> prefabLists;

    [Tooltip("This gets set automatically, disregard.")]public List<GameObject>combinedPrefabList;

    void OnEnable(){
        if (prefabLists != null){
            CombinePrefabLists();
        }

    }

    void OnDisable(){
        combinedPrefabList = default;
    }


    void CombinePrefabLists(){
        foreach (var prefabList in prefabLists){
            for (var i = 0; i < prefabList.prefabs.Count; i++){
                combinedPrefabList.Add(prefabList.prefabs[i]);
            }
        }
    }
}
