using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] LayerMask _damageLayers;
    [SerializeField] float _maxHealth;
    [SerializeField] float _currentHealth;
    [SerializeField] UnityEvent _onEntityDead;
    void Start()
    {
        _currentHealth = _maxHealth;
    }
    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            _onEntityDead.Invoke();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object's layer is part of the LayerMask
        if (((1 << collision.gameObject.layer) & _damageLayers) != 0)
        {
            float defense = 1f;
            if (gameObject.CompareTag("player"))
            {
                defense = collision.otherCollider.GetComponent<ShipComponent>().GetDefense();
            }
            //Probably will change later; calculating damage based on momentum
            TakeDamage(collision.rigidbody.mass * collision.relativeVelocity.magnitude * (1 - (defense / 100)));
            float damage = collision.rigidbody.mass * collision.relativeVelocity.magnitude * (1 - (defense / 100));
            Debug.Log(gameObject.name + " took " + damage + " damage!");
        }
    }
}
