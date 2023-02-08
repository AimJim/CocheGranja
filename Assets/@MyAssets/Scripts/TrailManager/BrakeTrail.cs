using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrakeTrail : MonoBehaviour
{

    [SerializeField]
    GameObject trailMesh;
    GameObject prev = null;

    private void Update()
    {
        GameObject coso = Instantiate(trailMesh);
        coso.transform.position = gameObject.transform.position;
        coso.transform.rotation = gameObject.transform.rotation;
        //if(prev!=null) coso.transform.LookAt(prev.transform);

        prev = coso;
    }
}
