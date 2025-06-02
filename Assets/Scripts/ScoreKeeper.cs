using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading;
using System.IO;

public class ScoreKeeper : Singleton<ScoreKeeper>
{
    public int ID;
    public int score;
    public string userName;
    public int IDregion;


    public int GetID()
    {
        return ID;
    }
    public string GetUserName()
    {
        return userName;
    }

    public int GetScore()
    {
        return score;
    }
    public int GetIDregion()
    {
        return IDregion;
    }

    public void ModifyScore(int value)
    {
        score += value;
        Mathf.Clamp(score, 0, int.MaxValue);
    }

    public void ResetScore(string userName, int IDregion)
    {
        score = 0;
        this.ID = Random.Range(0,19999);
        this.userName = userName;
        this.IDregion = IDregion;
    }
    
}
