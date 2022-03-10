using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Event", fileName = "New Game Event")]
public class GameEventSO : ScriptableObject{
    HashSet<GameEventListener> listeners = new HashSet<GameEventListener>();
}
