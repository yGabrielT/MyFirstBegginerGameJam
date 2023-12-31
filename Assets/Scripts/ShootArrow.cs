using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class ShootArrow : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform arrowSpawn;
    [SerializeField] private float ArrowForce;
    [SerializeField] private GameObject aiPos;
    [SerializeField] private GameObject camPos;
    private PlayAudio audioArrow;
    // Start is called before the first frame update
    void Start()
    {
        audioArrow = GetComponent<PlayAudio>();
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
                var obj = Instantiate(arrowPrefab, arrowSpawn.position, aiPos.transform.rotation);
                var rbobj = obj.GetComponent<Rigidbody>();
                rbobj.AddForce(obj.transform.forward * ArrowForce, ForceMode.Impulse);
                audioArrow.playNormalAudio();
                Destroy(obj, 5f);
            }
            else
            {
                var obj = Instantiate(arrowPrefab, arrowSpawn.position, camPos.transform.rotation);
                var rbobj = obj.GetComponent<Rigidbody>();
                rbobj.AddForce(obj.transform.forward * ArrowForce, ForceMode.Impulse);
                Destroy(obj, 5f);
            }
            


        }
    }
}
