using System;
using UnityEngine.UIElements;

public class PauseMenu
{
    private Button _resumeButton;
    private Button _creditsButton;
    private Button _exitGameButton;
    
    public Action OnResumeButton { set => _resumeButton.clicked += value; }
    public Action OnCreditsButton { set => _creditsButton.clicked += value; }
    public Action OnExitGameButton { set => _exitGameButton.clicked += value; }
    
    public PauseMenu(VisualElement root)
    {
        _resumeButton = root.Q<Button>("resume-button");
        _creditsButton = root.Q<Button>("credits-button");
        _exitGameButton = root.Q<Button>("exit-game-button");
    }
}
