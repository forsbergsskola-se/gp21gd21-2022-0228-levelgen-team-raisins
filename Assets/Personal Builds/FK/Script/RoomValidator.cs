using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//TODO:REMOVE used for debug
public class RoomValidator : MonoBehaviour{
    [SerializeField] Room room;

    public bool isColliding;

    void OnTriggerEnter(Collider other){
        Debug.Log(name +"Colliding with " + other);
        room.IsValidRoom = false;
        isColliding = true;
    }
}
