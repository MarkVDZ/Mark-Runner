using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private bool moveLeft = false;
    private bool moveRight = false;
    private float jumpCap = 1;
    private float leftLane = -3;
    private float centerLane = 0;
    private float rightLane = 3;
    private bool isSliding = false;
    private bool isJumping = false;
    private const float Move = 3;
    private float timer = 0;
    private float moveDelay = 0;
    //public float jumping;
    //public float movingHorizontal;
    public float speed = 10;


    // Use this for initialization
    void Start () {
        //jumping = Input.GetAxis("Jump");
        //movingHorizontal = Input.GetAxis("Horizontal");
        //timer = Time.deltaTime;
    }
	
	// Update is called once per frame
	void Update () {
        timer = 60 * Time.deltaTime;
        //print(timer);

        float jumping = Input.GetAxis("Jump"); 
        float movingHorizontal = Input.GetAxisRaw("Horizontal");
        float sliding = Input.GetAxis("Vertical");

        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;
        
        if(jumping > 0)
        {
            Jump();
        }

        if (movingHorizontal > 0 && moveLeft == false && moveRight == false && moveDelay <= 0) {
            //print(movingHorizontal);
            moveRight = true;

            //SwapLanes();
        }
        else if (movingHorizontal < 0 && moveRight == false && moveLeft == false && moveDelay <= 0)
        {
            moveLeft = true;
            //print(movingHorizontal);

           // SwapLanes();
        }
        print(moveLeft);
        print(moveRight);

        if (moveRight == true || moveLeft == true)
        {
            SwapLanes();
            moveDelay = .5f;
        }

        if (sliding < 0)
        {
            isSliding = true;
            Slide();
        }
        else
        {
            isSliding = false;
        }

        if(transform.localPosition.y >= jumpCap)
        {
            transform.localPosition = new Vector3(0, jumpCap);
        }

        if(transform.localPosition.z >= 3.1)
        {
            //transform.localPosition = new Vector3(0, 0, rightLane);
            transform.Translate(0, 0, leftLane);
            print("LOCKED TO THE RIGHT!!!!!!!!!!!");
        }
        if(transform.localPosition.z <= -3.1)
        {
            //transform.localPosition = new Vector3(0, 0, leftLane);
            transform.Translate(0, 0, rightLane);
            print("LOCKED TO THE LEFT!!!!!!!!!!!!!!");
        }

        if(isSliding == false)
        {
            transform.localScale = Vector3.one;
        }

        print(transform.localPosition);
        
        if(moveDelay > 0)
        {
            moveDelay = moveDelay - Time.deltaTime;
        }
        print(moveDelay);
	}

    void Jump()
    {
        print("Jumping");
        transform.Translate(new Vector3(0, 0 + Time.deltaTime, 0));

    }

    void Slide()
    {
        print("Sliding");
        
        transform.localScale = new Vector3(transform.localScale.x, .5f, transform.localScale.z);
    }

    void SwapLanes()
    {
        float moveAmount = 0;
        if (moveRight == true)
        {
            print("Move Right");
            /*moveAmount =+ .01f;
            transform.Translate(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + moveAmount);
            if(transform.localPosition.z >= 3)
            {
                moveRight = false;
                moveAmount = 0;
            }*/
            transform.Translate(transform.localPosition.x, transform.localPosition.y, rightLane);
            moveRight = false;
            //moveDelay = 10;

        }
        if (moveLeft == true)
        {
            print("Move Left");
            /*moveAmount =- .01f;
            transform.Translate(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + moveAmount);
            if(transform.localPosition.z <= -3)
            {
                moveLeft = false;
                moveAmount = 0;
            }*/
            transform.Translate(transform.localPosition.x, transform.localPosition.y, leftLane);
            moveLeft = false;
            //moveDelay = 10;
        }

        //print(moveAmount);
    }

}
