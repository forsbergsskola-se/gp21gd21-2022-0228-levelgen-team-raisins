using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshBuilder : MonoBehaviour{
    [SerializeField] NavMeshSurface navMeshSurface;
    public List<Room> Rooms;
    int oldCount;

    void Update(){
        if (Rooms.Count > oldCount){
            navMeshSurface.BuildNavMesh();
            oldCount = Rooms.Count;
        }
    }
}
