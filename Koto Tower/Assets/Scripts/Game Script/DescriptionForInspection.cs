using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionForInspection : MonoBehaviour
{
    [SerializeField] List<GameObject> towersPic = new List<GameObject>();
    [SerializeField] Text costText = null;

    // change the descriptor
    public void changeDescriptor(TowerType type)
    {
        resetAll();
        switch (type)
        {
            case TowerType.MACHINE_GUN:
                towersPic[0].gameObject.SetActive(true);
                costText.text = "" + Mathf.Floor(GameManager.instance.towerPrices[0].price / 4);
                break;
            case TowerType.SNIPER:
                towersPic[1].gameObject.SetActive(true);
                costText.text = "" + Mathf.Floor(GameManager.instance.towerPrices[1].price / 4);
                break;
            case TowerType.ELECTRIC:
                towersPic[2].gameObject.SetActive(true);
                costText.text = "" + Mathf.Floor(GameManager.instance.towerPrices[2].price / 4);
                break;
            default:
                towersPic[0].gameObject.SetActive(true);
                costText.text = "" + Mathf.Floor(GameManager.instance.towerPrices[0].price / 4);
                break;
        }
    }

    //reset Field
    void resetAll()
    {
        foreach (GameObject tower in towersPic)
            tower.gameObject.SetActive(false);
    }
}
