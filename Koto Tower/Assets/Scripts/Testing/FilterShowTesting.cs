using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterShowTesting : MonoBehaviour
{
    // Event initialization
    private void Start()
    {
        GameEventsTesting.current.onTowerSelected += showFilter;
        GameEventsTesting.current.onTowerUnselected += unshowFilter;
        GameEventsTesting.current.onTowerOrTrapBuild += unshowFilter;

        this.gameObject.SetActive(false);
    }

    // activate the object for select tower
    void showFilter(TowerBehaviourTesting tower)
    {
        this.gameObject.SetActive(true);
    }

    // deactivate the object
    void unshowFilter()
    {
        this.gameObject.SetActive(false);
    }

    // delete the event
    private void OnDestroy()
    {
        GameEventsTesting.current.onTowerSelected -= showFilter;
        GameEventsTesting.current.onTowerUnselected -= unshowFilter;
        GameEventsTesting.current.onTowerOrTrapBuild -= unshowFilter;
    }
}
