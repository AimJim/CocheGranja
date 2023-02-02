using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLightControl : MonoBehaviour
{
    [SerializeField]
    Material brakeLights;
    [SerializeField]
    BrakeTrail bt;

    public void brake(bool turn)
    {
        if (turn){ brakeLights.EnableKeyword("_EMISSION"); } else { brakeLights.DisableKeyword("_EMISSION"); }
        bt.enabled = turn;
    }

}
