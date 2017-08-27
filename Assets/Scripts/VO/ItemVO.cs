using UnityEngine;
using System.Collections;

public class ItemVO
{
    public int Type;
    public bool HpUp;
    public bool SpeedUp;
    public int Score;

    public ItemVO(int nType, bool bHpUp, bool bSpeedUp, int nScore)
    {
        Type = nType;
        HpUp = bHpUp;
        SpeedUp = bSpeedUp;
        Score = nScore;
    }
}
