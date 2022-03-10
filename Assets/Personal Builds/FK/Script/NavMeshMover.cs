using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class NavMeshMover : MonoBehaviour{
    [SerializeField] PositionSO playerTransform;
    [SerializeField]float navmeshUpdateRange = 10f;
    [SerializeField] GameEventSO onMoveNavMesh;


    void Start(){
        MoveNavMesh();
    }

    void Update(){
        if (Vector3.Distance(playerTransform.position, this.transform.position) > navmeshUpdateRange){
            MoveNavMesh();
        }
    }

    void MoveNavMesh(){
        this.transform.position = new Vector3(playerTransform.position.x, 0, playerTransform.position.z);
        onMoveNavMesh?.Invoke();
    }
}
