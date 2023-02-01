using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField]
    float accelForce;

    [SerializeField]
    float turnSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
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
    private void FixedUpdate()
    {
        prevPos = posPos;
        if (forward)
        {
            //rb.MovePosition(gameObject.transform.position + gameObject.transform.forward * accelForce * Time.fixedDeltaTime);
            rb.AddForce(gameObject.transform.forward*accelForce);
        }
        if (backward)
        {
            rb.AddForce(gameObject.transform.forward * -accelForce);
            //rb.MovePosition(gameObject.transform.position + gameObject.transform.forward * -accelForce * Time.fixedDeltaTime);
        }
        if (left)
        {
            rb.MoveRotation(gameObject.transform.rotation * Quaternion.Euler(0, -turnSpeed*Time.fixedDeltaTime, 0));
        }
        if (right)
        {
            rb.MoveRotation(gameObject.transform.rotation * Quaternion.Euler(0, turnSpeed*Time.fixedDeltaTime, 0));
        }

        posPos = transform.position;
    }

    public Vector3 getMovementVector()
    {
        return posPos - prevPos;
    }
}
