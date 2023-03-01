using UnityEngine;
using UnityEngine.UIElements;

public class BasicUI 
{
    private InputHandler _inputHandler;
    private string _actionName;
    private string _controlScheme;

    private bool _messageAreaActive;
    private VisualElement _messageArea;

    public BasicUI(VisualElement root, InputHandler inputHandler, string actionName, string controlScheme, BoolEventSO onItemLook)
    {
        _inputHandler = inputHandler;
        _actionName = actionName;
        _controlScheme = controlScheme;

        _messageArea = root.Q("message-area");
        _messageArea.Q<Label>("button-to-use-label").text = GetButtonNameForInteract();

        onItemLook.Action += SwitchMessageArea;
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
