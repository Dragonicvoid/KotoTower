using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FontChangeSizeTesting : MonoBehaviour
{
    [SerializeField] float division = 0f;

    // change every text on this child to change it font size
    private void Start()
    {
        Text[] texts = this.gameObject.GetComponentsInChildren<Text>();
        foreach (Text text in texts)
            text.fontSize = (int)(Screen.width / division);
    }
}
