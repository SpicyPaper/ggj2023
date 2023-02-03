using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextFallDown : MonoBehaviour
{

    [SerializeField] int fallDownStep;
    // Start is called before the first frame update

    [SerializeField] int updateBetweenSteps;

    private int updateBetweenStepsCounter;

    void Start()
    {

        updateBetweenStepsCounter = 0;

    }

    void FixedUpdate()
    {
        if (updateBetweenStepsCounter == updateBetweenSteps)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - fallDownStep, transform.position.z);
            updateBetweenStepsCounter = 0;
        }
        else
        {
            updateBetweenStepsCounter++;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
