using EasyTransition;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;
    [SerializeField] private string creditsScene = "Credits";
    [SerializeField] private string cutScene = "Cutscene";
    [SerializeField] private string backToMenuScene = "Menu";
    [SerializeField] private string StartGameScene = "Fase";
    [SerializeField] private string GameOverScene = "GameOver";
    public TransitionManager manager;
    [SerializeField] private float delay;
    [SerializeField] TransitionSettings transitionSetting;

    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        
        manager = TransitionManager.Instance();
    }
    public void StartButton()
    {
        manager.Transition(cutScene, transitionSetting, delay);
    }
    public void CreditsButton()
    {
        manager.Transition(creditsScene, transitionSetting, delay);
    }

    public void BackToMenuButton()
    {
        manager.Transition(backToMenuScene, transitionSetting, delay);
    }
    public void StartingGame()
    {
        manager.Transition(StartGameScene, transitionSetting, delay);
    }

    public void GoToGameOver()
    {
        manager.Transition(GameOverScene, transitionSetting, delay);

    }
    public void ExitButton()
    {
        Application.Quit();
    }
}
