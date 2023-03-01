using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Void Event", menuName = "Scriptable Objects/Events/VoidEvent")]
public class VoidEventSO : ScriptableObject
{
    private UnityAction _action;
    
    public UnityAction Action { get => _action; set => _action = value; }
}
