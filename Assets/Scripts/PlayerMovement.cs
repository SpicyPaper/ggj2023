using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum PlayerDirection
{
    Left,
    Right
}


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] int MovementStep;
    // Start is called before the first frame update

    [SerializeField] int updateBetweenSteps;

    private PlayerDirection playerDirection;

    private int updateBetweenStepsCounter;


    // Start is called before the first frame update
    void Start()
    {
        playerDirection = PlayerDirection.Left;

        updateBetweenStepsCounter = 0;
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        if (updateBetweenStepsCounter < updateBetweenSteps)
        {
            updateBetweenStepsCounter++;
            return;
        }

        updateBetweenStepsCounter = 0;


        // get Horizontal input
        float horizontalInput = Input.GetAxis("Horizontal");

        // if horizontal input is 0 then return
        if (horizontalInput == 0)
        {
            return;
        }

        // move left or right
        PlayerDirection directionInCurrentFrame = horizontalInput < 0 ? PlayerDirection.Left : PlayerDirection.Right;

        if (directionInCurrentFrame != playerDirection)
        {
            // multiply x scale by -1 to flip the sprite
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            playerDirection = directionInCurrentFrame;
        }

        // move the player on local position
        transform.localPosition = new Vector3(transform.localPosition.x + (horizontalInput * MovementStep), transform.localPosition.y, transform.localPosition.z);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // if the player collides with an object tagged FallingText then destroy the player
        if (collision.gameObject.tag == "FallingText")
        {
            Debug.Log("Player collided with FallingText");

            // Destroy the gameobject linked to the collision
            Destroy(collision.gameObject);

            // TODO handle RAM usage or opening folder
        }
    }
}
