using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour
{
    public float gravity;
    public float speed;
    public Vector2 Jump;

    public bool canClimb;
    public bool canjump;

    public Coroutine MovementCoroutineInstance;

    public InputAction Move;
    public InputAction jump;

    public PlayerInput MyPlayerInput;
    private Rigidbody2D myRB;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();

        Move = MyPlayerInput.currentActionMap.FindAction("Move");
        jump = MyPlayerInput.currentActionMap.FindAction("Jump");

        Move.started += Move_started;
        Move.canceled += Move_Cancelled;
        jump.performed += jump_performed;
    }

    private void jump_performed(InputAction.CallbackContext obj)
    {
        if(canjump)
        {
            myRB.AddForce(Jump, ForceMode2D.Impulse);
            Invoke("stopMomentum", .5f);
            canjump = false;
        }
    }

    private void Move_Cancelled(InputAction.CallbackContext obj)
    {
        StopCoroutine(MovementCoroutineInstance);
    }

    private void Move_started(InputAction.CallbackContext obj)
    {
        MovementCoroutineInstance = StartCoroutine(moving());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator moving()
    {
        while (true)
        {
            Vector2 direction = Move.ReadValue<Vector2>();
            if (canClimb)
            {
                transform.position = transform.position + new Vector3(direction.x * speed * Time.deltaTime, direction.y * speed * Time.deltaTime, 0);
            }
            else
            {
                transform.position = transform.position + new Vector3(direction.x * speed * Time.deltaTime, 0, 0);
            }
            yield return null;
        }
    }

    private void stopMomentum()
    {
        if(canClimb)
        {
            canjump = true;
            myRB.velocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        canjump = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Tree")
        {
            Invoke("stopMomentum", .3f);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Tree")
        {
            canjump = true;
            canClimb = true;
            myRB.gravityScale = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Tree")
        {
            canjump = false;
            canClimb = false;
            myRB.gravityScale = gravity;
        }
    }
}
