using TMPro;
using UnityEngine;

public class UseButton : MonoBehaviour
{
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private string actionName;
    [SerializeField] private string controlScheme;
    
    private string _useButton;
    
    
    private void OnEnable()
    {
        if (_useButton is null)
        {
            foreach (var binding in _inputHandler.GameInputActionAsset.bindings)
            {
                if (binding.action == actionName && binding.groups == controlScheme)
                {
                    _useButton = binding.path.Split('/')[1].ToUpper();
                    break;
                }
            }
        }

        GetComponent<TextMeshProUGUI>().text = _useButton;
    }
}
