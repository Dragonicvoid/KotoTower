using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveState
{
    public int levelDone = 0;
    public string username;
    public string password;

    //constructor when the save are created
    public SaveState(bool forNewGame)
    {
        if (forNewGame)
        {
            levelDone = 1;
            username = "";
            password = "";
        }
        else
        {
            levelDone = 0;
            username = "";
            password = "";
        }
    }

    public SaveState()
    {
        levelDone = 0;
        username = "";
        password = "";
    }

    // setter getter
    public void setPassword(string password)
    {
        this.password = password;
    }

    public string getPassword()
    {
        return this.password;
    }
}
