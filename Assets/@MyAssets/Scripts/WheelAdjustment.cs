using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelAdjustment : MonoBehaviour
{
    [SerializeField]
    float minDistance; //Brazo encojido
    [SerializeField]
    float maxDistance; //Brazo extendido

    Vector3 ogPosLocal;
    

    private void Awake()
    {
        ogPosLocal = transform.localPosition;
    }


    private void Update()
    {

        //transform.localPosition = Vector3.zero;
        transform.localPosition = new Vector3(ogPosLocal.x, Mathf.Clamp(transform.localPosition.y, -maxDistance, minDistance) , ogPosLocal.z);
    }
}
