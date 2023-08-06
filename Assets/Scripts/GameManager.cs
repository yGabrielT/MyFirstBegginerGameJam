using EasyTransition;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum CharacterUsing
    {
        Human,
        Spider,
        Skeleton
    }
    public CharacterUsing usingCharacter = CharacterUsing.Human;
    public static GameManager instance { get; private set;}
    private Transform _playerSpawnPoint;
    private Transform _enemySpawnPoint;
    [SerializeField] private GameObject[] _playerPrefabs;
    [SerializeField] private GameObject[] _enemiesPrefabs;
    [SerializeField] private int _difficulty = 0;
    [SerializeField] private float _timeToStart = 3f;
    public bool isThereEnemies = true;
    public bool _isStarted = false;
    private Scene scene;

    [SerializeField] public int RoundsSurvived = 0;
    [SerializeField] private TextMeshProUGUI score;

    [SerializeField] private AudioClip audioVictory;

    private void Awake()
    {

        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        

        

        

    }

    private void Start()
    {
        
        
        if (!_isStarted)
        {
            _isStarted = true;
            StartGame();
        }
    }

    public void GameOver()
    {
        MenuManager.instance.GoToGameOver();
        Invoke(nameof(ChangeOverScore), _timeToStart);
    }

    private void ChangeOverScore()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        score = GameObject.FindWithTag("Score").GetComponent<TextMeshProUGUI>();
        if (score != null)
        {
            score.text = "You spectated " + RoundsSurvived + " rounds.";
        }
        Destroy(gameObject);
    }
    // When winnig next difficulty
    public void NextDifficulty()
    {
        MenuManager.instance.StartingGame();

    }

    public void StartGame()
    {
        RoundsSurvived++;
        score = GameObject.FindWithTag("Score").GetComponent<TextMeshProUGUI>();
        if (score != null)
        {
            score.text = "Round: " + RoundsSurvived;
        }
        
        Debug.Log("Spawning");
        _playerSpawnPoint = GameObject.FindWithTag("PlayerSpawn").GetComponent<Transform>();
        _enemySpawnPoint = GameObject.FindWithTag("EnemySpawn").GetComponent<Transform>();

        //To instantiate the Player depending of the last scene
        switch (usingCharacter)
        {
            case(CharacterUsing.Human):
                Instantiate(_playerPrefabs[0], _playerSpawnPoint.position, Quaternion.identity);
                break;
            case (CharacterUsing.Skeleton):
                Instantiate(_playerPrefabs[1], _playerSpawnPoint.position, Quaternion.identity);
                break;
            case (CharacterUsing.Spider):
                Instantiate(_playerPrefabs[2], _playerSpawnPoint.position, Quaternion.identity);
                break;
        }
        /*
        if (_difficulty > 3)
        {

            for (int i = 0; i < InverseGetRandomEnemyNubers(_difficulty); i++)
            {
                Instantiate(_enemiesPrefabs[getRandomEnemyNubers(0)], getRandomEnemySpawn(), Quaternion.identity);
            }
        }*/

        //Spawn Enemies
        switch (_difficulty)
        {
            case 0:
                Instantiate(_enemiesPrefabs[1], getRandomEnemySpawn(), Quaternion.identity);
                break;
            case 1:
                Instantiate(_enemiesPrefabs[0], getRandomEnemySpawn(), Quaternion.identity);
                break;
            case 2:
                Instantiate(_enemiesPrefabs[2], getRandomEnemySpawn(), Quaternion.identity);
                Instantiate(_enemiesPrefabs[1], getRandomEnemySpawn(), Quaternion.identity);
                break;
            case 3:
                Instantiate(_enemiesPrefabs[2], getRandomEnemySpawn(), Quaternion.identity);
                Instantiate(_enemiesPrefabs[1], getRandomEnemySpawn(), Quaternion.identity);
                Instantiate(_enemiesPrefabs[0], getRandomEnemySpawn(), Quaternion.identity);
                break;
            default:
                for (int i = 0; i < _difficulty; i++)
                {
                    Instantiate(_enemiesPrefabs[getRandomEnemyNubers(0)], getRandomEnemySpawn(), Quaternion.identity);
                }
                break;
        }

        
    }

    void ChangeDifficulty()
    {
        
        _difficulty++;
        NextDifficulty();
        Invoke(nameof(StartGame),_timeToStart);
    }

    void Update()
    {
        if (!isThereEnemies)
        {
            isThereEnemies = true;
            AudioSource.PlayClipAtPoint(audioVictory, Camera.main.transform.position);
            Invoke("ChangeDifficulty",3.5f);
        }
    }

    Vector3 getRandomEnemySpawn()
    {
        return new Vector3(Random.Range(-3, 3), 0, Random.Range(-3, 3)) + _enemySpawnPoint.position;
    }

    int getRandomEnemyNubers(int bonus)
    {
        return Random.Range(0,3) + bonus;

    }


}
