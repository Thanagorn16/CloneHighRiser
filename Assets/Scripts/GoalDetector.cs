using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalDetector : MonoBehaviour
{
    [SerializeField] ParticleSystem getGoalEffect;
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Goal")
        {
            getGoalEffect.Play();
        }
    }
}
