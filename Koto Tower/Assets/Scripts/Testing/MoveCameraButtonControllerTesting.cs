using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraButtonControllerTesting : MonoBehaviour
{
    [SerializeField] GameObject kotoTowerButton;
    [SerializeField] GameObject generatorButton;

    private void Start()
    {
        GameEventsTesting.current.onObjectOffScreenEnter += OnObjectOffScreen;
        GameEventsTesting.current.onObjectOnScreenEnter += OnObjectOnScreen;
    }

    private void OnObjectOffScreen(int id)
    {
        switch (id)
        {
            case 0:
                kotoTowerButton.SetActive(true);
                break;
            case 1:
                generatorButton.SetActive(true);
                break;
            default:
                break;
        }
    }

    private void OnObjectOnScreen(int id)
    {
        switch (id)
        {
            case 0:
                kotoTowerButton.SetActive(false);
                break;
            case 1:
                generatorButton.SetActive(false);
                break;
            default:
                break;
        }
    }

    private void OnDestroy()
    {
        GameEventsTesting.current.onObjectOffScreenEnter -= OnObjectOffScreen;
        GameEventsTesting.current.onObjectOnScreenEnter -= OnObjectOnScreen;
    }
}
