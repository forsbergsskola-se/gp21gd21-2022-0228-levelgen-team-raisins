using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Float Value", menuName = "Values/Float")]
public class FloatSO : ScriptableObject{
    public float value;
    void OnEnable(){
        value = 0;
    }
}
