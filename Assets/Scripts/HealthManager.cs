using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CombatScript;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private CombatScript _cbScript;
    [SerializeField] private bool isPlayerDamaged;
    [SerializeField] private int _arrowDamage = 20;
    [SerializeField] private int _swordDamage = 10;
    [SerializeField] private int _webDamage = 30;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Sword")
        {
            _cbScript.ManageHealth(isPlayerDamaged, _swordDamage);
        }
        if (other.gameObject.tag == "Web")
        {
            _cbScript.ManageHealth(isPlayerDamaged, _webDamage);
        }
        if (other.gameObject.tag == "Arrow")
        {
            _cbScript.ManageHealth(isPlayerDamaged, _arrowDamage);
        }
    }
}
