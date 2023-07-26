using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFollow : MonoBehaviour
{
    [SerializeField] private Transform playerPos;
    [SerializeField] private Transform IAPlaceHolderPos;
    [SerializeField]private Transform WeaponPlaceHolder;
    [SerializeField]private Transform Weapon;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
        if (!playerPos.gameObject.activeSelf && !IAPlaceHolderPos.gameObject.activeSelf && Weapon != null)
        {
            Destroy(Weapon.gameObject);
            return;
        }


        if (playerPos.gameObject.activeSelf && playerPos != null)
        {
            Weapon.SetParent(WeaponPlaceHolder);
        }
        if(!playerPos.gameObject.activeSelf &&  IAPlaceHolderPos != null)
        {
            Weapon.SetParent(IAPlaceHolderPos);
        }
        
        
        
    }

}
