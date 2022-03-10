using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Range SO ", menuName = "Values/Float Range")]
public class RangesSO : ScriptableObject
{
    public FloatSO roomSpawnRange;
    public FloatSO roomDespawnRange;
    public FloatSO updatePosThreshold;
}
