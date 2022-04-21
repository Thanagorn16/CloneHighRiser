using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f;

    void Update()
    {
        Run();
    }

    void Run()
    {
        transform.Translate(new Vector2(moveSpeed,0) * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //flip player
        moveSpeed = -moveSpeed;
        transform.localScale = new Vector2(Mathf.Sign(-(transform.position.x)), 1f);
    }

}
