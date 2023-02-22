using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarControl : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset inputActions;

    InputAction steerAction;
    InputAction accelAction;
    InputAction brakeAction;
    InputAction startAction;
    InputAction lightAction;
    InputAction hornAction;

    float steerInput;
    float accelInput;
    float brakeInput;
    

    bool startInput;
    bool lightInput;
    bool hornInput;

    static CarControl instance = null;
    public static CarControl getInstance()
    {
        return instance;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        accelAction = inputActions.FindAction("Forward");
        brakeAction = inputActions.FindAction("Backward");
        steerAction = inputActions.FindAction("Steer");
        startAction = inputActions.FindAction("Start");
        lightAction = inputActions.FindAction("TurnLights");
        hornAction = inputActions.FindAction("Horn");
    }

    private void Update()
    {
        accelInput = Mathf.Clamp(accelAction.ReadValue<float>(), 0, 1);
        brakeInput = -Mathf.Clamp(brakeAction.ReadValue<float>(), 0, 1);
        steerInput = Mathf.Clamp(steerAction.ReadValue<Vector2>().x, -1, 1);

        startInput = startAction.triggered;
        lightInput = lightAction.triggered;
        hornInput = hornAction.triggered;
        

    }

    public float getAccelInput()
    {
        return accelInput;
    }
    public float getBrakeInput()
    {
        return brakeInput;
    }
    public float getSteerInput()
    {
        return steerInput;
    }
    public bool getStartInput()
    {
        return startInput;
    }
    public bool getLightsInput()
    {
        return lightInput;
    }
    public bool getHornInput()
    {
        return hornInput;
    }


}
