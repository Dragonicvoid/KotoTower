using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerQuestionCanvasController : MonoBehaviour
{
    [SerializeField] float animationTimer = 0.5f;
    [SerializeField] short eventId = 0;
    public AnimationCurve curve;
    RectTransform rectTransform;
    float maxX, maxY;
    ClickOnGenerator generatorClick;
    ClickOnKotoTower kotoTowerClick;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = this.GetComponent<RectTransform>();
        maxX = rectTransform.rect.width;
        maxY = rectTransform.rect.height;
        generatorClick = this.gameObject.GetComponentInParent<ClickOnGenerator>();
        kotoTowerClick = this.gameObject.GetComponentInParent<ClickOnKotoTower>();

        //event
        GameEvents.current.onCloseKotoTowerBalloonBox += OnCloseKotoTowerBalloonBox;
        GameEvents.current.onOpenKotoTowerBalloonBox += OnOpenKotoTowerBalloonBox;
        GameEvents.current.onCloseGeneratorBalloonBox += OnCloseGeneratorBalloonBox;
        GameEvents.current.onOpenGeneratorBalloonBox += OnOpenGeneratorBalloonBox;
    }

    // event that play to close the balloon box
    private void OnCloseKotoTowerBalloonBox()
    {
        if(kotoTowerClick != null)
            TweenCustom.tween.uiVertical(rectTransform, maxY, 0, animationTimer, Tweens.CURVE, curve, true);
    }

    // event that play to open the balloon box
    private void OnOpenKotoTowerBalloonBox()
    {
        if (kotoTowerClick != null)
            TweenCustom.tween.uiVertical(rectTransform, 0, maxY, animationTimer, Tweens.CURVE, curve, false);
    }

    // event that play to close the balloon box
    private void OnCloseGeneratorBalloonBox()
    {
        if (generatorClick != null)
            TweenCustom.tween.uiHorizontal(rectTransform, maxX, 0, animationTimer, Tweens.CURVE, curve, true);
    }

    // event that play to open the balloon box
    private void OnOpenGeneratorBalloonBox()
    {
        if (generatorClick != null)
            TweenCustom.tween.uiHorizontal(rectTransform, 0, maxX, animationTimer, Tweens.CURVE, curve, false);
    }

    private void OnDestroy()
    {
        GameEvents.current.onCloseKotoTowerBalloonBox -= OnCloseKotoTowerBalloonBox;
        GameEvents.current.onOpenKotoTowerBalloonBox -= OnOpenKotoTowerBalloonBox;
        GameEvents.current.onCloseGeneratorBalloonBox -= OnCloseGeneratorBalloonBox;
        GameEvents.current.onOpenGeneratorBalloonBox -= OnOpenGeneratorBalloonBox;
    }
}
