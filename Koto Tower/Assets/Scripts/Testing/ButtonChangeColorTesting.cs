using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonChangeColorTesting : MonoBehaviour
{
    // attribute
    Image thisImage;

    // Initialization
    private void Awake()
    {
        thisImage = this.gameObject.GetComponent<Image>();
        thisImage.color = new Color(1, 1, 1, 150f / 255f);
    }

    // activate means change the background color to black with alpha 150
    public void activate()
    {
        thisImage.color = new Color(0, 0, 0, 150f / 255f);
    }

    // disable means change to normal
    public void disable()
    {
        thisImage.color = new Color(1, 1, 1, 150f / 255f);
    }
}
