using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class DemoSceneMenuController : MonoBehaviour
{
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private string actionName;
    [SerializeField] private string controlScheme;
    
    [SerializeField] private BoolEventSO _onItemLook;

    private VisualElement _rootVisual;
    private bool _inPause;
    private VisualElement _basicUI;
    private VisualElement _pauseMenuElement;
    private VisualElement _creditsMenuElement;

    // Start is called before the first frame update
    private void Start()
    {
        _rootVisual = GetComponent<UIDocument>().rootVisualElement;

        _basicUI = _rootVisual.Q("BasicUI");
        _pauseMenuElement = _rootVisual.Q("PauseMenu");
        _creditsMenuElement = _rootVisual.Q("Credits");
        
        
        InitializeMainMenu();
        InitializeCreditsMenu();
        InitializeBasicUI();
    }

    private void InitializeCreditsMenu()
    {
        CreditsMenu creditsMenu = new CreditsMenu(_creditsMenuElement);
        creditsMenu.OnBack = () => Debug.Log("some");
    }

    private void InitializeMainMenu()
    {
        PauseMenu pauseMenu = new PauseMenu(_pauseMenuElement);
        
        pauseMenu.OnCreditsButton = () => ToggleSettingsMenu(true);
        pauseMenu.OnResumeButton = () => TogglePauseMenu(!_inPause);
        pauseMenu.OnExitGameButton = () => Application.Quit();
    }

    private void InitializeBasicUI()
    {
        BasicUI basicUI = new BasicUI(_basicUI, _inputHandler, actionName, controlScheme, _onItemLook);
    }
    
    private void ToggleSettingsMenu(bool enable)
    {
        _pauseMenuElement.Display(!enable);
        _creditsMenuElement.Display(enable);
    }

    private void TogglePauseMenu(bool enable)
    {
        _pauseMenuElement.Display(enable);
        _basicUI.Display(!enable);
        _inPause = enable;
    }
}