using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FallDownElementMovement : MonoBehaviour
{

    [SerializeField] int fallDownStep;

    [SerializeField] float durationBetweenSteps;

    [SerializeField] int FallDownLimit;

    private float elapsedDuration;

    void Start()
    {

        elapsedDuration = 0f;

    }

    void Update()
    {

        elapsedDuration += Time.deltaTime;

        if (elapsedDuration < durationBetweenSteps)
        {
            return;
        }

        elapsedDuration -= durationBetweenSteps;


        // use local position to prevent issue when rescaling the window
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - fallDownStep, transform.localPosition.z);
        elapsedDuration = 0;

        if (transform.localPosition.y < FallDownLimit)
        {
            Destroy(gameObject);
        }
    }
}
