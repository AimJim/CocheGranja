using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    Rigidbody rb;
    CarLightControl clc;
    
    

    [SerializeField]
    float accelForce;

    [SerializeField]
    float turnSpeed;

    [SerializeField]
    float maxDrag;

    [SerializeField]
    float driftForwardMovePercentage;

    int collisions;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        clc = GetComponent<CarLightControl>();
        
        driftForwardMovePercentage = driftForwardMovePercentage / 100;
        
    }


    //TODO cambiar a input en condiciones, de momento esto
    

    Vector3 prevPos = Vector3.zero;
    Vector3 posPos = Vector3.zero;
    float angle;
    private void FixedUpdate()
    {
        
        angle = Vector3.Angle(getMovementVector(), transform.forward);
        

        prevPos = posPos;

        if(collisions > 0)
        {
            rb.drag = maxDrag * (angle % 90) / 10;

            

            rb.AddForce(gameObject.transform.forward * accelForce * CarControl.getInstance().getAccelInput());
            
            
             if(rb.velocity.magnitude > 10f && angle < 90 && CarControl.getInstance().getAccelInput() < 0.1f && CarControl.getInstance().getBrakeInput() > -0.1f)
            {
                //Forward
                rb.AddForce(gameObject.transform.forward * accelForce * driftForwardMovePercentage);
            } else if (rb.velocity.magnitude > 10f && angle > 90 && CarControl.getInstance().getAccelInput() < 0.1f && CarControl.getInstance().getBrakeInput() > -0.1f)
            {
                //Backwards
                rb.AddForce(- gameObject.transform.forward * accelForce * driftForwardMovePercentage);
            }              

            rb.AddForce(gameObject.transform.forward * accelForce * CarControl.getInstance().getBrakeInput());

            
            
            rb.MoveRotation(gameObject.transform.rotation * Quaternion.Euler(0, turnSpeed * Time.fixedDeltaTime * CarControl.getInstance().getSteerInput(), 0));
        
            
        } else
        {
            rb.drag = 0;
        }
        //Control de luces de freno
        if (rb.velocity.magnitude > 1f && angle < 90 && CarControl.getInstance().getBrakeInput() < -0.1f)
        {

            clc.brake(true);
        }
        else
        {
            clc.brake(false);
        }




        //Ultima linea siempre
        posPos = transform.position;
    }

    public Vector3 getMovementVector()
    {
        return posPos - prevPos;
    }

    private void OnCollisionEnter(Collision collision)
    {
        collisions++;
    }
    private void OnCollisionExit(Collision collision)
    {
        collisions--;
    }

   
}
