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
    [SerializeField] private AudioClip audioClick;
    [SerializeField] private AudioClip audioDie;
    private float volume = 2f;
    private bool change = false;

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
        AudioSource.PlayClipAtPoint(audioClick, Camera.main.transform.position, volume);
        manager.Transition(cutScene, transitionSetting, delay);
    }
    public void CreditsButton()
    {
        AudioSource.PlayClipAtPoint(audioClick, Camera.main.transform.position, volume);
        manager.Transition(creditsScene, transitionSetting, delay);
    }

    public void BackToMenuButton()
    {
        AudioSource.PlayClipAtPoint(audioClick, Camera.main.transform.position, volume);
        manager.Transition(backToMenuScene, transitionSetting, delay);
    }
    public void StartingGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        manager.Transition(StartGameScene, transitionSetting, delay);
    }

    public void GoToGameOver()
    {
        if (!change)
        {
            change = true;
            AudioSource.PlayClipAtPoint(audioDie, Camera.main.transform.position, volume);
            manager.Transition(GameOverScene, transitionSetting, delay);
        }
        

    }
    public void ExitButton()
    {
        AudioSource.PlayClipAtPoint(audioClick, Camera.main.transform.position, volume);
        Application.Quit();
    }
}
