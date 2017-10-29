using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof (Controller2D))]
public class Player : MonoBehaviour {


    public float jumpHeight = 4;
    public float timeToJumpApex = .4f;
    float jumpVelocity;

    //Event for BombPlacer
    public UnityEvent BombAction;

    SpriteRenderer spriteRenderer;
    Controller2D controller;
    float gravity ;
    float moveSpeed = 6; 
    Vector3 velocity;
    private Vector3 lookFacing ;
    public Vector3 LookFacing { get { return lookFacing ; } private set{; }  }
    AudioSource audioSource;

//    Weapon weapon;

    Animator animator;
	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        controller = GetComponent<Controller2D>();
        //Formular from E.03 SL 9min
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
//        print("Gravity: " + gravity + " Jump Velocity: " + jumpVelocity);
        animator = GetComponent<Animator>();
 //       weapon = GetComponent<Weapon>();
	}
	
	// Update is called once per frame
	void Update () {

        if (controller.collinfo.below || controller.collinfo.above)
        {
            velocity.y = 0;
        }

        //Now set jumpVelocity if grounded
        if (Input.GetKeyDown(KeyCode.UpArrow) && controller.collinfo.below)
        { velocity.y = jumpVelocity; }
        //To prevent accumulating velocity, when on the ground
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));



        animator.SetFloat("speedx", input.x);
        animator.SetFloat("speedy", input.y);
        velocity.x = input.x * moveSpeed;
        velocity.y = input.y * moveSpeed;
        controller.Move(velocity * Time.deltaTime);
        if (input.x == -1)
        { spriteRenderer.flipX = false;
            lookFacing   = Vector3.left;
        }
        else if  (input.x ==1)
        {
            spriteRenderer.flipX = true;
            lookFacing   = Vector3.right;
        }
        else 
        {
            lookFacing = Vector3.zero;

        }
            //YT
    		if (Input.GetKeyDown(KeyCode.X))
            {
            //Subscribed by bombplacer
                BombAction.Invoke();
            }
		
	}
}
