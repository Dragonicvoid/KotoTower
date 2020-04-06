using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterShow : MonoBehaviour
{
    // Event initialization
    private void Start()
    {
        GameEvents.current.onTowerSelected += showFilter;
        GameEvents.current.onTowerUnselected += unshowFilter;
        GameEvents.current.onTowerOrTrapBuild += unshowFilter;

        this.gameObject.SetActive(false);
    }

    // activate the object for select tower
    void showFilter(TowerBehaviour tower)
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
        GameEvents.current.onTowerSelected -= showFilter;
        GameEvents.current.onTowerUnselected -= unshowFilter;
        GameEvents.current.onTowerOrTrapBuild -= unshowFilter;
    }
}
