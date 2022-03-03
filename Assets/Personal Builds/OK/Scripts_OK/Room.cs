using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum RoomType{
    LargeRoom,
    MediumRoom,
    SmallRoom,
    Corridor,

}

public class Room : MonoBehaviour{
    [SerializeField] List<GameObject> connections; //Reference door scripts


    void Awake(){


        foreach (var connection in connections){

        }
    }
}
