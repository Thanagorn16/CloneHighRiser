using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    public int health = 3;
    public float OffFloorTime = 5f;
    Player player;
    Coroutine coroutine;

    public int CurrentHealth()
    {
        return health;
    }

    public int LoseHealth()
    {
        return health--;
    }

}
