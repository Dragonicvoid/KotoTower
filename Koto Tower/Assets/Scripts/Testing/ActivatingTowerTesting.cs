using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatingTowerTesting : MonoBehaviour
{
    bool isActiveAllCircle;

    // initialization
    private void Start()
    {
        isActiveAllCircle = false;
    }

    // activate all tower circle
    public void activateAllCircle()
    {
        // Get all children with circle script
        CircleOnTowerTesting[] children = this.gameObject.GetComponentsInChildren<CircleOnTowerTesting>();
        isActiveAllCircle = !isActiveAllCircle;

        foreach (CircleOnTowerTesting child in children)
            child.setCircleToActive(isActiveAllCircle);
    }
}
