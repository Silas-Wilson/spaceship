using System;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    [SerializeField] CircleCollider2D col;
    private GameObject player;
    public bool IsPlayerVisible { get; private set; }
    private const string _PLAYER_TAG = "player";

    void Awake()
    {
        IsPlayerVisible = false;
        player = null;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(_PLAYER_TAG))
        {
            IsPlayerVisible = true;
            player = collision.gameObject;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(_PLAYER_TAG))
        {
            IsPlayerVisible = false;
        }
    }
    public GameObject TryGetPlayer()
    {
        return player;
    }
}
