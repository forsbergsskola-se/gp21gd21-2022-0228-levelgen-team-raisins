using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NavigationBaker : MonoBehaviour{
    public NavMeshSurface[] surfaces;
    public Dungeon dungeon;


    public void AddNavigationSurfaces(){
        for (int i = 0; i < dungeon.Rooms.Count; i++){
            {
                surfaces[i] = dungeon.Rooms[i].GetComponent<NavMeshSurface>();
            }
        }
    }
}
