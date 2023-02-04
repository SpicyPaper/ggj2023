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
    [SerializeField]
    int MovementStep;

    // Start is called before the first frame update

    [SerializeField]
    float durationBetweenSteps;

    [SerializeField]
    float minXPosition;

    [SerializeField]
    float maxXPosition;

    private PlayerDirection playerDirection;

    private float elapsedDuration;

    private float previousAbsHorizontalInput;

    // Start is called before the first frame update
    void Start()
    {
        playerDirection = PlayerDirection.Left;
        elapsedDuration = durationBetweenSteps;
    }

    // Update is called once per frame

    void Update()
    {
        if (GameHandler.instance.GetGameStatus() != GameStatus.InGame)
        {
            return;
        }

        elapsedDuration += Time.deltaTime;

        float horizontalInput = Input.GetAxis("Horizontal");

        // if horizontal input is 0 then return
        if (previousAbsHorizontalInput > 0.01f && Mathf.Abs(horizontalInput) < 0.8f)
        {
            previousAbsHorizontalInput = Mathf.Abs(horizontalInput);
            return;
        }

        previousAbsHorizontalInput = Mathf.Abs(horizontalInput);
        if (Mathf.Abs(horizontalInput) <= 0.01f || elapsedDuration < durationBetweenSteps)
        {
            return;
        }

        elapsedDuration = 0;

        // move left or right
        PlayerDirection directionInCurrentFrame =
            horizontalInput < 0 ? PlayerDirection.Left : PlayerDirection.Right;

        if (directionInCurrentFrame != playerDirection)
        {
            // multiply x scale by -1 to flip the sprite
            transform.localScale = new Vector3(
                transform.localScale.x * -1,
                transform.localScale.y,
                transform.localScale.z
            );
            playerDirection = directionInCurrentFrame;
        }

        // move the player on local position
        transform.localPosition = new Vector2(
            Mathf.Clamp(
                transform.localPosition.x + (Mathf.Sign(horizontalInput) * MovementStep),
                minXPosition,
                maxXPosition
            ),
            transform.localPosition.y
        );
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // if the player collides with an object tagged FallingText then destroy the player
        if (collision.gameObject.tag == "FallingElement")
        {
            // Destroy the gameobject linked to the collision
            Destroy(collision.gameObject);

            // call the GameHandler to increase the ram usage
            GameHandler.instance.AddReduceRameUsage(1);
        }
    }
}
