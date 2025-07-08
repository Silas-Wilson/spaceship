using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Input { get; private set; }

    [field: SerializeField] private InputAction Steer;
    [field: SerializeField] private InputAction Accelerate;
    [field: SerializeField] public InputAction Action1 { get; private set; }
    [field: SerializeField] public InputAction Action2 { get; private set; }
    public bool IsAccelerating { get; private set; }
    public int SteerDirection { get; private set; }
    public event Action OnAct1Pressed;
    public event Action OnAct2Pressed;
    void Awake()
    {
        if (Input != null && Input != this)
        {
            Destroy(gameObject); //prevent duplicates
        }
        else
        {
            Input = this;
        }
        
    }
    void OnEnable()
    {
        Steer.Enable();
        Accelerate.Enable();

        Steer.performed += OnSteerPerformed;
        Steer.canceled += OnSteerCanceled;

        Accelerate.performed += OnAcceleratePerformed;
        Accelerate.canceled += OnAccelerateCanceled;

        Action1.performed += ctx => OnAct1Pressed?.Invoke();
        Action1.Enable();
        Action2.performed += ctx => OnAct2Pressed?.Invoke();
        Action2.Enable();
    }
    void OnDisable()
    {
        Steer.Disable();
        Accelerate.Disable();

        Steer.performed -= OnSteerPerformed;
        Steer.canceled -= OnSteerCanceled;

        Accelerate.performed -= OnAcceleratePerformed;
        Accelerate.canceled -= OnAccelerateCanceled;

        Action1.performed -= ctx => OnAct1Pressed?.Invoke();
        Action1.Disable();
        Action2.performed -= ctx => OnAct2Pressed?.Invoke();
        Action2.Disable();
    }

    void OnAcceleratePerformed(InputAction.CallbackContext context)
    {
        IsAccelerating = true;
    }
    void OnAccelerateCanceled(InputAction.CallbackContext context)
    {
        IsAccelerating = false;
    }
    void OnSteerPerformed(InputAction.CallbackContext context)
    {
        SteerDirection = (int)Mathf.Sign(context.ReadValue<float>());
    }
    void OnSteerCanceled(InputAction.CallbackContext context)
    {
        SteerDirection = 0;
    }
}
