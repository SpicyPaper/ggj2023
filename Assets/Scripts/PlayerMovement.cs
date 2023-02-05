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
    float minXPosition;

    [SerializeField]
    float maxXPosition;

    private PlayerDirection playerDirection;

    public float speed = 300;

    public int dashDistance = 500;
    public float dashStretchRatioMax = 0.5f;
    public float dashStretchStart = 0.25f;
    public float dashTime = 0.5f;

    public float dashCooldown = 0.5f;
    private bool canDash = true;
    private bool isDashing = false;
    private float currentDashTime = 0f;
    private Vector2 dashStart,
        dashEnd;

    private List<Vector2> dashGoThroughBorderInOrder = new List<Vector2>();

    private bool doesDashCollideBorder = false;

    // Start is called before the first frame update
    void Start()
    {
        playerDirection = PlayerDirection.Left;
    }

    // Update is called once per frame

    void Update()
    {
        if (GameHandler.Instance.GetGameStatus() != GameStatus.InGame)
            return;

        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Mathf.Abs(horizontalInput) > 0)
        {
            playerDirection = horizontalInput > 0 ? PlayerDirection.Right : PlayerDirection.Left;
        }

        transform.localScale = new Vector3(playerDirection == PlayerDirection.Left ? 1 : -1, 1, 1);

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

        if (Mathf.Abs(horizontalInput) < 0.1f)
        {
            return;
        }

        // move the player on local position
        transform.localPosition = new Vector2(
            transform.localPosition.x + (Mathf.Sign(horizontalInput) * speed * Time.deltaTime),
            transform.localPosition.y
        );

        // if the player goes out of the screen then it appears on the other side
        if (transform.localPosition.x < minXPosition)
        {
            float delta = minXPosition - transform.localPosition.x;
            transform.localPosition = new Vector2(maxXPosition - delta, transform.localPosition.y);
        }
        else if (transform.localPosition.x > maxXPosition)
        {
            float delta = transform.localPosition.x - maxXPosition;
            transform.localPosition = new Vector2(minXPosition + delta, transform.localPosition.y);
        }
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

        if (collidedGameObjectParent.tag == "File" && !isDashing)
        {
            FallDownFileData fallDownFileData =
                collidedGameObjectParent.GetComponent<FallDownFileData>();

            int ramToAdd = fallDownFileData.File.GetRamWeight();

            // Destroy the gameobject linked to the collision
            Destroy(collidedGameObjectParent);

            // call the GameHandler to increase the ram usage
            GameHandler.Instance.AddReduceRameUsage(ramToAdd);
        }
        if (collidedGameObjectParent.tag == "FinalFolder")
        {
            // Destroy the gameobject linked to the collision
            Destroy(collidedGameObjectParent);

            GameHandler.Instance.GoToNextStage();
        }
    }

    private void StartDash()
    {
        isDashing = true;
        canDash = false;
        currentDashTime = 0;
        dashStart = transform.localPosition;

        dashEnd = new Vector2(
            transform.localPosition.x
                + dashDistance * (playerDirection == PlayerDirection.Left ? -1 : 1),
            transform.localPosition.y
        );

        doesDashCollideBorder = false;
        // clear all content of dashGoThroughBorderInOrder
        dashGoThroughBorderInOrder.Clear();

        // if the player goes out of the screen then it appears on the other side
        if (dashEnd.x < minXPosition)
        {
            doesDashCollideBorder = true;
            dashGoThroughBorderInOrder.Add(new Vector2(minXPosition, dashEnd.y));
            dashGoThroughBorderInOrder.Add(new Vector2(maxXPosition, dashEnd.y));
            float delta = minXPosition - dashEnd.x;
            dashEnd = new Vector2(maxXPosition - delta, dashEnd.y);
        }
        else if (dashEnd.x > maxXPosition)
        {
            doesDashCollideBorder = true;
            dashGoThroughBorderInOrder.Add(new Vector2(maxXPosition, dashEnd.y));
            dashGoThroughBorderInOrder.Add(new Vector2(minXPosition, dashEnd.y));
            float delta = dashEnd.x - maxXPosition;
            dashEnd = new Vector2(minXPosition + delta, dashEnd.y);
        }
    }

    private void Dash()
    {
        // Is currently dashing
        if (!isDashing)
        {
            return;
        }

        currentDashTime += Time.deltaTime;

        if (doesDashCollideBorder)
        {
            DashWithBorderCollision();
        }
        else
        {
            DashWithoutBorderCollision();
        }
    }

    private void DashWithoutBorderCollision()
    {
        float perc = Mathf.Clamp01(currentDashTime / dashTime);
        float scalingRatio = SmoothScaling(perc);

        transform.localPosition = Vector2.Lerp(dashStart, dashEnd, perc);
        transform.localScale = new Vector2(
            Mathf.Sign(transform.localScale.x) * (1 / scalingRatio),
            scalingRatio
        );

        if (currentDashTime >= dashTime)
        {
            transform.localPosition = dashEnd;
            transform.localScale = new Vector2(Mathf.Sign(transform.localScale.x), 1);
            isDashing = false;
            Invoke("ResetDash", dashCooldown);
        }
    }

    private void DashWithBorderCollision()
    {
        float perc = Mathf.Clamp01(currentDashTime / dashTime);
        float scalingRatio = SmoothScaling(perc);

        if (perc < 0.5f)
        {
            transform.localPosition = Vector2.Lerp(
                dashStart,
                dashGoThroughBorderInOrder[0],
                perc * 2
            );
        }
        else
        {
            transform.localPosition = Vector2.Lerp(
                dashGoThroughBorderInOrder[1],
                dashEnd,
                (perc - 0.5f) * 2
            );
        }

        transform.localScale = new Vector2(
            Mathf.Sign(transform.localScale.x) * (1 / scalingRatio),
            scalingRatio
        );

        if (currentDashTime >= dashTime)
        {
            transform.localPosition = dashEnd;
            transform.localScale = new Vector2(Mathf.Sign(transform.localScale.x), 1);
            isDashing = false;
            Invoke("ResetDash", dashCooldown);
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
