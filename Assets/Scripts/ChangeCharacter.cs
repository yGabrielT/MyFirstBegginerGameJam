using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class ChangeCharacter : MonoBehaviour
{
    [SerializeField] private GameObject[] characters;
    [SerializeField] private Transform[] characterPlayer;
    [SerializeField] private Transform[] characterIA;
    public int characterIndex;
    private bool change = false;
    [SerializeField] private CombatScript[] characterCombatScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && !change && characterIndex + 1 != characters.Length) 
        {
            
            FindRootCamera();
            ChangeCamera();
            
        }
        
    }

    void FindRootCamera()
    {
        change = true;
        Invoke("Cooldown", 2f);
        if (characters[characterIndex + 1] != null && characters[characterIndex].gameObject.activeSelf)
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
        characterIndex++;
    }

    void Cooldown()
    {
        change = false;
    }

}
