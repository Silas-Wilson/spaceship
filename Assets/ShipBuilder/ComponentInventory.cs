using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComponentInventory : MonoBehaviour
{
    public static ComponentInventory Instance;
    [SerializeField] int _inventorySize;
    [SerializeField] InventoryButton _button;
    [SerializeField] Image _inventoryUI;
    [field: SerializeField] public List<ShipComponent> components { get; private set; }
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
    }
    void Start()
    {
        LoadInventory();
    }
    public void LoadInventory()
    {
        for (int i = 0; i < components.Count; i++)
        {
            ShipComponent component = components[i];
            InventoryButton button = Instantiate(_button, _inventoryUI.transform);
            button.SetComponent(component);
            button.SetPosition(i);
        }
    }
}
