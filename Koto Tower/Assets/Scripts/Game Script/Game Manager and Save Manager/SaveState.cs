using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveState
{
    public int levelDone;
    public string username;
    private string password;

    //constructor when the save are created
    public SaveState()
    {
        levelDone = 1;
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
