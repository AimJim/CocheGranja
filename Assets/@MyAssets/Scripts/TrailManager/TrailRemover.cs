using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailRemover : MonoBehaviour
{
    [SerializeField]
    float dieTime;
    private void Awake()
    {
        StartCoroutine(die());
    }

    IEnumerator die()
    {
        yield return new WaitForSeconds(dieTime);
        Destroy(gameObject);
    }
}
