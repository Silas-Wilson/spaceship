using UnityEngine;
using UnityEngine.InputSystem;

public class Draggable : MonoBehaviour
{
    [SerializeField] ShipComponent _associatedComponent;
    public void SetComponent(ShipComponent component)
    {
        _associatedComponent = component;
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
