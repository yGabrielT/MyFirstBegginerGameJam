using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class IAController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    public Transform targetPos;
    [SerializeField] private float distanceToTarget;
    [SerializeField] private float Attackrange;
    [SerializeField] private CombatScript combatEnemy;
    private float timeElapsed = .5f;
    private float agentOriginalSpeed;
    void Start()
    {
        agentOriginalSpeed = agent.speed;
    }

    void Update()
    {
        FindActualPlayer();
        distanceToTarget = Vector3.Distance(transform.position, targetPos.position);
        if (targetPos != null && combatEnemy.isPlayer == false)
        {
            MoveAI();
            Attack();
        }
    }

    private void FindActualPlayer()
    {
        //Puts every character in an array to find the player transform
        if (targetPos == null)
        {
            var gObjs = GameObject.FindGameObjectsWithTag("Character");
            for (int i = 0; i < gObjs.Length; i++)
            {
                if (gObjs[i].GetComponent<CombatScript>().isPlayer && gObjs[i].activeSelf)
                {
                    targetPos = gObjs[i].transform.Find("Player");
                }
                else
                {
                    Debug.Log("No player Found or player game object dont have (Player) in his name");
                }
            }
        }
        
    }

    void MoveAI()
    {
        //Look at player everytime
        transform.LookAt(targetPos.transform);
        var angle = transform.rotation.eulerAngles;
        angle.x = 0;
        angle.z = 0;
        transform.rotation = Quaternion.Euler(angle);
        //Limits attack range
        if (distanceToTarget >= Attackrange)
        {
            agent.speed = agentOriginalSpeed;
            agent.SetDestination(targetPos.position);
        }
        // If is close dont walk more
        else 
        {
            agent.speed = 0;
        }
            
    }

    void Attack()
    {
        timeElapsed -= Time.deltaTime;
        if (timeElapsed <= 0f)
        {
            timeElapsed = .5f;
            if (distanceToTarget <= Attackrange)
            {
                AttackReference();
            }
        }
        
    }

    void AttackReference()
    {
        combatEnemy.ManageAttackTypeAI();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Attackrange);
    }
}
