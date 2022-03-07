using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTimer : MonoBehaviour
{

   [HideInInspector] public float remainingTime = 5f;
   [HideInInspector] public bool outOfTime = false;

    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else
        {
            outOfTime = true;
        }


    }
}
