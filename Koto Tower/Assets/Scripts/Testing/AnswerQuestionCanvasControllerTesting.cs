using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerQuestionCanvasControllerTesting : MonoBehaviour
{
    [SerializeField] float animationTimer = 0.5f;
    [SerializeField] short eventId = 0;
    public AnimationCurve curve;
    RectTransform rectTransform;
    float maxX, maxY;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = this.GetComponent<RectTransform>();
        maxX = rectTransform.rect.width;
        maxY = rectTransform.rect.height;
        GameEventsTesting.current.onObjectOffScreenEnter += OnObjectOffScreen;
        GameEventsTesting.current.onObjectOnScreenEnter += OnObjectOnScreen;
    }

    // Activate the event id 2 is for Answer Canvas and 3 is for Question Canvas

    // event that play when the object is off screen
    private void OnObjectOffScreen(int id)
    {
        if(id == eventId)
        {
            switch (id)
            {
                case 2:
                    TweenTesting.tween.uiVertical(rectTransform, maxY, 0, animationTimer, Tweens.CURVE, curve, true);
                    break;
                case 3:
                    TweenTesting.tween.uiHorizontal(rectTransform, maxX, 0, animationTimer, Tweens.CURVE, curve, true);
                    break;
                default:
                    TweenTesting.tween.uiVertical(rectTransform, maxY, 0, animationTimer, Tweens.CURVE, curve, true);
                    break;
            }
        }
    }

    // event that play when the object is on screen
    private void OnObjectOnScreen(int id)
    {
        if (id == eventId)
        {
            switch (id)
            {
                case 2:
                    TweenTesting.tween.uiVertical(rectTransform, 0, maxY, animationTimer, Tweens.CURVE, curve, false);
                    break;
                case 3:
                    TweenTesting.tween.uiHorizontal(rectTransform, 0, maxX, animationTimer, Tweens.CURVE, curve, false);
                    break;
                default:
                    TweenTesting.tween.uiVertical(rectTransform, 0, maxY, animationTimer, Tweens.CURVE, curve, false);
                    break;
            }
        }
    }

    private void OnDestroy()
    {
        GameEventsTesting.current.onObjectOffScreenEnter -= OnObjectOffScreen;
        GameEventsTesting.current.onObjectOnScreenEnter -= OnObjectOnScreen;
    }
}
