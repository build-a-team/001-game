using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class InGameObstacle : MonoBehaviour
{
    private DOTweenAnimation m_dtMove;
    private DOTweenAnimation m_dtSpecialEvent;
    private ObstacleVO obstacle;
    private Image m_img;
	// Use this for initialization

    void OnEnable()
    {
        if(m_dtMove == null)
            m_dtMove = GetComponent<DOTweenAnimation>();

        if (m_img == null)
            m_img = GetComponent<Image>(); 
        //m_dtMove.tween.Restart();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (obstacle.OnGround == true)
        {
            if (transform.localPosition.x < -50)
            {
            }
        }

        if(transform.localPosition.x < -1200)
        {
            InGameManager.Instance.RemoveObstacle(this.gameObject);
        }
	}

    public void Init(ObstacleVO obstaclevo)
    {
        obstacle = obstaclevo;
        m_img.sprite = InGameData.Instance.ObstacleSpriteList[obstaclevo.Type - 1];
        m_dtMove.tween.Restart();

        if (obstacle.OnGround == false)
        {
            m_dtMove.endValueV3.x += 200;
        }
        else
        {
            if(obstacle.Type == 1)
            {
                RectTransform rttrnf = GetComponent<RectTransform>();
                rttrnf.pivot = new Vector2(0.5f, 0);

                m_dtSpecialEvent = gameObject.AddComponent<DOTweenAnimation>();
                m_dtSpecialEvent.animationType = DG.Tweening.Core.DOTweenAnimationType.LocalRotate;
                m_dtSpecialEvent.loops = 100;
                m_dtSpecialEvent.loopType = LoopType.Yoyo;
                m_dtSpecialEvent.endValueV3 = new Vector3(0, 0, 30);
            }
        }
    }
}
