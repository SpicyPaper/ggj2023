using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FallDownElementMovement : MonoBehaviour
{
    [SerializeField]
    int FallDownLimit;

    private float speed = 180;

    private float elapsedTime;
    private float neededTime = 0.6f;
    private float neededTimePart2 = 0.7f;

    private float elapsedTimeShaky;
    private float neededTimeShaky = 0.2f;
    private int shakyDirection = 1;
    public ParticleSystem particle;

    private void Awake()
    {
        elapsedTime = 0;
        elapsedTimeShaky = neededTimeShaky;
    }

    private void Start()
    {
        // if the gameobject as the tag "File" then we need to change speed based on the file type
        bool isFile = gameObject.CompareTag("File");

        if (!isFile)
        {
            return;
        }

        // get the file type by looking at the FallDownFileData component
        FallDownFileData fileData = GetComponent<FallDownFileData>();

        // change the speed based on the file type
        speed *= fileData.File.GetSpeed();
    }

    void Update()
    {
        if (GameHandler.Instance.GetGameStatus() != GameStatus.InGame)
        {
            return;
        }

        elapsedTime += Time.deltaTime;
        if (elapsedTime < neededTime)
        {
            float perc = elapsedTime / neededTime;
            transform.localScale = Vector3.one * Mathf.Lerp(0, 1.7f, perc);
            return;
        }
        else if (elapsedTime < neededTimePart2)
        {
            float perc = (elapsedTime - neededTime) / (neededTimePart2 - neededTime);
            transform.localScale = Vector3.one * Mathf.Lerp(1.7f, 1, perc);
            return;
        }

        elapsedTimeShaky += Time.deltaTime;
        if (elapsedTimeShaky >= neededTimeShaky)
        {
            elapsedTimeShaky = 0;
            shakyDirection *= -1;
        }

        float percShaky = elapsedTimeShaky / neededTimeShaky;
        transform.Rotate(Vector3.forward * shakyDirection * 0.1f * percShaky);
        if (shakyDirection < 0)
        {
            transform.localScale = Vector3.one * Mathf.Lerp(1, 1.05f, percShaky);
        }
        else
        {
            transform.localScale = Vector3.one * Mathf.Lerp(1.05f, 1f, percShaky);
        }

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
            var p = Instantiate(particle);
            p.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z -10);
            p.Play();
            Destroy(p, 0.5f);
            Destroy(gameObject);
        }
    }

    private float ComputeFallDownSpeed()
    {
        return speed
            * Time.deltaTime
            * (
                (GameHandler.Instance.CounterClearedStage + 1)
                * GameHandler.Instance.FallDownElementMultiplier
            );
    }
}
