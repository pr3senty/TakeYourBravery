using System;
using UnityEngine;
using UnityEngine.UIElements;

public class BasicUIHandler : MonoBehaviour
{
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private BoolEventSO _onItemLook;
    [SerializeField] private string _actionName;
    [SerializeField] private string _controlScheme;
    
    private bool _messageAreaActive;
    private VisualElement _messageArea;

    private void Start()
    {
        _messageArea = GetComponent<UIDocument>().rootVisualElement.Q("message-area");
        _messageArea.Q<Label>("button-to-use-label").text = GetButtonNameForInteract();
    }

    private void Update()
    {
        _onItemLook.Action += SwitchMessageArea;
    }


    private void SwitchMessageArea(bool value)
    {
        _messageArea.Display(value);
    }

    private string GetButtonNameForInteract()
    {
        string letter = "Cant find a button SCRIPT BASICUI";
        
        foreach (var binding in _inputHandler.GameInputActionAsset.bindings)
        {
            if (binding.action == _actionName && binding.groups == _controlScheme)
            {
                letter = binding.path.Split('/')[1].ToUpper();
                break;
            }
        }
        return letter;
    }
}
