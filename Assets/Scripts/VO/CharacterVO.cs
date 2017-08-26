using UnityEngine;
using System.Collections;

public class CharacterVO
{
    public int Id;
    public int Hp;
    public int MaxHp;
    public int Speed;
    public float Position;

    public CharacterVO(int nId, int nMaxHp, int nSpeed, int nPosition)
    {
        Id = nId;
        MaxHp = nMaxHp;
        Hp = MaxHp;
        Speed = nSpeed;
        Position = nPosition;
    }
}
