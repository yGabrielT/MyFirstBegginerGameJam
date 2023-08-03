using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class ShootArrow : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private float ArrowForce;
    [SerializeField] private GameObject aiPos;
    [SerializeField] private GameObject camPos;
    // Start is called before the first frame update
    void Start()
    {
        camPos = Camera.main.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        if (arrowPrefab != null)
        {
            if(aiPos != null && aiPos.activeSelf)
            {
                var obj = Instantiate(arrowPrefab, transform.position, aiPos.transform.rotation);
                var rbobj = obj.GetComponent<Rigidbody>();
                rbobj.AddForce(obj.transform.forward * ArrowForce, ForceMode.Impulse);
                Destroy(obj, 5f);
            }
            else
            {
                var obj = Instantiate(arrowPrefab, transform.position, camPos.transform.rotation);
                var rbobj = obj.GetComponent<Rigidbody>();
                rbobj.AddForce(obj.transform.forward * ArrowForce, ForceMode.Impulse);
                Destroy(obj, 5f);
            }
            


        }
    }
}
