using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] float _maxHealth;
    [SerializeField] float _currentHealth;
    [SerializeField] UnityEvent _onEntityDead;
    void Start()
    {
        _currentHealth = _maxHealth;
    }
    public void SetMaxHealth(float value)
    {
        _maxHealth = value;
        _currentHealth = _maxHealth;
    }
    public float GetHealth()
    {
        return _currentHealth;
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
}
