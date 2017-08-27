using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class InGameManager : MonoBehaviour
{
 
    /// //////////////////////////////////////////////////
    /// Singleton
    public static InGameManager Instance = null;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Init();
        }
        else
            Destroy(this.gameObject);
    }

    void Init()
    {
        
        StartCoroutine(InitRoutine());

        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    private IEnumerator InitRoutine()
    {
        yield return new WaitUntil(() => InGameData.Instance != null && InGameData.Instance.CharacterList.Count != 0);

        bool bCharacterId = DataManager.instance.mood == "angry" || DataManager.instance.mood == "sad";
        int nCharacterId = bCharacterId ? 2 : 1;
        
        Character = InGameData.Instance.CharacterList.Find(a =>a.Id == nCharacterId);

        if(nCharacterId == 2)
        {
            GameObject obj = GameObject.Find("Character_1");
            obj.SetActive(false);
            obj.transform.parent.FindChild("Character_2").gameObject.SetActive(true);;
        }

        m_characterController = GameObject.FindObjectOfType<InGameCharacterController>();

        InGameData.Instance.Speed = Character.Speed;
        CharacterHpChange(0);
        CreateObstaclePool();
    }

    void Start()
    {
        
        for (int i = 0; i < 200; i++)
        {
            int n = Random.Range(0, 4) + 1;
            ObstacleVO obstacle = InGameData.Instance.ObstacleList.Find(a => a.Type == n).Copy();
            obstacle.SetCreatePosition(2000 + (i * Random.Range(0, 5) * 60));
            ObstacleList.Add(obstacle);
            

        }
        
    }

  

    /// //////////////////////////////////////////////////
    /// Field
    /// 
    public float CharacterPosition;

    public float GroundYPosition = -339.5f;

    public List<ObstacleVO> ObstacleList = new List<ObstacleVO>();
    private int m_nObstacleIdx = 0;
    public Queue<GameObject> ObstaclePool = new Queue<GameObject>();

    public int Score = 0;

    public CharacterVO Character;
    private InGameCharacterController m_characterController;

    public delegate void CharacterHpChanged(int nChangeValue);
    public event CharacterHpChanged OnCharacterHpChange;

    public delegate void ItemGet(int nItemType);
    public event ItemGet OnItemGet;

    public delegate void JumpButtonPressed();
    public event JumpButtonPressed OnJumpButtonPressed;

    public AudioSource audioSource;

    /// //////////////////////////////////////////////////
    /// Method

    public void CreateObstaclePool()
    {
        GameObject obstacleObj = GameObject.Find("Obstacle");
        for (int i = 0; i < 9; i++)
        {
            GameObject obj = Instantiate<GameObject>(obstacleObj);
            obj.transform.SetParent(obstacleObj.transform.parent);
            obj.transform.localScale = obstacleObj.transform.localScale;
            obj.transform.SetSiblingIndex(obstacleObj.transform.GetSiblingIndex());
            ObstaclePool.Enqueue(obj);
            obj.SetActive(false);
        }
        Destroy(obstacleObj);
        StartCoroutine(CreateObstacleRoutine());
    }

    private IEnumerator CreateObstacleRoutine()
    {
        while (m_nObstacleIdx < ObstacleList.Count)
        {
            CreateObstacle();
            yield return new WaitForEndOfFrame();
        }
    }

    public void CreateObstacle()
    {
        if(ObstacleList[m_nObstacleIdx].CreatePosition - Character.Position < 2000)
        {
            GameObject obj = ObstaclePool.Dequeue();
            
            RectTransform rttrnf = obj.GetComponent<RectTransform>();
            rttrnf.sizeDelta = ObstacleList[m_nObstacleIdx].Size * 120;
            
            obj.transform.localPosition = new Vector3(ObstacleList[m_nObstacleIdx].CreatePosition - Character.Position + (960 + m_characterController.transform.localPosition.x),
                                            60 * ObstacleList[m_nObstacleIdx].Size.y - 300f,
                                            0);
            BoxCollider2D bxc = obj.GetComponent<BoxCollider2D>();
            bxc.offset = ObstacleList[m_nObstacleIdx].ColliderOffset;
            bxc.size = ObstacleList[m_nObstacleIdx].ColliderSize;
            obj.SetActive(true);
            obj.GetComponent<InGameObstacle>().Init(ObstacleList[m_nObstacleIdx]);
            m_nObstacleIdx++;
        }
    }

    public void RemoveObstacle(GameObject obj)
    {
        obj.SetActive(false);
        ObstaclePool.Enqueue(obj);
    }

    private IEnumerator ItemCreateRoutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(10.0f);

            int nVal = Random.Range(0, 5);
            if (nVal == 0)
                CreateItem();
        }
    }

    public void CreateItem()
    {
        GameObject item = InGameData.Instance.ItemPrefabList[Random.Range(0, 3)];
        item = Instantiate<GameObject>(item);

        Transform trnf = GameObject.Find("InGameCanvas").transform;
        item.transform.SetParent(trnf);
        item.transform.SetSiblingIndex(4);
        item.transform.localScale = new Vector3(1, 1, 1);
        item.transform.localPosition = new Vector3(2500, Random.Range(60, 300), 0);
    }

    ///////////////////////////////////////////////////////////////////
    /// 

    public void CharacterHpChange(int nChangeValue)
    {
        if(OnCharacterHpChange != null)
        {
            OnCharacterHpChange(nChangeValue);
        }
    }

    public void ItemAcquire(int nType)
    {
        Score += InGameData.Instance.ItemList.Find(a => a.Type == nType).Score;
        if (OnItemGet != null)
            OnItemGet(Score);
    }

    public void JumpButtonPress()
    {
        if (OnJumpButtonPressed != null)
            OnJumpButtonPressed();
    }

}
