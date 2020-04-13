using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoreboard
{
    public float time;
    public int consecutiveAnswer;

    // construction
    public Scoreboard()
    {
        time = 0f;
        consecutiveAnswer = 0;
    }

    //reset the attribute
    public void reset()
    {
        time = 0f;
        consecutiveAnswer = 0;
    }
}
