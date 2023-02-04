using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAdapter : MonoBehaviour
{

    [SerializeField] RectTransform nameGameObjectSizeFitter;

    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log("Background size: " + rectTransform.rect.size);
        Debug.Log("Name size: " + nameGameObjectSizeFitter.rect.size);

        // set the size of the background to the width and height of the name rect transform
        rectTransform.sizeDelta = new Vector2(nameGameObjectSizeFitter.rect.width, nameGameObjectSizeFitter.rect.height);

    }
}
