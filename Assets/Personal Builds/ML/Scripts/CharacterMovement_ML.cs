using System.Collections;
using System.Collections.Generic;
using Unity.Multiplayer.Samples.BossRoom;
using Unity.Multiplayer.Samples.BossRoom.Server;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

public class CharacterMovement_ML : MonoBehaviour
{
    [SerializeField]
    NavMeshAgent m_NavMeshAgent;

    [SerializeField]
    Rigidbody m_Rigidbody;

    [SerializeField]
    private ServerCharacter m_CharLogic;

    [SerializeField]
    NetworkCharacterState m_NetworkCharacterState;

    // when we are in charging and knockback mode, we use these additional variables
    private float m_ForcedSpeed;
    private float m_SpecialModeDurationRemaining;

    // this one is specific to knockback mode
    private Vector3 m_KnockbackVector;

    //unnecessary?
  //  private NavigationSystem m_NavigationSystem;

    private DynamicNavPath m_NavPath;

    private MovementState m_MovementState;



    public void SetMovementTarget(Vector3 position)
    {
        m_MovementState = MovementState.PathFollowing;
        m_NavPath.SetTargetPosition(position);
    }

    public void StartForwardCharge(float speed, float duration)
    {
        m_NavPath.Clear();
        m_MovementState = MovementState.Charging;
        m_ForcedSpeed = speed;
        m_SpecialModeDurationRemaining = duration;
    }

    public void StartKnockback(Vector3 knocker, float speed, float duration)
    {
        m_NavPath.Clear();
        m_MovementState = MovementState.Knockback;
        m_KnockbackVector = transform.position - knocker;
        m_ForcedSpeed = speed;
        m_SpecialModeDurationRemaining = duration;
    }


      public bool IsPerformingForcedMovement()
        {
            return m_MovementState == MovementState.Knockback || m_MovementState == MovementState.Charging;
        }

        /// <summary>
        /// Returns true if the character is actively moving, false otherwise.
        /// </summary>
        /// <returns></returns>
        public bool IsMoving()
        {
            return m_MovementState != MovementState.Idle;
        }

        /// <summary>
        /// Cancels any moves that are currently in progress.
        /// </summary>
        public void CancelMove()
        {
            m_NavPath.Clear();
            m_MovementState = MovementState.Idle;
        }

        /// <summary>
        /// Instantly moves the character to a new position. NOTE: this cancels any active movement operation!
        /// This does not notify the client that the movement occurred due to teleportation, so that needs to
        /// happen in some other way, such as with the custom action visualization in DashAttackActionFX. (Without
        /// this, the clients will animate the character moving to the new destination spot, rather than instantly
        /// appearing in the new spot.)
        /// </summary>
        /// <param name="newPosition">new coordinates the character should be at</param>
        public void Teleport(Vector3 newPosition)
        {
            CancelMove();
            if (!m_NavMeshAgent.Warp(newPosition))
            {
                // warping failed! We're off the navmesh somehow. Weird... but we can still teleport
                Debug.LogWarning($"NavMeshAgent.Warp({newPosition}) failed!", gameObject);
                transform.position = newPosition;
            }

            m_Rigidbody.position = transform.position;
            m_Rigidbody.rotation = transform.rotation;
        }

        private void FixedUpdate()
        {
            PerformMovement();

            m_NetworkCharacterState.MovementStatus.Value = GetMovementStatus();
        }


        private void PerformMovement()
        {
            if (m_MovementState == MovementState.Idle)
                return;

            Vector3 movementVector;

            if (m_MovementState == MovementState.Charging)
            {
                // if we're done charging, stop moving
                m_SpecialModeDurationRemaining -= Time.fixedDeltaTime;
                if (m_SpecialModeDurationRemaining <= 0)
                {
                    m_MovementState = MovementState.Idle;
                    return;
                }

                var desiredMovementAmount = m_ForcedSpeed * Time.fixedDeltaTime;
                movementVector = transform.forward * desiredMovementAmount;
            }
            else if (m_MovementState == MovementState.Knockback)
            {
                m_SpecialModeDurationRemaining -= Time.fixedDeltaTime;
                if (m_SpecialModeDurationRemaining <= 0)
                {
                    m_MovementState = MovementState.Idle;
                    return;
                }

                var desiredMovementAmount = m_ForcedSpeed * Time.fixedDeltaTime;
                movementVector = m_KnockbackVector * desiredMovementAmount;
            }
            else
            {
                var desiredMovementAmount = GetBaseMovementSpeed() * Time.fixedDeltaTime;
                movementVector = m_NavPath.MoveAlongPath(desiredMovementAmount);

                // If we didn't move stop moving.
                if (movementVector == Vector3.zero)
                {
                    m_MovementState = MovementState.Idle;
                    return;
                }
            }

            m_NavMeshAgent.Move(movementVector);
            transform.rotation = Quaternion.LookRotation(movementVector);

            // After moving adjust the position of the dynamic rigidbody.
            m_Rigidbody.position = transform.position;
            m_Rigidbody.rotation = transform.rotation;
        }


        private float GetBaseMovementSpeed()
        {
           // CharacterClass characterClass = GameDataSource.Instance.CharacterDataByType[m_CharLogic.NetState.CharacterType];
           // Assert.IsNotNull(characterClass, $"No CharacterClass data for character type {m_CharLogic.NetState.CharacterType}");
           // return characterClass.Speed;
           return 8f;
        }

        /// <summary>
        /// Determines the appropriate MovementStatus for the character. The
        /// MovementStatus is used by the client code when animating the character.
        /// </summary>
        private MovementStatus GetMovementStatus()
        {
            switch (m_MovementState)
            {
                case MovementState.Idle:
                    return MovementStatus.Idle;
                case MovementState.Knockback:
                    return MovementStatus.Uncontrolled;
                default:
                    return MovementStatus.Normal;
            }
        }
}
