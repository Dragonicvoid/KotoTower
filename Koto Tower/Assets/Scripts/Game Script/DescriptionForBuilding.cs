using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionForBuilding : MonoBehaviour
{
    [SerializeField] List<GameObject> towerImages = new List<GameObject>();
    [SerializeField] List<GameObject> towerDesc = new List<GameObject>();
    [SerializeField] List<GameObject> trapImages = new List<GameObject>();
    [SerializeField] List<GameObject> trapDesc = new List<GameObject>();
    [SerializeField] Text moneyText = null;

    // change the desc and pic
    public void changeDesc(bool isTower, int idx)
    {
        resetAll();
        if (isTower)
        {
            moneyText.text = "" + GameManager.instance.towerPrices[idx].price;
            towerImages[idx].gameObject.SetActive(true);
            towerDesc[idx].gameObject.SetActive(true);
        }
        else
        {
            moneyText.text = "" + GameManager.instance.trapPrices[idx].price;
            trapImages[idx].gameObject.SetActive(true);
            trapDesc[idx].gameObject.SetActive(true);
        }
    }

    // reset all game object
    private void resetAll()
    {
        foreach (GameObject tower in towerImages)
            tower.gameObject.SetActive(false);

        foreach (GameObject desc in towerDesc)
            desc.gameObject.SetActive(false);

        foreach (GameObject trap in trapImages)
            trap.gameObject.SetActive(false);

        foreach (GameObject desc in trapDesc)
            desc.gameObject.SetActive(false);
    }
}
