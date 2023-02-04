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

    public int dashMultiplier = 5;
    public float dashStretchRatioMax = 0.5f;
    public float dashStretchStart = 0.25f;
    public float dashTime = 0.5f;

    public float dashCooldown = 0.5f;
    private bool canDash = true;
    private bool isDashing = false;
    private float currentDashTime = 0f;
    private Vector2 dashStart, dashEnd;

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
            return;


        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartDash();
            return;
        }
        else if (isDashing)
        {
            Dash();
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
        GameObject collidedGameObject = collision.gameObject;

        // if the collided object has no parent then return
        if (collidedGameObject.transform.parent == null)
        {
            return;
        }

        GameObject collidedGameObjectParent = collidedGameObject.transform.parent.gameObject;

        if (collidedGameObjectParent.tag == "FallingElement" && !isDashing)
        {
            // Destroy the gameobject linked to the collision
            Destroy(collidedGameObjectParent);

            // call the GameHandler to increase the ram usage
            GameHandler.instance.AddReduceRameUsage(1);
        }
    }


    private void StartDash()
    {
        isDashing = true;
        canDash = false;
        currentDashTime = 0;
        dashStart = transform.localPosition;
        dashEnd = new Vector2(transform.localPosition.x + MovementStep * (playerDirection == PlayerDirection.Left ? -1 : 1) * dashMultiplier, transform.localPosition.y);
    }

    private void Dash()
    {
        
        // Is currently dashing
        if (isDashing)
        {
            currentDashTime += Time.deltaTime;

            float perc = Mathf.Clamp01(currentDashTime / dashTime);
            float scalingRatio = SmoothScaling(perc);

            transform.localPosition = Vector2.Lerp(dashStart, dashEnd, perc);
            transform.localScale = new Vector2(1 / scalingRatio, scalingRatio);


            if (currentDashTime >= dashTime)
            {
                transform.localPosition = dashEnd;
                transform.localScale = new Vector2(1, 1);
                isDashing = false;
                Invoke("ResetDash", dashCooldown);
                elapsedDuration = 0;
            }
        }

    }

    private void ResetDash()
    {
        canDash = true;
    }

    private float SmoothScaling(float perc)
    {
        float modifier = dashStretchRatioMax;
        if (perc < dashStretchStart)
            modifier = Mathf.Lerp(1f, dashStretchRatioMax, perc / dashStretchStart);
        else if (perc > (1f - dashStretchStart))
            modifier = Mathf.Lerp(dashStretchRatioMax, 1f, perc / (1f - dashStretchStart));

        return modifier;
    }
}
