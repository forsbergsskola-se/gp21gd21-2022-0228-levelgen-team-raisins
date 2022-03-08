using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//TODO:REMOVE used for debug
public class RoomValidator : MonoBehaviour{
    [SerializeField] Room room;

    void OnCollisionEnter(Collision collision){
        if (collision.transform.CompareTag("Validation_Collider")){
            room.IsValidRoom = false;
        }
    }
}
