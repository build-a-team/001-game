using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class InGameObstacle : MonoBehaviour
{
    private DOTweenAnimation m_dtMove;
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
            m_dtMove.endValueV3.x += 200;
    }
}
