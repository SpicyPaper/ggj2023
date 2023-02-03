using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextFallDown : MonoBehaviour
{

    [SerializeField] int fallDownStep;

    [SerializeField] int updateBetweenSteps;

    [SerializeField] int FallDownLimit;

    private int updateBetweenStepsCounter;

    void Start()
    {

        updateBetweenStepsCounter = 0;

    }

    void FixedUpdate()
    {
        if (updateBetweenStepsCounter == updateBetweenSteps)
        {
            // use local position to prevent issue when rescaling the window
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - fallDownStep, transform.localPosition.z);
            updateBetweenStepsCounter = 0;

            if (transform.localPosition.y < FallDownLimit)
            {
                Destroy(gameObject);
            }

        }
        else
        {
            updateBetweenStepsCounter++;
        }
    }
}
