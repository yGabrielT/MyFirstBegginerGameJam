using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using Unity.VisualScripting;

public class CombatScript : MonoBehaviour
{
    public enum Creature
    {
        Human,
        Skeleton,
        Spider,
        Globlin
    }
    [SerializeField] private Animator weaponAnimator;
    public Creature creature;
    private bool canAttack = true;
    private float timeElapsed;
    void Awake()
    {

    }
    void Update()
    {
        switch (creature)
        {
            case Creature.Human:
                HumanAttack();
                break;
                
        }
    }

    void HumanAttack()
    {
        if (StarterAssetsInputs.instance.attack)
        {
            StarterAssetsInputs.instance.attack = false;
            if (canAttack)
            {
                canAttack = false;
                
                weaponAnimator.SetTrigger("Attack");
                timeElapsed = 0f;
                Invoke("OnCooldown", 1.5f);
            }
            

        }

    }
    private void OnCooldown()
    {
        canAttack = true;
    }
    
}
