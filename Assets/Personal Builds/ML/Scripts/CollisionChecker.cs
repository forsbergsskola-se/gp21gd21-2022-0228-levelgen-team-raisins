using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    // Start is called before the first frame update
    private SphereCollider useCollider;


    void Start()
    {
        useCollider = GetComponentInChildren<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
