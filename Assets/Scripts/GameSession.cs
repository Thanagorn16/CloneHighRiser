using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] int health = 3;
    public float OffFloorTime = 5f;
    [SerializeField] TextMeshProUGUI liveText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] int score = 0;
    

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
        scoreText.text = score.ToString("Score : " + score);
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
    }

    public void AddScore(int coinValue)
    {
        score += coinValue;
        scoreText.text = "Score : " + score.ToString(); // this way of converting int to string is more efficient and has a lesser chance to create bug than the way I do in LostHealth.
    }

    void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

}
