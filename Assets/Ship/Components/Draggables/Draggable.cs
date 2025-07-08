using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;

public class Draggable : MonoBehaviour
{
    [SerializeField] SpriteRenderer sr;
    [SerializeField] InputAction rotate;
    void OnEnable()
    {
        rotate.Enable();

        rotate.performed += OnRotate;
    }
    void OnDisable()
    {
        rotate.Disable();

        rotate.performed -= OnRotate;
    }
    private void OnRotate(InputAction.CallbackContext context)
    {
        transform.Rotate(0f, 0f, 90f);
    }
    public void SetComponent(ShipComponent component)
    {
        sr.sprite = component.GetComponent<SpriteRenderer>().sprite;
    }
    void Update()
    {
        Vector3Int mouseGridPosition = ShipConstructManager.Instance.GetQuantizedMousePosition();
        transform.position = mouseGridPosition;
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            Destroy(gameObject);
        }
    }
}
