using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour{
    [SerializeField] GameEventSO gameEvent;
    [SerializeField] UnityEvent unityEvent;

    void Awake() => gameEvent.Register(this);
    void OnDestroy() => gameEvent.Deregister(this);
    public void RaiseEvent() => unityEvent.Invoke();
}
