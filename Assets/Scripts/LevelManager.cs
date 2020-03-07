using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Manages the state of the level </summary>
public class LevelManager : MonoBehaviour
{
    public int Score { get; private set; }
    
    public void IncrementScore()
    {
        Score++;
    }

    public void Reset()
    {
        Score = 0;
        // reset logic
    }
}
