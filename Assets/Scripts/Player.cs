using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float jumpSpeed = 1f;
    [SerializeField] float delayCollider = 1f;
    [SerializeField] Animator myAnimator;
    Rigidbody2D rb; 
    CapsuleCollider2D bodyCollider;
    bool isRunning = true;
    GameSession gameSession;
    Coroutine coroutine;

    void Awake()
    {
        bodyCollider = GetComponent<CapsuleCollider2D>();
        gameSession = FindObjectOfType<GameSession>();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Run();
    }

    void Run()
    {
        if(isRunning)
        {
            transform.Translate(new Vector2(moveSpeed,0) * Time.deltaTime);
        }
        else
        {
            rb.velocity = Vector2.zero;
            myAnimator.SetBool("isIdling", true);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Goal") //stop running if reach the goal
        {
            isRunning = false;
        }

        if(bodyCollider.IsTouchingLayers(LayerMask.GetMask("Floor", "Goal")) && coroutine != null) //stop coroutine for deducting player's life
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }

    }

    void OnCollisionExit2D(Collision2D other)
    {
        if(!bodyCollider.IsTouchingLayers(LayerMask.GetMask("Floor", "Goal")) && coroutine == null) 
        {
            coroutine = StartCoroutine(OutOfBuiding());
        }
        // print("in exit");
        // coroutine = StartCoroutine(OutOfBuiding());
        // StartCoroutine(OutOfBuiding());
    }

    IEnumerator OutOfBuiding()// if player is not touching floor for a certain period --> lose life
    {
        if(!bodyCollider.IsTouchingLayers(LayerMask.GetMask("Floor")))
        {
            yield return new WaitForSeconds(gameSession.OffFloorTime);

            gameSession.LoseHealth();
            // print(gameSession.health);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //flip player if it hit the wall of the building
        if(other.tag == "Side")
        {
            moveSpeed = -moveSpeed;
            transform.localScale = new Vector2(Mathf.Sign(-(transform.position.x)), 1f);
        }
    }

    void OnJump(InputValue value)
    {
        if(!bodyCollider.IsTouchingLayers(LayerMask.GetMask("Floor")))
        {
            return;
        }

        if(value.isPressed)
        {
            // rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse); //this will make the jump to have the same speed until the end of the action
            //however, since we make sure that the player can jump once at a time, the below is enough
            rb.velocity += new Vector2(0f, jumpSpeed);
            SetAllCollidersState(false);
            StartCoroutine(ReturnColliders(true));
        }
    }

    // this method is not quite efficient. Will work on new one later
    void SetAllCollidersState(bool active) // disable all of the collisions of the gameObject
    {
        foreach(Collider2D collider in GetComponents<Collider2D>())
        {
            collider.enabled = active;
        }
    }

    IEnumerator ReturnColliders(bool active) //set time before re-active the colliders
    {
        yield return new WaitForSeconds(delayCollider);

        foreach(Collider2D collider in GetComponents<Collider2D>())
        {
            collider.enabled = active;
        }
    }

}
