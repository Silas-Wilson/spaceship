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
            ShipBuildData.Instance.Grid.AddComponent(_associatedComponent, (Vector2Int)mouseGridPosition);
            ShipBuildData.Instance.BuildShip(Vector2.zero);
            Destroy(gameObject);
        }
    }
}
