using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipConstructManager : MonoBehaviour
{
    public static ShipConstructManager Instance;
    Camera mainCam;
    [SerializeField] Draggable DEBUG_COMPONENT;

    [SerializeField] InventoryButton button;
    [SerializeField] Draggable draggablePrefab;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        mainCam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            ShipComponent removedComponent = ShipBuildData.Instance.Grid.RemoveComponent(GetQuantizedMousePosition());
            if (removedComponent != null)
            {
                ComponentInventory.Instance.AddToInventory(removedComponent);
                ComponentInventory.Instance.LoadInventory();
                ShipBuildData.Instance.BuildShip(Vector2.zero);
            }
        }
    }
    public Vector3Int GetQuantizedMousePosition()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector2 worldPos = mainCam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y)); // z = 0 for 2D
        Vector3Int quantizedPosition = new Vector3Int((int)Math.Round(worldPos.x), (int)Math.Round(worldPos.y), 0);
        return quantizedPosition;
    }
    public void CreateDraggableComponent(ShipComponent component)
    {
        Draggable thisDraggable = Instantiate(draggablePrefab, GetQuantizedMousePosition(), Quaternion.identity);
        thisDraggable.SetComponent(component);
    }
}
