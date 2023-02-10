using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLightControl : MonoBehaviour
{
    [SerializeField]
    Material brakeLights;
    [SerializeField]
    BrakeTrail bt1;
    [SerializeField]
    BrakeTrail bt2;
    public void brake(bool turn)
    {
        if (turn){ brakeLights.EnableKeyword("_EMISSION"); } else { brakeLights.DisableKeyword("_EMISSION"); }
        bt1.enabled = turn;
        bt2.enabled = turn;
    }

}
