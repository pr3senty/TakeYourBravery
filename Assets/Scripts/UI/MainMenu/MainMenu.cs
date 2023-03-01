using System;
using UnityEngine.UIElements;

public class MainMenu
{
    private readonly Button _startGameButton;
    private readonly Button _creditsButton;
    private readonly Button _exitGameButton;
    
    public Action OnOpenCredits { set => _creditsButton.clicked += value; }
    public Action OnStartGame { set => _startGameButton.clicked += value; }
    public Action OnExitGame { set => _exitGameButton.clicked += value; }

    public MainMenu(VisualElement root)
    {
        _startGameButton = root.Q<Button>("start-game-button");
        _creditsButton = root.Q<Button>("credits-button");
        _exitGameButton = root.Q<Button>("exit-game-button");
    }
}
