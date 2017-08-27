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

    public List<ItemVO> ItemList = new List<ItemVO>();
    public List<GameObject> ItemPrefabList = new List<GameObject>();

    public int Speed = 40;

    private void Init()
    {
        Sprite[] sprts = Resources.LoadAll<Sprite>("Obstacles");

        for (int i = 0; i < sprts.Length; i++)
        {
            ObstacleSpriteList.Add(sprts[i]);
        }

        GameObject[] objs = Resources.LoadAll<GameObject>("Items");

        for (int i = 0; i < objs.Length; i++)
        {
            ItemPrefabList.Add(objs[i]);
        }

        ObstacleList.Add(new ObstacleVO(1, new Vector2(1, 4), true, new Vector2(0,190), new Vector2(128,128)));
        ObstacleList.Add(new ObstacleVO(2, new Vector2(1, 1), true, Vector2.zero, new Vector2(100, 100)));
        ObstacleList.Add(new ObstacleVO(3, new Vector2(1, 1), false, Vector2.zero, new Vector2(100, 100)));
        ObstacleList.Add(new ObstacleVO(4, new Vector2(1, 1), false, Vector2.zero, new Vector2(100, 100)));

        CharacterList.Add(new CharacterVO(1, 5, 40, 0));
        CharacterList.Add(new CharacterVO(2, 4, 60, 0));

        ItemList.Add(new ItemVO(1, true, false,1000));
        ItemList.Add(new ItemVO(2, false, true,1000));
        ItemList.Add(new ItemVO(3, true, true, 2000));
    }
}
