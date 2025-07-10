using UnityEngine;

public class ShipComponent : MonoBehaviour
{
    [field: SerializeField] public float mass { get; private set; }
    [field: SerializeField] public float defense { get; private set; }
    [field: SerializeField] public float bonusHP { get; private set; }
    [field: SerializeField] public float bonusAcc { get; private set; }
    [field: SerializeField] public float bonusRotAcc { get; private set; }
    [field: SerializeField] public float bonusMax { get; private set; }
    [field: SerializeField] public float bonusRotMax { get; private set; }
}
