using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float jumpSpeed = 1f;
    [SerializeField] float delay = 1f;
    Rigidbody2D rb; 
    CapsuleCollider2D bodyCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
    }

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

    void SetAllCollidersState(bool active) // disable all of the collisions of the gameObject
    {
        foreach(Collider2D collider in GetComponents<Collider2D>())
        {
            collider.enabled = active;
        }
    }

    IEnumerator ReturnColliders(bool active) //set time before re-active the colliders
    {
        yield return new WaitForSeconds(delay);

        foreach(Collider2D collider in GetComponents<Collider2D>())
        {
            collider.enabled = active;
        }
    }

}
