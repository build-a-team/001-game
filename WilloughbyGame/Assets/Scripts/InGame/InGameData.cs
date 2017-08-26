using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class InGameData : MonoBehaviour
{

    public static InGameData Instance = null;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Init();
        }
        else
            Destroy(this.gameObject);
    }

    public List<ObstacleVO> ObstacleList = new List<ObstacleVO>();
    public List<CharacterVO> CharacterList = new List<CharacterVO>();
    public List<Sprite> ObstacleSpriteList = new List<Sprite>();

    private void Init()
    {
        Sprite[] sprts = Resources.LoadAll<Sprite>("Obstacles");

        for (int i = 0; i < sprts.Length; i++)
        {
            ObstacleSpriteList.Add(sprts[i]);
        }

        ObstacleList.Add(new ObstacleVO(1, new Vector2(1, 4), true, new Vector2(0,190), new Vector2(128,128)));
        ObstacleList.Add(new ObstacleVO(2, new Vector2(1, 1), true, Vector2.zero, new Vector2(100, 100)));
        ObstacleList.Add(new ObstacleVO(3, new Vector2(1, 1), false, Vector2.zero, new Vector2(100, 100)));
        ObstacleList.Add(new ObstacleVO(4, new Vector2(1, 1), false, Vector2.zero, new Vector2(100, 100)));

        CharacterList.Add(new CharacterVO(1, 5, 40, 0));
        CharacterList.Add(new CharacterVO(2, 4, 60, 0));
    }
}
