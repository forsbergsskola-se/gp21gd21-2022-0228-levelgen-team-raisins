using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Room list", menuName = "List/Room list")]
public class RoomListSO : ScriptableObject{
    public List<Room> rooms;
    public Room startRoom;

    void OnEnable(){
        rooms = new List<Room>();
        rooms.Add(startRoom);
    }

    void OnDisable(){
        rooms = new List<Room>();
    }
}
