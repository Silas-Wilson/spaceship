using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Input { get; private set; }

    [field: SerializeField] private InputAction Steer;
    [field: SerializeField] private InputAction Accelerate;
    [field: SerializeField] public InputAction Action1 { get; private set; }
    public bool IsAccelerating { get; private set; }
    public int SteerDirection { get; private set; }
    public event Action OnAct1Pressed;
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
        Action1.Enable();

        Steer.performed += OnSteerPerformed;
        Steer.canceled += OnSteerCanceled;

        Accelerate.performed += OnAcceleratePerformed;
        Accelerate.canceled += OnAccelerateCanceled;

        Action1.performed += ctx => OnAct1Pressed?.Invoke();
        Action1.Enable();
    }
    void OnDisable()
    {
        Steer.Disable();
        Accelerate.Disable();
        Action1.Disable();

        Steer.performed -= OnSteerPerformed;
        Steer.canceled -= OnSteerCanceled;

        Accelerate.performed -= OnAcceleratePerformed;
        Accelerate.canceled -= OnAccelerateCanceled;

        Action1.performed -= ctx => OnAct1Pressed?.Invoke();
        Action1.Disable();
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
