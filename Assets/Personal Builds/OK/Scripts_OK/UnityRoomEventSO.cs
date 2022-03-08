using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Unity Room Event", menuName = "Events/Unity Events/Unity Room Event")]
public class UnityRoomEventSO : ScriptableObject{

    public UnityEvent<Room> roomEvent;

    void OnEnable(){
        if (roomEvent == null){
            roomEvent = new UnityEvent<Room>();
        }
    }
}
