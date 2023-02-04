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

    [SerializeField] float durationBetweenSteps;

    private PlayerDirection playerDirection;

    private float elapsedDuration;



    // Start is called before the first frame update
    void Start()
    {
        playerDirection = PlayerDirection.Left;
        elapsedDuration = 0f;
    }

    // Update is called once per frame

    void Update()
    {
        elapsedDuration += Time.deltaTime;

        if (elapsedDuration < durationBetweenSteps)
        {
            return;
        }

        elapsedDuration -= durationBetweenSteps;

        // get Horizontal input
        float horizontalInput = Input.GetAxis("Horizontal");

        // if horizontal input is 0 then return
        if (Mathf.Abs(horizontalInput) < 0.8f)
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
        transform.localPosition = new Vector2(transform.localPosition.x + (Mathf.Sign(horizontalInput) * MovementStep), transform.localPosition.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // if the player collides with an object tagged FallingText then destroy the player
        if (collision.gameObject.tag == "FallingElement")
        {
            Debug.Log("Player collided with FallingElement");

            // Destroy the gameobject linked to the collision
            Destroy(collision.gameObject);

            // TODO handle RAM usage or opening folder
        }
    }
}
