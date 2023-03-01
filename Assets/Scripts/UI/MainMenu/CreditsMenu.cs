using System;
using UnityEngine.UIElements;

public class CreditsMenu
{
    private readonly Button _backButton;
    
    public Action OnBack { set => _backButton.clicked += value; }
        
    public CreditsMenu(VisualElement root)
    {
        _backButton = root.Q<Button>("back-button");
    }
}
