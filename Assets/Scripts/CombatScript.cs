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
        Spider
    }

    [SerializeField] private Animator weaponAnimator;
    public Creature creature;
    private bool canAttack = true;
    public bool isPlayer = true;

    void Awake()
    {
    }
    void Update()
    {
        ManageAttackTypePlayer();
    }

    private void ManageAttackTypePlayer()
    {
        if (StarterAssetsInputs.instance.attack && isPlayer)
        {
            StarterAssetsInputs.instance.attack = false;
            switch (creature)
            {
                case Creature.Human:
                    HumanAttack();
                    break;
                case Creature.Spider:

                    break;
                case Creature.Skeleton:

                    break;

            }
        }
    }

    public void ManageAttackTypeAI()
    {
            switch (creature)
            {
                case Creature.Human:
                    HumanAttack();
                    break;
                case Creature.Spider:

                    break;
                case Creature.Skeleton:

                    break;

            }
    }
    public void HumanAttack()
    {

        if (canAttack)
        {
            canAttack = false;    
            weaponAnimator.SetTrigger("Attack");
            Invoke("OnCooldown", 1.5f);
        }
            

    }
    private void OnCooldown()
    {
        canAttack = true;
    }
    
}
