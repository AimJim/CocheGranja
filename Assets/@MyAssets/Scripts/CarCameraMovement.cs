using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCameraMovement : MonoBehaviour
{
    [SerializeField]
    GameObject objective;
    
    Rigidbody objectvieRB;

  

    [SerializeField]
    Vector3 offsetPosition;

    private void Awake()
    {
        
    }

    private void Update()
    {
        transform.position = objective.transform.position;
        float neededRotation = 0;
        if (objective.GetComponent<Rigidbody>().velocity.magnitude > 0.5f)
        {
            Vector3 direction = objective.GetComponent<CarMovement>().getMovementVector();
            if (direction.z == 0) return;
            
            if (direction.x > 0 && direction.z > 0)
            {
                neededRotation = Mathf.Abs(Mathf.Atan(direction.x / direction.z) * Mathf.Rad2Deg);
            }
            else if (direction.z > 0 && direction.x < 0)
            {
                neededRotation = -Mathf.Abs(Mathf.Atan(direction.x / direction.z) * Mathf.Rad2Deg);

            }
            else if (direction.z < 0 && direction.x < 0)
            {
                neededRotation = 180 + Mathf.Abs(Mathf.Atan(direction.x / direction.z) * Mathf.Rad2Deg);
            }
            else if (direction.z < 0 && direction.x > 0)
            {
                neededRotation = 180 - Mathf.Abs(Mathf.Atan(direction.x / direction.z) * Mathf.Rad2Deg);
            }
        }

        
       
        transform.rotation = Quaternion.Euler(0, neededRotation, 0);
        

    }

}
