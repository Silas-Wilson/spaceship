using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class ShipBuildData : MonoBehaviour
{
    public static ShipBuildData Instance;
    public ComponentGrid Grid { get; private set; }
    private ShipStats _activeShip;
    [SerializeField] ShipStats ShipPrefab;
    [SerializeField] ShipComponent CorePrefab;
    [SerializeField] ShipComponent GunPrefabFOR_DEBUG;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        Grid = new ComponentGrid();
    }
    void Start()
    {
        Grid.AddComponent(CorePrefab, Vector2Int.zero);

        //FOR TESTING PURPOSES:
        Grid.AddComponent(GunPrefabFOR_DEBUG, new Vector2Int(0, 1));
        Grid.AddComponent(GunPrefabFOR_DEBUG, new Vector2Int(0, -1));

        BuildShip(Vector2.zero);

        Grid.AddComponent(GunPrefabFOR_DEBUG, new Vector2Int(1, 0));
        Grid.AddComponent(GunPrefabFOR_DEBUG, new Vector2Int(-1, 0));

        BuildShip(Vector2.zero);

        Grid.AddComponent(GunPrefabFOR_DEBUG, new Vector2Int(2, 0));
        Grid.AddComponent(GunPrefabFOR_DEBUG, new Vector2Int(-2, 0));

        BuildShip(Vector2.zero);
    }
    public void BuildShip(Vector2 location)
    {
        if (_activeShip == null)
        {
            _activeShip = Instantiate(ShipPrefab, location, Quaternion.identity);
        }
        _activeShip.UpdateShipStats();
        BuildShipComponents();
    }
    private void BuildShipComponents()
    {
        //Destroy all current components before adding components
        foreach (Transform child in _activeShip.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (var pair in Grid.GetAllValues())
        {
            Vector2Int localPosition = pair.Key;
            ShipComponent component = pair.Value;

            ShipComponent componentAdded = Instantiate(component, _activeShip.transform);
            componentAdded.transform.localPosition = new Vector3Int(localPosition.x, localPosition.y, 0);
        }
    }
}

public class ComponentGrid
{
    private Dictionary<Vector2Int, ShipComponent> _grid = new();
    public bool AddComponent(ShipComponent component, Vector2Int location)
    {
        if (_grid.ContainsKey(location))
        {
            Debug.LogWarning($"Cannot add: Location {location} already occupied.");
            return false;
        }
        _grid[location] = component;
        return true;
    }
    public IEnumerable<KeyValuePair<Vector2Int, ShipComponent>> GetAllValues()
    {
        return _grid;
    }
    public ShipComponent[] GetAllComponents()
    {
        return _grid.Values.ToArray();
    }
}