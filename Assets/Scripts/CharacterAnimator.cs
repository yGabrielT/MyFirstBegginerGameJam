using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using StarterAssets;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private Animator _iaAnimator;
    [SerializeField] private StarterAssetsInputs _input;
    [SerializeField] private NavMeshAgent _iaAgent;
    private int _agentSpeed;
    public float speeds;
    private float speedFactor;
    private float speedPlayerFactor;
    [SerializeField] private float mutiplyTime;

    private void Start()
    {
        
        _agentSpeed = Animator.StringToHash("Speed");
    }
    private void Update()
    {
        if(_input.move != Vector2.zero)
        {
            speedPlayerFactor = Mathf.Lerp(speedPlayerFactor, .5f, mutiplyTime * Time.deltaTime);
        }
        else
        {
            speedPlayerFactor = Mathf.Lerp(speedPlayerFactor, 0, mutiplyTime * Time.deltaTime);
        }
        if (_input.sprint)
        {
            speedPlayerFactor = Mathf.Lerp(speedPlayerFactor, 1, mutiplyTime * Time.deltaTime);
        }
        if (_iaAgent.speed != 0)
        {
            speedFactor = Mathf.Lerp(speedFactor, 1, mutiplyTime * Time.deltaTime);
        }
        else
        {
            speedFactor = Mathf.Lerp(speedFactor, 0, mutiplyTime * Time.deltaTime);
        }
        if (_playerAnimator != null && _playerAnimator.isActiveAndEnabled)
        {
            _playerAnimator.SetFloat(_agentSpeed, speedPlayerFactor);
        }
        if (_iaAnimator != null && _iaAnimator.isActiveAndEnabled)
        {
            _iaAnimator.SetFloat(_agentSpeed, speedFactor);
        }
        
    }

}
