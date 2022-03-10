using System.Collections;
using System.Collections.Generic;
using Unity.Multiplayer.Samples.BossRoom;
using Unity.Netcode;
using UnityEngine;

public class CharacterState_ML : MonoBehaviour
{
    public MovementStatus MovementStatus { get; } = new MovementStatus();

    public bool IsHealthy { get; }

    private HealthState_ML m_HealthStateMl;

    [SerializeField]
    CharacterClassContainer m_CharacterClassContainer;

    public CharacterClass CharacterClass => m_CharacterClassContainer.CharacterClass;

    public CharacterTypeEnum CharacterType => m_CharacterClassContainer.CharacterClass.CharacterType;


    public bool IsValidTarget => LifeState != LifeState.Dead;

    private LifeState lifeState;

    public HealthState_ML HealthState
    {
        get
        {
            return m_HealthStateMl;
        }
    }


    public LifeState LifeState
    {
        get => lifeState;
        set => lifeState = value;
    }
}
