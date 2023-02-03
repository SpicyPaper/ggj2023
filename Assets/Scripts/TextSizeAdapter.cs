using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TextSizeAdapter : MonoBehaviour
{
    [SerializeField] private GameObject attachedElement;
    [SerializeField] private int leftMargin = 5;

    void Update()
    {
        gameObject.transform.localPosition = new Vector2(
            attachedElement.GetComponent<RectTransform>().sizeDelta.x + leftMargin,
            gameObject.transform.localPosition.y);
    }
}
