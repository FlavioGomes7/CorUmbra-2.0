using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuEvents : MonoBehaviour
{
    private UIDocument _document;

    private Button _buttonStart;
    private Button _buttonQuit;

    private void Awake()
    {
        _document = GetComponent<UIDocument>();

        _buttonStart = _document.rootVisualElement.Q("StartButton") as Button;
        _buttonQuit = _document.rootVisualElement.Q("QuitButton") as Button;
        _buttonStart.clicked += () => SceneManager.LoadScene(1);
        _buttonQuit.clicked += () => Debug.Log("Saiu Do Jogo");

    }

}
