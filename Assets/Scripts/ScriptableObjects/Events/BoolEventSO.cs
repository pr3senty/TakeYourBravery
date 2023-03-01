using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Bool Event", menuName = "Scriptable Objects/Events/BoolEvent")]
public class BoolEventSO : ScriptableObject
{
    private UnityAction<bool> _action;
    public UnityAction<bool> Action { get => _action; set => _action = value; }
}
