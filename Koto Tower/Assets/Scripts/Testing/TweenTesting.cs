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

public enum TweensDirection
{
    VERTICAL,
    HORIZONTAL
}

public class TweenTesting : MonoBehaviour
{
    // Singleton so other class can call it
    public static TweenTesting tween;

    // List of action that has object id to identify
    Dictionary<int, float> timeList = new Dictionary<int, float>();

    // Called singleton to reference that static class to this object
    private void Awake()
    {
        tween = this;
    }

    // Calculating time so the timer reach the timer equals or greater than time
    private float deltaTime(float timer, float time)
    {
        if (timer < time)
            return timer / time;
        else
            return 1;
    }

    private bool checkForAvailability(int objId)
    {
        float totalTime;
        if (!timeList.TryGetValue(objId, out totalTime))
        {
            timeList.Add(objId, 0f);
            return false;
        }
        else
            return true;
    }

    // calling vertical UI tween
    public void uiVertical(RectTransform rectTransform, float fromHeight, float toHeight, float time, Tweens tweenType, AnimationCurve curve = null, bool disable = false)
    {
        switch (tweenType)
        {
            case Tweens.CURVE:
                tween.StartCoroutine(tween.TweenCurveUI(rectTransform, fromHeight, toHeight, time, TweensDirection.VERTICAL, curve, disable));
                break;
            case Tweens.EASE_IN:
                tween.StartCoroutine(tween.TweenEaseInUI(rectTransform, fromHeight, toHeight, time, TweensDirection.VERTICAL, disable));
                break;
            case Tweens.LINEAR:
                tween.StartCoroutine(tween.TweenLinearUI(rectTransform, fromHeight, toHeight, time, TweensDirection.VERTICAL, disable));
                break;
            default:
                break;
        }
    }

    // calling horizontal UI tween
    public void uiHorizontal(RectTransform rectTransform, float fromWidth, float toWidth, float time, Tweens tweenType, AnimationCurve curve = null, bool disable = false)
    {
        switch (tweenType)
        {
            case Tweens.CURVE:
                tween.StartCoroutine(tween.TweenCurveUI(rectTransform, fromWidth, toWidth, time, TweensDirection.HORIZONTAL, curve, disable));
                break;
            case Tweens.EASE_IN:
                tween.StartCoroutine(tween.TweenEaseInUI(rectTransform, fromWidth, toWidth, time, TweensDirection.HORIZONTAL, disable));
                break;
            case Tweens.LINEAR:
                tween.StartCoroutine(tween.TweenLinearUI(rectTransform, fromWidth, toWidth, time, TweensDirection.HORIZONTAL, disable));
                break;
            default:
                break;
        }
    }

    // Tweening with linear (value of x)
    IEnumerator TweenLinearUI(RectTransform rectTransform, float start, float end, float time, TweensDirection direction, bool disable)
    {
        float timer = 0;
        float fractionTime = 0;
        // This is to prevent animation got stuck in the middle of something
        rectTransform.SetSizeWithCurrentAnchors(direction == TweensDirection.VERTICAL ? RectTransform.Axis.Vertical : RectTransform.Axis.Horizontal,
                                                start);

        // Enable game object at the start of animation
        if (!disable)
            rectTransform.gameObject.SetActive(true);

        while (fractionTime < 1)
        {
            // Tweening
            timer += Time.deltaTime;
            fractionTime = deltaTime(timer, time);
            rectTransform.SetSizeWithCurrentAnchors(direction == TweensDirection.VERTICAL ? RectTransform.Axis.Vertical : RectTransform.Axis.Horizontal,
                                                     Mathf.Lerp(start, end, fractionTime * fractionTime));
            yield return null;
        }

        // This is to prevent floating point error
        rectTransform.SetSizeWithCurrentAnchors(direction == TweensDirection.VERTICAL ? RectTransform.Axis.Vertical : RectTransform.Axis.Horizontal,
                                                end);

        // disable the game object after animation
        if (disable)
            rectTransform.gameObject.SetActive(false);

        yield return null;
    }

    // Tweening with ease in (square value of x)
    IEnumerator TweenEaseInUI(RectTransform rectTransform, float start, float end, float time, TweensDirection direction, bool disable)
    { 

        float timer = 0;
        float fractionTime = 0;
        // This is to prevent animation got stuck in the middle of something
        rectTransform.SetSizeWithCurrentAnchors(direction == TweensDirection.VERTICAL ? RectTransform.Axis.Vertical : RectTransform.Axis.Horizontal,
                                                start);

        // Enable game object at the start of animation
        if (!disable)
            rectTransform.gameObject.SetActive(true);

        while (fractionTime < 1)
        {
            // Tweening
            timer += Time.deltaTime;
            fractionTime = deltaTime(timer, time);
            rectTransform.SetSizeWithCurrentAnchors(direction == TweensDirection.VERTICAL ? RectTransform.Axis.Vertical : RectTransform.Axis.Horizontal,
                                                    Mathf.Lerp(start, end, fractionTime));
            yield return null;
        }

        // This is to prevent floating point error
        rectTransform.SetSizeWithCurrentAnchors(direction == TweensDirection.VERTICAL ? RectTransform.Axis.Vertical : RectTransform.Axis.Horizontal,
                                                end);
        // disable the game object after animation
        if (disable)
            rectTransform.gameObject.SetActive(false);

        yield return null;
    }

    // Tweening using curve animation
    IEnumerator TweenCurveUI(RectTransform rectTransform, float start, float end, float time, TweensDirection direction, AnimationCurve curve, bool disable)
    {
        float timer = 0;
        float fractionTime = 0;
        // This is to prevent animation got stuck in the middle of something
        rectTransform.SetSizeWithCurrentAnchors(direction == TweensDirection.VERTICAL ? RectTransform.Axis.Vertical : RectTransform.Axis.Horizontal,
                                                start);

        // Enable game object at the start of animation
        if (!disable)
            rectTransform.gameObject.SetActive(true);

        while (fractionTime < 1)
        {
            // Tweening
            timer += Time.deltaTime;
            fractionTime = deltaTime(timer, time);
            rectTransform.SetSizeWithCurrentAnchors(direction == TweensDirection.VERTICAL ? RectTransform.Axis.Vertical : RectTransform.Axis.Horizontal,
                                                        (end - start) * curve.Evaluate(fractionTime) + start);
            yield return null;
        }

        // This is to prevent floating point error
        rectTransform.SetSizeWithCurrentAnchors(direction == TweensDirection.VERTICAL ? RectTransform.Axis.Vertical : RectTransform.Axis.Horizontal,
                                                end);

        // disable the game object after animation
        if (disable)
            rectTransform.gameObject.SetActive(false);

        yield return null;
    }
}
