using System.Collections;
using System.Collections.Generic;
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
    public bool isThereEnemies = true;
    public bool _isStarted = false;
    private Scene scene;
    private void Awake()
    {

        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);

        

        

    }

    private void Start()
    {
        
        if (!_isStarted)
        {
            _isStarted = true;
            StartGame();
        }
    }
    // When winnig end the game
    public void GameOver()
    {
        scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void StartGame()
    {
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
        }

        if (_difficulty > 3)
        {
            
            for(int i = 0 ; i < InverseGetRandomEnemyNubers(_difficulty); i++)
            {
                Instantiate(_enemiesPrefabs[getRandomEnemyNubers(0)], getRandomEnemySpawn(), Quaternion.identity);
            }
        }
    }

    void ChangeDifficulty()
    {
        
        _difficulty++;
        GameOver();
        Invoke(nameof(StartGame),.5f);
    }

    void Update()
    {
        if (!isThereEnemies)
        {
            isThereEnemies = true;
            
            Invoke("ChangeDifficulty",3f);
        }
    }

    Vector3 getRandomEnemySpawn()
    {
        return new Vector3(Random.Range(-3, 3), 0, Random.Range(-3, 3)) + _enemySpawnPoint.position;
    }

    int getRandomEnemyNubers(int bonus)
    {
        return Random.Range(0,2) + bonus;

    }

    int InverseGetRandomEnemyNubers(int value)
    {
        return Random.Range(0, 2) - value;

    }

}
