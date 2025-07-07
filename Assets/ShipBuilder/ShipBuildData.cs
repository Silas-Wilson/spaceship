using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class ShipBuildData : MonoBehaviour
{
    public static ShipBuildData Instance;
    public ComponentGrid Grid { get; private set; }
    private ComponentGrid _defaultGrid;
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

        BuildShip(Vector2.zero);
    }
    public ShipStats BuildShip(Vector2 location)
    {
        if (_activeShip == null)
        {
            _activeShip = Instantiate(ShipPrefab, location, Quaternion.identity);
        }
        _activeShip.UpdateShipStats();
        BuildShipComponents();
        return _activeShip;
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
        /**
        if (_grid.ContainsKey(location))
        {
            return false;
        }
        _grid[location] = component;
        return true;
        **/

        // _grid.Count < 1 is here to allow the first component (The core) to be placed.
        if (IsInValidLocation(location))
        {
            _grid[location] = component;
            return true;
        }
        return false;
    }
    public IEnumerable<KeyValuePair<Vector2Int, ShipComponent>> GetAllValues()
    {
        return _grid;
    }
    public ShipComponent[] GetAllComponents()
    {
        return _grid.Values.ToArray();
    }
    private bool IsInValidLocation(Vector2Int location)
    {
        // Allows the first component (The core) to be added no matter what.
        if (_grid.Count < 1)
        {
            return true;
        }
        bool isComponentUp = _grid.ContainsKey(location + Vector2Int.up);
        bool isComponentDown = _grid.ContainsKey(location + Vector2Int.down);
        bool isComponentRight = _grid.ContainsKey(location + Vector2Int.right);
        bool isComponentLeft = _grid.ContainsKey(location + Vector2Int.left);
        if ((isComponentUp || isComponentDown || isComponentRight || isComponentLeft) && !_grid.ContainsKey(location))
        {
            return true;
        }
        Debug.LogWarning($"Location {location} is invalid!");
        return false;
    }
}