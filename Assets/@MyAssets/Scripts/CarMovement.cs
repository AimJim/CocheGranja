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

    int collisions;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        clc = GetComponent<CarLightControl>();
        
    }


    //TODO cambiar a input en condiciones, de momento esto
    bool forward;
    bool backward;
    bool left;
    bool right;
    private void Update()
    {
        
        forward = Input.GetKey(KeyCode.W);
        backward = Input.GetKey(KeyCode.S);
        left = Input.GetKey(KeyCode.A);
        right = Input.GetKey(KeyCode.D);
    }

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

            if (forward)
            {

                rb.AddForce(gameObject.transform.forward * accelForce);
            }
            if (backward)
            {
                //TODO el control de las luces moverlas a otro sitio por que no le afectan las colisiones
                if (rb.velocity.magnitude > 1f && angle < 90)
                {

                    clc.brake(true);
                }
                else
                {
                    clc.brake(false);
                }

                rb.AddForce(gameObject.transform.forward * -accelForce);

            }else
            {
                clc.brake(false);
            }
            if (left)
            {
                rb.MoveRotation(gameObject.transform.rotation * Quaternion.Euler(0, -turnSpeed * Time.fixedDeltaTime, 0));
            }
            if (right)
            {
                rb.MoveRotation(gameObject.transform.rotation * Quaternion.Euler(0, turnSpeed * Time.fixedDeltaTime, 0));
            }
        } else
        {
            rb.drag = 0;
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

    //TODO cambiar cuando ponga los inputs
    public bool getBraking()
    {
        return backward;
    }
}
