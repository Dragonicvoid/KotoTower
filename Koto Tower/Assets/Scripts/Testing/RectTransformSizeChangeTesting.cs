using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectTransformSizeChangeTesting : MonoBehaviour
{
    // ratio to a screen width
    [SerializeField] float divisionWidth = 80f;
    [SerializeField] float divisionHeight = 80f;
    [SerializeField] bool isForParent = false;
    RectTransform rect;

    private void Start()
    {
        rect = this.gameObject.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(Screen.width / divisionWidth, Screen.width / divisionHeight);
    }
}
