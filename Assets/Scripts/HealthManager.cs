using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private CombatScript _cbScript;
    [SerializeField] private bool isPlayerDamaged;
    [SerializeField] private int _arrowDamage = 20;
    [SerializeField] private int _swordDamage = 10;
    [SerializeField] private int _webDamage = 30;
    [SerializeField] private int _knockResistance = 5;
    private IAController aController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        aController = GetComponentInParent<IAController>();

        if (other.gameObject.tag == "Sword")
        {
            _cbScript.ManageHealth(isPlayerDamaged, _swordDamage);
            aController?.KnockBack(_knockResistance);
        }
        if (other.gameObject.tag == "Web")
        {
            _cbScript.ManageHealth(isPlayerDamaged, _webDamage);
            aController?.KnockBack(_knockResistance);
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Arrow")
        {
            _cbScript.ManageHealth(isPlayerDamaged, _arrowDamage);
            aController?.KnockBack(_knockResistance);
            Destroy(other.gameObject);
        }

        
    }
}
