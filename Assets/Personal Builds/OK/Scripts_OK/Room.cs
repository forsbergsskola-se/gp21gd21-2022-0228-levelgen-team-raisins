using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour{
    [SerializeField] List<GameObject> connections; //Reference door scripts


    void Awake(){


        foreach (var connection in connections){

        }
    }
}
