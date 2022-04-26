using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    GameSession gameSession;
    bool isCollected; 
    [SerializeField] int coinValue = 100;

    void Awake()
    {
        // gameSession = FindObjectOfType<GameSession>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !isCollected)   
        {
            // gameSession.AddScore(coinValue);
            FindObjectOfType<GameSession>().AddScore(coinValue);
            isCollected = true; // since the player has 2 collisions we need to make sure that there'll be only one collision to the coin
            Destroy(gameObject); 
        }
    }
}
