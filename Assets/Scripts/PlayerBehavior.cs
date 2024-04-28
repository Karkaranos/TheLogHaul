using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour
{
    public float gravity;
    public float speed;
    public int smogMultiplier;
    public Vector2 Jump;

    public bool canClimb;
    public bool canjump;

    public bool jumping;
    public bool running;
    public bool falling;
    public bool climbing;

    public Animator animator;

    public Coroutine MovementCoroutineInstance;

    public InputAction Move;
    public InputAction jump;

    public PlayerInput MyPlayerInput;
    private Rigidbody2D myRB;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

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
            jumping = true;
            falling = false;
            myRB.AddForce(Jump, ForceMode2D.Impulse);
            Invoke("stopMomentum", .5f);
            canjump = false;
        }
    }

    private void Move_Cancelled(InputAction.CallbackContext obj)
    {
        running = false;
        StopCoroutine(MovementCoroutineInstance);
    }

    private void Move_started(InputAction.CallbackContext obj)
    {
        running = true;
        MovementCoroutineInstance = StartCoroutine(moving());
    }

    // Update is called once per frame
    void Update()
    {
        if(myRB.velocity.y <= 0)
        {
            jumping = false;
            if(myRB.velocity.y < -.5)
            {
                falling = true;
            }
        }
        if(myRB.velocity.y >= 0)
        {
            falling= false;
        }
    }

    private void FixedUpdate()
    {
        if (falling)
        {
            animator.SetBool("Running", false);
            animator.SetBool("Jumping", false);
            animator.SetBool("Falling", true);
            animator.SetBool("Climbing", false);
        }
        else if (jumping)
        {
            animator.SetBool("Running", false);
            animator.SetBool("Jumping", true);
            animator.SetBool("Falling", false);
            animator.SetBool("Climbing", false);
        }
        else if (climbing)
        {
            animator.SetBool("Running", false);
            animator.SetBool("Jumping", false);
            animator.SetBool("Falling", false);
            animator.SetBool("Climbing", true);
        }
        else if (running)
        {
            animator.SetBool("Running", true);
            animator.SetBool("Jumping", false);
            animator.SetBool("Falling", false);
            animator.SetBool("Climbing", false);
        }
        else
        {
            animator.SetBool("Running", false);
            animator.SetBool("Jumping", false);
            animator.SetBool("Falling", false);
            animator.SetBool("Climbing", false);
        }
    }

    public IEnumerator moving()
    {
        while (true)
        {
            Vector2 direction = Move.ReadValue<Vector2>();
            if (direction.x < -0.1)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (direction.x > 0.1)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
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
            jumping = false;
            canjump = true;
            myRB.velocity = Vector2.zero;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.transform.position.y < gameObject.transform.position.y)
        {
            canjump = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Tree")
        {
            jumping = false;
            climbing= true;
            Invoke("stopMomentum", .3f);
        }
        else if(collision.tag == "smog")
        {
            speed = speed / smogMultiplier;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Tree")
        {
            if(myRB.velocity.y < .5)
            {
                jumping = false;
                canjump = true;
            }
            canClimb = true;
            myRB.gravityScale = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Tree")
        {
            climbing = false;
            canjump = false;
            canClimb = false;
            myRB.gravityScale = gravity;
        }
        else if (collision.tag == "smog")
        {
            speed = speed * smogMultiplier;
        }
    }

    public void giveJump()
    {

    }
}
