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
    ClickOnGeneratorTesting generatorClick;
    ClickOnKotoTowerTesting kotoTowerClick;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = this.GetComponent<RectTransform>();
        maxX = rectTransform.rect.width;
        maxY = rectTransform.rect.height;
        generatorClick = this.gameObject.GetComponentInParent<ClickOnGeneratorTesting>();
        kotoTowerClick = this.gameObject.GetComponentInParent<ClickOnKotoTowerTesting>();

        //event
        GameEventsTesting.current.onCloseKotoTowerBalloonBox += OnCloseKotoTowerBalloonBox;
        GameEventsTesting.current.onOpenKotoTowerBalloonBox += OnOpenKotoTowerBalloonBox;
        GameEventsTesting.current.onCloseGeneratorBalloonBox += OnCloseGeneratorBalloonBox;
        GameEventsTesting.current.onOpenGeneratorBalloonBox += OnOpenGeneratorBalloonBox;
    }

    // event that play to close the balloon box
    private void OnCloseKotoTowerBalloonBox()
    {
        if(kotoTowerClick != null)
            TweenTesting.tween.uiVertical(rectTransform, maxY, 0, animationTimer, Tweens.CURVE, curve, true);
    }

    // event that play to open the balloon box
    private void OnOpenKotoTowerBalloonBox()
    {
        if (kotoTowerClick != null)
            TweenTesting.tween.uiVertical(rectTransform, 0, maxY, animationTimer, Tweens.CURVE, curve, false);
    }

    // event that play to close the balloon box
    private void OnCloseGeneratorBalloonBox()
    {
        if (generatorClick != null)
            TweenTesting.tween.uiHorizontal(rectTransform, maxX, 0, animationTimer, Tweens.CURVE, curve, true);
    }

    // event that play to open the balloon box
    private void OnOpenGeneratorBalloonBox()
    {
        if (generatorClick != null)
            TweenTesting.tween.uiHorizontal(rectTransform, 0, maxX, animationTimer, Tweens.CURVE, curve, false);
    }

    private void OnDestroy()
    {
        GameEventsTesting.current.onCloseKotoTowerBalloonBox -= OnCloseKotoTowerBalloonBox;
        GameEventsTesting.current.onOpenKotoTowerBalloonBox -= OnOpenKotoTowerBalloonBox;
        GameEventsTesting.current.onCloseGeneratorBalloonBox -= OnCloseGeneratorBalloonBox;
        GameEventsTesting.current.onOpenGeneratorBalloonBox -= OnOpenGeneratorBalloonBox;
    }
}
