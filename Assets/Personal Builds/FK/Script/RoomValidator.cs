using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//TODO:REMOVE used for debug
public class RoomValidator : MonoBehaviour{

    public Room room;
    public bool isColliding;

    void OnTriggerEnter(Collider other){
        Debug.Log("Collision:" + other.name);
        if (other.CompareTag("Validation_Collider")){
            room.IsValidRoom = false;
            isColliding = true;
        }
    }


    // void OnCollisionEnter(Collision collision){
    //     Debug.Log("Collision:" + collision.transform.name);
    //     if (collision.transform.CompareTag("Validation_Collider")){
    //         room.IsValidRoom = false;
    //         isColliding = true;
    //     }
    // }
}
