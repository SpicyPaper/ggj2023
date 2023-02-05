using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDownFinalFolderMovement : MonoBehaviour
{
    [SerializeField]
    int FallDownLimit;

    private float speed = 180;

    private float fallDownSpeed = 1.35f;

    private GameObject playerFollow;

    public void SetPlayerFollow(GameObject playerFollow)
    {
        this.playerFollow = playerFollow;
    }

    void Update()
    {
        // get the current state of the GameHandler
        if (GameHandler.Instance.GetGameStatus() != GameStatus.InGame)
        {
            return;
        }

        // use local position to prevent issue when rescaling the window
        transform.localPosition = new Vector3(
            playerFollow.transform.localPosition.x,
            transform.localPosition.y - ComputeFallDownSpeed(),
            transform.localPosition.z
        );

        if (transform.localPosition.y < FallDownLimit)
        {
            GameHandler.Instance.GoToNextStage();
            Destroy(gameObject);
        }
    }

    private float ComputeFallDownSpeed()
    {
        return speed * fallDownSpeed * Time.deltaTime;
    }
}
