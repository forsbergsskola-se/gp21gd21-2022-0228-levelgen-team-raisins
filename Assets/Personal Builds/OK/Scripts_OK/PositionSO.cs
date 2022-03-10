using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Position", menuName = "Essentials/Transform/Position")]
public class PositionSO : ScriptableObject{
    public Vector3 position;
    public Vector3 savedPosition;
    public void SavePosition(){
        savedPosition = position;
    }

    void OnEnable(){
        savedPosition = position;
    }

    void OnDisable(){
        savedPosition = position;
    }
}
