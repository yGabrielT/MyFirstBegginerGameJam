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
    private ShootArrow _shootArrow;
    private ShootWeb _shootWeb;
    private Animator _shootWebAnimator;
    [SerializeField] private float _cooldown = 1f;

    public float IAHealth = 100;
    public float PlayerHealth = 100;

    void Awake()
    {
    }
    void Update()
    {
        ChangeOnDeath();
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
                    SpiderAttack();
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
                    SpiderAttack();
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
    private void SpiderAttack()
    {
        if (_shootWebAnimator == null)
        {
            _shootWebAnimator = GetComponentInChildren<Animator>();
        }
        if (_shootWeb == null)
        {
            _shootWeb = GetComponentInChildren<ShootWeb>();
        }
        else if (canAttack)
        {
            
            canAttack = false;
            _shootWebAnimator.SetTrigger("Attack");
            _shootWeb.Shoot(isPlayer);
            Invoke("OnCooldown", _cooldown);
        }
        
        
    }

    private void SkeletonAttack()
    {
        if(_shootArrow == null)
        {
            _shootArrow = GetComponentInChildren<ShootArrow>();
        }
        else if(canAttack)
        {
            canAttack = false;
            _shootArrow.Shoot();
            Invoke("OnCooldown", _cooldown);
        }
        
    }
    private void OnCooldown()
    {
        canAttack = true;
    }

    public void ManageHealth(bool isPlayerDamaged, int Damage)
    {
        if (!isPlayerDamaged)
        {
            IAHealth -= Damage;
        }
        else
        {
            PlayerHealth -= Damage;
        }
    }
    private void ChangeOnDeath()
    {
        //Change character
        if (IAHealth <= 0)
        {
            //Put this character in the array
            ChangeCharacter.Instance.ChangeWhenKill(gameObject);
            //Change
            ChangeCharacter.Instance.toChangeNow = true;
            IAHealth = 100;
        }

        //game over
        if(PlayerHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    
}
