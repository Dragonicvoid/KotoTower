using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUIComponentTesting : MonoBehaviour
{
    [SerializeField] float divX = 0, divY = 0;
    RectTransform rect;

    private void Start()
    {
        rect = this.gameObject.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2((divX != 0f ? Screen.width / divX : 0f), (divY != 0f ? Screen.width / divY : 0f));
    }

    // get divY
    public float getDivY()
    {
        return this.divY;
    }
}
