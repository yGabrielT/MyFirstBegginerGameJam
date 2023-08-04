using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class ChangeCharacter : MonoBehaviour
{
    public static ChangeCharacter Instance; 
    [SerializeField] private GameObject[] characters;
    [SerializeField] private Transform[] characterPlayer;
    [SerializeField] private Transform[] characterIA;
    public int characterIndex;
    private bool change = false;
    public bool toChangeNow = false;
    private bool isWorking = true;
    [SerializeField] private CombatScript[] characterCombatScript;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    void Start()
    {
        characters[characterIndex] = FindPlayerArray();
    }

    // Update is called once per frame
    void Update()
    {
        if(toChangeNow && !change && characterIndex + 1 != characters.Length) 
        {
            toChangeNow = false;
            OrganizeArray();
            FindRootCamera();
            ChangeCamera();
            
        }
        
    }

    void FindRootCamera()
    {
        
        change = true;
        Invoke("Cooldown", 2f);
        if (characters[characterIndex + 1] != null && characters[characterIndex].gameObject.activeSelf && characters[characterIndex] != null)
        {
            Debug.Log("Searching");

            //Find all character transforms for IA and players
            characterPlayer[characterIndex] = characters[characterIndex].transform.Find("Player");
            characterPlayer[characterIndex + 1] = characters[characterIndex + 1].transform.Find("Player");
            characterIA[characterIndex + 1] = characters[characterIndex + 1].transform.Find("IA");

            //Find all combatsScripts to change later
            characterCombatScript[characterIndex + 1] = characters[characterIndex + 1].GetComponent<CombatScript>();

        }
        else
        {
            Debug.Log("No more characters");
        }
    }

    void ChangeCamera()
    {
        if(characters[characterIndex + 1] != null && characters[characterIndex] != null)
        {
            //Deactivate previous player
            characterPlayer[characterIndex].gameObject.SetActive(false);
            Destroy(characters[characterIndex].gameObject, 2f);

            //Activate next player bool to true
            if (characterCombatScript[characterIndex + 1] != null)
            {
                characterCombatScript[characterIndex + 1].isPlayer = true;
            }
            else
            {
                Debug.Log(characterCombatScript[characterIndex + 1]);
            }

            //Activate player and deactivate previous IA
            characterIA[characterIndex + 1].gameObject.SetActive(false);
            characterPlayer[characterIndex + 1].gameObject.SetActive(true);
        }
        
    }

    void Cooldown()
    {
        change = false;
    }

    GameObject FindPlayerArray()
    {
        var gObjsPlayer = GameObject.FindGameObjectsWithTag("Character");

        if (gObjsPlayer != null || gObjsPlayer.Length != 0)
        {
            for (int j = 0; j < gObjsPlayer.Length; j++)
            {
                if (gObjsPlayer[j].GetComponent<CombatScript>().isPlayer && gObjsPlayer[j].activeSelf)
                {
                    return gObjsPlayer[j].gameObject;
                }
                else
                {
                    Debug.Log("No player Found while changing character");
                }
            }
        }
        return null;
    }

    GameObject FindEnemiesArray()
    {
        var gObjsEnemies = GameObject.FindGameObjectsWithTag("Character");
        if (gObjsEnemies != null || gObjsEnemies.Length != 0)
        {
            for (int x = 0; x < gObjsEnemies.Length; x++)
            {
                if (!gObjsEnemies[x].GetComponent<CombatScript>().isPlayer && gObjsEnemies[x].activeSelf)
                {
                    return gObjsEnemies[x].gameObject;
                }
                else
                {
                    Debug.Log("No enemy Found while changing character");
                }
            }
        }
        return null;
    }

    void OrganizeArray()
    {
        try
        {
            characterIndex = 0;
            characters[characterIndex] = FindPlayerArray();
            //characters[characterIndex + 1] = FindEnemiesArray();
        }
        catch
        {
            Debug.Log("Oh no");
        }
        
    }

    public void ChangeWhenKill(GameObject enemyToBe)
    {
        characters[characterIndex + 1] = enemyToBe;
    }
}
