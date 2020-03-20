using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool isSelectTrap;
    public static short selectedTrap;
    public static bool isDoneMakingTrapLines;
    public static bool isPressedButtonTrap;

    public static bool isSelectTower;
    public static short selectedTower;
    public static bool isDoneMakingTowerLines;
    public static bool isPressedButtonTower;

    public static float money;
    public static bool moneyChanged;

    public static List<TowerPrice> towerPrices;
    public static List<TrapPrice> trapPrices;

    private void Start()
    {
        resetOnPlay();
    }

    // initialization
    public static void resetOnPlay()
    {
        isSelectTrap = false;
        isSelectTower = false;

        isDoneMakingTowerLines = false;
        isDoneMakingTrapLines = false;

        selectedTower = -1;
        selectedTrap = -1;

        isPressedButtonTower = false;
        isPressedButtonTrap = false;

        moneyChanged = false;
    }

    // set the money and says that the money has changed
    public static void setMoney(float money)
    {
        GameManager.money = money;
        moneyChanged = true;
    }

    // add the money and says that the money has changed
    public static void addMoney(float money)
    {
        GameManager.money += money;
        moneyChanged = true;
    }

    // set the Tower and Trap prices
    public static void setPrices(List<TowerPrice> towerP, List<TrapPrice> trapP)
    {
        towerPrices = new List<TowerPrice>();
        trapPrices = new List<TrapPrice>();

        towerPrices.AddRange(towerP);
        trapPrices.AddRange(trapP);
    }

    // pay for tower
    public static void pay(TowerType type)
    {
        switch (type)
        {
            case TowerType.MACHINE_GUN:
                money -= towerPrices[0].price;
                break;
            case TowerType.SNIPER:
                money -= towerPrices[1].price;
                break;
            case TowerType.ELECTRIC:
                money -= towerPrices[2].price;
                break;
            default:
                break;
        }
        moneyChanged = true;
    }

    // pay for trap
    public static void pay(TrapType type)
    {
        switch (type)
        {
            case TrapType.BOMB_TRAP:
                money -= trapPrices[0].price;
                break;
            case TrapType.TIME_TRAP:
                money -= trapPrices[1].price;
                break;
            case TrapType.FREEZE_TRAP:
                money -= trapPrices[2].price;
                break;
            default:
                break;
        }
        moneyChanged = true;
    }
}
