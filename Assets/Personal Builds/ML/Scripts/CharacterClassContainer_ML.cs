using System.Collections;
using System.Collections.Generic;
using Unity.Multiplayer.Samples.BossRoom;
using UnityEngine;

public class CharacterClassContainer_ML : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    CharacterClass m_CharacterClass;


    public CharacterClass CharacterClass
    {
        get
        {
            if (m_CharacterClass == null)
            {
            //    m_CharacterClass = m_State.RegisteredAvatar.CharacterClass;
            }

            return m_CharacterClass;
        }
    }

    public void SetCharacterClass(CharacterClass characterClass)
    {
        m_CharacterClass = characterClass;
    }
}
