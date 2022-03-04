using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//TODO:REMOVE used for debug
public class RoomValidator : MonoBehaviour{
    internal UnityEvent<bool> RoomValidationEvent;

    void OnCollisionEnter(Collision other){
        if (other.transform.GetComponent<RoomValidator>()){
            RoomValidationEvent.Invoke(false);
            Debug.Log("OMFG COLLISION DETECT");
        }
    }
}
