using UnityEngine;
using System.Collections;

public class ObstacleVO
{
    public int Type;
    public Vector2 Size;
    public int CreatePosition;
    public bool OnGround;
    public Vector2 ColliderOffset;
    public Vector2 ColliderSize;

    public ObstacleVO(int nType, Vector2 vcSize, bool bOnGround, Vector2 vcColliderOffset, Vector2 vcColliderSize)
    {
        Type = nType;
        Size = vcSize;
        OnGround = bOnGround;
        ColliderOffset = vcColliderOffset;
        ColliderSize = vcColliderSize;
    }

    public void SetCreatePosition(int pos)
    {
        CreatePosition = pos;
    }

    public ObstacleVO Copy()
    {
        return new ObstacleVO(Type, Size, OnGround, ColliderOffset, ColliderSize);
    }
}
