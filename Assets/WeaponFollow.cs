using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFollow : MonoBehaviour
{
    [SerializeField] private Transform playerPos;
    [SerializeField] private Transform IAPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(playerPos.gameObject.activeSelf)
        {
            this.transform.position = new Vector3(playerPos.transform.position.x, transform.position.y, playerPos.transform.position.z);
            this.transform.rotation = Quaternion.Euler(0, playerPos.localRotation.y, 0);
        }
        else
        {
            this.transform.position = new Vector3(IAPos.transform.position.x, transform.position.y, IAPos.transform.position.z);
            this.transform.rotation = Quaternion.Euler(0, IAPos.localRotation.y, 0);
        }
    }
}
