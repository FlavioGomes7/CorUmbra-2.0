using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using Cursor = UnityEngine.Cursor;

public class MenuController : MonoBehaviour
{
    private InputAction toggleAction;

    private VisualElement rootOptions;
    private VisualElement currentMenuOptions;
    private Stack<VisualElement> menuHistory = new Stack<VisualElement>();

    private AsyncOperation operation;
    void OnEnable()
    {

        var uiDocument = GetComponent<UIDocument>();
        rootOptions = uiDocument.rootVisualElement;

        if (rootOptions == null)
        {
            Debug.LogError("Root is null");
            return;

        }
        if(uiDocument.visualTreeAsset.name == "mai-menu")
        {
            currentMenuOptions = rootOptions.Q<VisualElement>("BaseMenu");
            SetMainMenuButtons();
        }else if (uiDocument.visualTreeAsset.name == "Pause-MENU")
        {
            currentMenuOptions = rootOptions.Q<VisualElement>("PauseMenu");
            SetPauseMenu();
        }

        
    }
    void Start()
    {

    }
    void SetMainMenuButtons()
    {
        // Adicionando eventos aos botões do menu principal
        var startButton = rootOptions.Q<Button>("StartButton");
        var optionsButton = rootOptions.Q<Button>("OptionsButton");
        var exitButton = rootOptions.Q<Button>("ExitButton");
        var backButton = rootOptions.Q<Button>("BackButton");
        var readyButton = rootOptions.Q<Button>("readyButton");
        startButton.clicked += ShowStartMenu;
        optionsButton.clicked += ShowOptionsMenu;
        exitButton.clicked += ExitGame;
        readyButton.clicked += OnReadyButtonClicked;
        if (backButton != null)
        {
            backButton.clicked += Back;
        }

        ClickeAbleButtons();
    }
    void SetPauseMenu()
    {
        var pauseMenu = rootOptions.Q<VisualElement>("PauseMenu");
        var resumeButton = rootOptions.Q<Button>("ResumeButton");

        toggleAction = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/p");
        toggleAction.performed += ctx => TogglePauseMenu(pauseMenu);
        toggleAction.Enable();

        
        pauseMenu.style.display = DisplayStyle.None; // Iniciar com o menu de pausa desabilitado
        resumeButton.clicked += () => TogglePauseMenu(pauseMenu);
        ClickeAbleButtons() ;
    }
    void ShowStartMenu()
    {
        currentMenuOptions = rootOptions.Q<VisualElement>("VisualElement-Base");
        // Vai até a tela de calibragem e aguarda o jogo carregar asyncronamente.
        
        SwitchMenu("Brigthness-screen");
        LoadGameScene();
        //SceneManager.LoadSceneAsync(1);

    }

    void ShowOptionsMenu()
    {
        SwitchMenu("OptionsMenu");
    }
    void Back()
    {
        if (menuHistory.Count > 0) { 
            currentMenuOptions.style.display = DisplayStyle.None; 
            currentMenuOptions = menuHistory.Pop(); 
            currentMenuOptions.style.display = DisplayStyle.Flex; }
    }

    void ExitGame()
    {
        Debug.Log("ExitGame called");
        Application.Quit();
    }


    void SwitchMenu(string menuName)
    {

        if (currentMenuOptions != null)
        {
            currentMenuOptions.style.display = DisplayStyle.None;
            menuHistory.Push(currentMenuOptions);
        }

        currentMenuOptions = rootOptions.Q<VisualElement>(menuName);

        if (currentMenuOptions != null)
        {
            currentMenuOptions.style.display = DisplayStyle.Flex;
        }
        else
        {
            Debug.LogError($"Menu with name {menuName} not found");
        }
    }

    private void OnReadyButtonClicked() {
        operation.allowSceneActivation = true;
    }

    void LoadGameScene()
    {
        StartCoroutine(LoadSceneAsync(1));
    }
    private IEnumerator LoadSceneAsync(int sceneNumber) { 
        operation = SceneManager.LoadSceneAsync(sceneNumber); 
        operation.allowSceneActivation = false;
        var readyBtn = rootOptions.Q<Button>("readyButton");
        while (!operation.isDone) { 
            if (operation.progress >= .9f) { 
                readyBtn.visible = true;
                break; 
            } yield return null; } }
    void OnMouseEnter(Button button, string originalText)
    {
        // Adicionar ">" antes do texto original ao passar o mouse
        button.text = "> " + originalText;
    }

    void OnMouseLeave(Button button, string originalText)
    {
        // Restaurar o texto original ao remover o mouse
        button.text = originalText;
    }
    void ClickeAbleButtons()
    {
        foreach (var button in rootOptions.Query<Button>().ToList())
        {
            var originalText = button.text; // Adicionar manipuladores de eventos de mouse
            button.RegisterCallback<MouseEnterEvent>(evt => OnMouseEnter(button, originalText));
            button.RegisterCallback<MouseLeaveEvent>(evt => OnMouseLeave(button, originalText));
        }
    }
    void TogglePauseMenu(VisualElement pauseMenu)
    {
            if (pauseMenu.style.display == DisplayStyle.None)
            {
                pauseMenu.style.display = DisplayStyle.Flex; 
                Time.timeScale = 0f; // Pausar o jogo
                ToggleCursor(true);
            } else { 
                pauseMenu.style.display = DisplayStyle.None; 
                Time.timeScale = 1f; // Retomar o jogo
                ToggleCursor(false);
            }
        }
    void ToggleCursor(bool isVisible)
    {
        if (isVisible)
        {
            Cursor.lockState = CursorLockMode.None; // Libera o cursor
            Cursor.visible = true; // Torna o cursor visível
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked; // Trava o cursor
            Cursor.visible = false; // Torna o cursor invisível
        }
    }
}
