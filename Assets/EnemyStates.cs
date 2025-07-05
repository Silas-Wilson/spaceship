using System.Resources;
using UnityEngine;

public class EnemyStates : MonoBehaviour
{
    public enum State
    {
        idle,
        active
    }
    private State _currentState;
    private void Awake()
    {
        _currentState = State.idle;
    }
    public void SetState(State state)
    {
        _currentState = state;
    }
    public State GetState()
    {
        return _currentState;
    }
}
