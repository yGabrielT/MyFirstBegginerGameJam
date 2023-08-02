using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootWeb : MonoBehaviour
{
    [SerializeField] private GameObject webPrefab;
    [SerializeField] private float webForce;
    [SerializeField] private GameObject aiPos;
    private GameObject camPos;
    // Start is called before the first frame update
    void Start()
    {
        camPos = Camera.main.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Shoot(bool isPlayer)
    {
        if (webPrefab != null)
        {
            if (!isPlayer)
            {
                var obj = Instantiate(webPrefab, transform.position, aiPos.transform.rotation * Quaternion.Euler(0,40f,0));
                var rbobj = obj.GetComponent<Rigidbody>();
                rbobj.AddForce(obj.transform.forward * webForce, ForceMode.Impulse);
                Destroy(obj, 10f);

                var obj2 = Instantiate(webPrefab, transform.position, aiPos.transform.rotation);
                var rbobj2 = obj2.GetComponent<Rigidbody>();
                rbobj2.AddForce(obj2.transform.forward * webForce, ForceMode.Impulse);
                Destroy(obj2, 10f);

                var obj3 = Instantiate(webPrefab, transform.position, aiPos.transform.rotation * Quaternion.Euler(0, -40f, 0));
                var rbobj3 = obj3.GetComponent<Rigidbody>();
                rbobj3.AddForce(obj3.transform.forward * webForce, ForceMode.Impulse);
                Destroy(obj3, 10f);
            }
            else
            {
                var obj = Instantiate(webPrefab, transform.position, camPos.transform.rotation * Quaternion.Euler(0, 40f, 0));
                var rbobj = obj.GetComponent<Rigidbody>();
                rbobj.AddForce(obj.transform.forward * webForce, ForceMode.Impulse);
                Destroy(obj, 10f);

                var obj2 = Instantiate(webPrefab, transform.position, camPos.transform.rotation);
                var rbobj2 = obj2.GetComponent<Rigidbody>();
                rbobj2.AddForce(obj2.transform.forward * webForce, ForceMode.Impulse);
                Destroy(obj2, 10f);

                var obj3 = Instantiate(webPrefab, transform.position, camPos.transform.rotation * Quaternion.Euler(0, -40f, 0));
                var rbobj3 = obj3.GetComponent<Rigidbody>();
                rbobj3.AddForce(obj3.transform.forward * webForce, ForceMode.Impulse);
                Destroy(obj3, 10f);
            }



        }
    }
}
