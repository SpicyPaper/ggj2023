using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderSizeAdapter : MonoBehaviour
{

    [SerializeField] RectTransform backgroundGameObjectTransform;

    private BoxCollider2D boxCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // set the size of the collider to the width and height of the background rect transform
        boxCollider2D.size = new Vector2(backgroundGameObjectTransform.rect.width, backgroundGameObjectTransform.rect.height);

    }
}
