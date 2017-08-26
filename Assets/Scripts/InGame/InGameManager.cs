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
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    void Start()
    {
        m_characterController = GameObject.FindObjectOfType<InGameCharacterController>();

        Character = new CharacterVO(1, 5, 40, 0);
        
        for (int i = 0; i < 20; i++)
        {
            int n = Random.Range(0, 4) + 1;
            Debug.Log(n + " :: " + InGameData.Instance);
            ObstacleVO obstacle = InGameData.Instance.ObstacleList.Find(a => a.Type == n).Copy();
            obstacle.SetCreatePosition(2000 + (i * Random.Range(0, 5) * 60));
            ObstacleList.Add(obstacle);
            

        }
        CreateObstaclePool();
    }

    /// //////////////////////////////////////////////////
    /// Field
    /// 
    public float CharacterPosition;

    public float GroundYPosition = -339.5f;

    public List<ObstacleVO> ObstacleList = new List<ObstacleVO>();
    private int m_nObstacleIdx = 0;
    public Queue<GameObject> ObstaclePool = new Queue<GameObject>();
    

    public CharacterVO Character;
    private InGameCharacterController m_characterController;

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
}
