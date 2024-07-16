using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class GameData
{
    public int lifeCount;
    
    /*
     * The values defined in this constructor will be the default values
     * the game starts with when player starts a new game.
     */
    public GameData()
    {
        this.lifeCount = 3;
    }
}
