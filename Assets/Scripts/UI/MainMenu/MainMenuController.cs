using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(UIDocument))]
public class MainMenuController : MonoBehaviour
{

    private VisualElement _rootVisual;
    private VisualElement _mainMenuElement;
    private VisualElement _creditsMenuElement;

    // Start is called before the first frame update
    void Start()
    {
        _rootVisual = GetComponent<UIDocument>().rootVisualElement;

        _mainMenuElement = _rootVisual.Q("MainMenu");
        _creditsMenuElement = _rootVisual.Q("CreditsMenu");
        
        InitializeMainMenu();
        InitializeCreditsMenu();
    }

    private void InitializeCreditsMenu()
    {
        CreditsMenu creditsMenu = new CreditsMenu(_creditsMenuElement);
        creditsMenu.OnBack = () => ToggleSettingsMenu(false);
    }

    private void InitializeMainMenu()
    {
        MainMenu mainMenu = new MainMenu(_mainMenuElement);
        mainMenu.OnStartGame = () => SceneManager.LoadScene("DemoScene");
        mainMenu.OnOpenCredits = () => ToggleSettingsMenu(true);
        mainMenu.OnExitGame = () => Application.Quit();
    }

    private void ToggleSettingsMenu(bool enable)
    {
        _mainMenuElement.Display(!enable);
        _creditsMenuElement.Display(enable);
    }
}
