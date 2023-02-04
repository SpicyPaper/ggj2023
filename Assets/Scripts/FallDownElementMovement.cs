using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FallDownElementMovement : MonoBehaviour
{
    [SerializeField] int FallDownLimit;

    private float speed = 180;

    void Update()
    {
        // get the current state of the GameHandler
        if (GameHandler.Instance.GetGameStatus() != GameStatus.InGame)
        {
            return;
        }

        // use local position to prevent issue when rescaling the window
        transform.localPosition = new Vector3(
            transform.localPosition.x,
            transform.localPosition.y - ComputeFallDownSpeed(),
            transform.localPosition.z
        );

        if (transform.localPosition.y < FallDownLimit)
        {
            Destroy(gameObject);
        }
    }

    private float ComputeFallDownSpeed()
    {
        return speed * Time.deltaTime *
            ((GameHandler.Instance.CounterClearedStage + 1) *
            GameHandler.Instance.FallDownElementMultiplier);
    }
}
