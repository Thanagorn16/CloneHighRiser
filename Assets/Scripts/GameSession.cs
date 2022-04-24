using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    public int health = 3;
    public float OffFloorTime = 5f;
    [SerializeField] TextMeshProUGUI liveText;
    

    void Awake() // this is singleton pattern
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;

        if(numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        liveText.text = health.ToString("Live : " + health);
    }

    public int CurrentHealth()
    {
        return health;
    }

    public void LoseHealth()
    {
        if(health > 1)
        {
            health--;
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
            liveText.text = health.ToString("Live : " + health);
        }
        else
        {
            ResetGameSession();
        }
        // health--;
        // int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        // SceneManager.LoadScene(currentSceneIndex);
    }

    void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

}
