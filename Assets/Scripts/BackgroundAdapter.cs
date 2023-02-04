using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAdapter : MonoBehaviour
{
    [SerializeField]
    RectTransform nameGameObjectSizeFitter;

    [SerializeField]
    float sideMargin = 0.3f;

    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        // set the size of the background to the width and height of the name rect transform
        rectTransform.sizeDelta = new Vector2(
            nameGameObjectSizeFitter.rect.width + sideMargin * 2,
            nameGameObjectSizeFitter.rect.height
        );
    }
}
