using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionUpdater : MonoBehaviour{
    [SerializeField] PositionSO positionSo;

    void Update(){
        positionSo.position = transform.position;
    }
}
