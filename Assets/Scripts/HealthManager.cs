using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private CombatScript _cbScript;
    [SerializeField] private bool isPlayerDamaged;
    [SerializeField] private int _arrowDamage = 20;
    [SerializeField] private int _swordDamage = 10;
    [SerializeField] private int _webDamage = 30;
    [SerializeField] private int _knockResistance = 5;
    private IAController aController;
    private Vector3 _originalScale;
    private Vector3 _toScale;
    // Start is called before the first frame update
    void Start()
    {
        _originalScale = transform.localScale;
        _toScale = _originalScale * 0.1f;
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        aController = GetComponentInParent<IAController>();

        if (other.gameObject.tag == "Sword")
        {
            _cbScript.ManageHealth(isPlayerDamaged, _swordDamage);
            aController?.KnockBack(_knockResistance);
            transform.DOShakeScale(.2f, new Vector3(0.4f, 0.3f, 0.1f), 1, 20, true, ShakeRandomnessMode.Full);
        }
        if (other.gameObject.tag == "Web")
        {
            _cbScript.ManageHealth(isPlayerDamaged, _webDamage);
            aController?.KnockBack(_knockResistance);
            
            Destroy(other.gameObject);
            transform.DOShakeScale(.2f, .5f, 1, 20, true, ShakeRandomnessMode.Full);

        }
        if (other.gameObject.tag == "Arrow")
        {
            _cbScript.ManageHealth(isPlayerDamaged, _arrowDamage);
            aController?.KnockBack(_knockResistance);
            Destroy(other.gameObject);
            transform.DOShakeScale(.2f, .5f, 1, 20, true, ShakeRandomnessMode.Full);
        }

        
    }
}
