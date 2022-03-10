using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToPool : MonoBehaviour
{
    ObjectPool _objectPool;

    void Start()
    {
        _objectPool = FindObjectOfType<ObjectPool>();
       // StartCoroutine(ReturnAfterPeriod());
    }

    void OnDisable()
    {
        if (_objectPool != null)
        {
            _objectPool.ReturnGameObject(this.gameObject);
        }
    }

    IEnumerator ReturnAfterPeriod()
    {
        yield return new WaitForSeconds(2);
        _objectPool.ReturnGameObject(this.gameObject);
    }
}
