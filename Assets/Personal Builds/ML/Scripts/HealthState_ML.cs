using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthState_ML : MonoBehaviour
{
    [SerializeField] public int HitPoints = 32;
    public event Action hitPointsDepleted;

    // public subscribable event to be invoked when HP has been replenished
    public event Action hitPointsReplenished;

    void OnEnable()
    {
      //  HitPoints.OnValueChanged += HitPointsChanged;
    }

    void OnDisable()
    {
      //  HitPoints.OnValueChanged -= HitPointsChanged;
    }

    void HitPointsChanged(int previousValue, int newValue)
    {
        if (previousValue > 0 && newValue <= 0)
        {
            // newly reached 0 HP
            hitPointsDepleted?.Invoke();
        }
        else if (previousValue <= 0 && newValue > 0)
        {
            // newly revived
            hitPointsReplenished?.Invoke();
        }
    }
}
