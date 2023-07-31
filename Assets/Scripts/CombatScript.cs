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

    [SerializeField] private GameObject weapon;
    public Creature creature;
    private bool canAttack = true;
    public bool isPlayer = true;
    private Animator swordAnimator;
    private ShootArrow _shoot;
    [SerializeField] private float _cooldown = 1f;

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
                    SkeletonAttack();
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
                    SkeletonAttack();
                    break;

            }
    }
    private void HumanAttack()
    {
        if (swordAnimator == null) swordAnimator = weapon.GetComponent<Animator>();
        if (canAttack)
        {
            canAttack = false;
            swordAnimator.SetTrigger("Attack");
            Invoke("OnCooldown", _cooldown);
        }
            

    }

    private void SkeletonAttack()
    {
        if(_shoot == null)
        {
            _shoot = GetComponentInChildren<ShootArrow>();
        }
        else if(canAttack)
        {
            canAttack = false;
            _shoot.Shoot();
            Invoke("OnCooldown", _cooldown);
        }
        
    }
    private void OnCooldown()
    {
        canAttack = true;
    }
    
}
