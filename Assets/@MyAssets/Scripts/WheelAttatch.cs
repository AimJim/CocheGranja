using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelAttatch : MonoBehaviour
{
    [SerializeField]
    Transform coche;

    private void Update()
    {
        transform.position = coche.position;
        transform.rotation = coche.rotation;
    }
}
