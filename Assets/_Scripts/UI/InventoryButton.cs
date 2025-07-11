using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] ShipComponent _associatedComponent;
    [SerializeField] Image buttonSprite;
    public void SetComponent(ShipComponent component)
    {
        _associatedComponent = component;
    }
    public void SetPosition(Vector2 pos)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.localPosition = pos;
    }

    private Draggable currentDraggable;
    public void OnPointerDown(PointerEventData eventData)
    {
        currentDraggable = ShipConstructManager.Instance.CreateDraggableComponent(_associatedComponent);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        Vector3Int mouseGridPosition = ShipConstructManager.Instance.GetQuantizedMousePosition();
        if (ShipBuildData.Instance.Grid.AddComponent(_associatedComponent, (Vector2Int)mouseGridPosition, currentDraggable.transform.rotation))
        {
            ComponentInventory.Instance.RemoveFromInventory(_associatedComponent);
            ComponentInventory.Instance.LoadInventory();
            ShipBuildData.Instance.BuildShip(Vector2.zero);
            Destroy(gameObject);
        }
    }
    void Start()
    {
        Sprite sourceSprite = _associatedComponent.GetComponent<SpriteRenderer>().sprite;
        buttonSprite.sprite = sourceSprite;
    }
}
