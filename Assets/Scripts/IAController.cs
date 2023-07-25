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
    private float NextMoveCD = 0.25f;
    void Start()
    {
        
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

    void FindActualPlayer()
    {

        if(GameObject.FindWithTag("Character").TryGetComponent(out CombatScript combScript))
        {
            if(combScript.isPlayer == true)
            {
                var mainObj = combScript.gameObject;
                targetPos = mainObj.transform.Find("Player");
            }
            else
            {
                return;
            }
        }
    }

    void TakeDamage()
    {
        

    }

    void MoveAI()
    {
        NextMoveCD -= Time.deltaTime;
        if(distanceToTarget >= Attackrange && NextMoveCD <= 0)
        {
            NextMoveCD = .25f;
            transform.LookAt(targetPos.position);
            agent.SetDestination(targetPos.position);
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
