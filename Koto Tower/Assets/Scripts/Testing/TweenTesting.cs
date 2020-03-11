using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Tweens
{
    LINEAR,
    EASE_IN,
    CURVE,
    STOP
}

public class TweenTesting : MonoBehaviour
{
    public AnimationCurve curve;
    public Tweens tweenType;
    [SerializeField] float time;
    [SerializeField]float start;
    [SerializeField] float end;
    float timer;
    RectTransform uiObj;

    private void Start()
    {
        resetAllTimer();
    }

    private float deltaTime()
    {
        if (timer < time)
            return timer / time;
        else
            return 1;
    }

    public void linearUIVertical()
    {
        this.uiObj = this.GetComponent<RectTransform>();
        resetAllTimer();
        StartCoroutine(TweenLinearUIVectical());
    }

    public void linearUIHorizontal(RectTransform rectTransform, float fromWidth, float toWidth, Tweens tweenType)
    {
        this.uiObj = rectTransform;
        this.start = fromWidth;
        this.end = toWidth;
        this.tweenType = tweenType;
        resetAllTimer();
    }

    void resetAllTimer()
    {
        timer = 0;
    }

    IEnumerator TweenLinearUIVectical()
    {
        float fractionTime = 0;
        while (fractionTime < 1)
        {
            timer += Time.deltaTime;
            fractionTime = deltaTime();
            uiObj.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Mathf.Lerp(start, end, fractionTime));
            yield return null;
        }
        yield return null;
    }
}
