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
    private bool m_bUp = false;
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
        if (obstacle == null)
            return;

        if (obstacle.Type == 2)
        {
            if (transform.localPosition.x < -50 && m_bUp == false)
            {
                Vector3 vc3 = transform.localPosition;
                vc3.y += 80;
                transform.localPosition = vc3;
                m_bUp = true;
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
        StartCoroutine(MoveRoutine());

        if (obstacle.Type == 2)
        {
            Vector3 vc3 = transform.localPosition;
            vc3.y = -325;
            transform.localPosition = vc3;
        }
        else if (obstacle.Type != 1)
        {
            Vector3 vc3 = transform.localPosition;
            vc3.y = Random.Range(-140,140);
            transform.localPosition = vc3;
        }
        else if(obstacle.Type == 1)
        {
            m_dtSpecialEvent = GetComponent<DOTweenAnimation>();
            m_dtSpecialEvent.tween.Restart();
        }
    }

    private IEnumerator MoveRoutine()
    {
        while(gameObject.activeInHierarchy)
        {
            yield return new WaitUntil(() => InGameData.Instance != null);

            Vector3 vc3 = transform.localPosition;
            vc3.x -= InGameData.Instance.Speed * 0.5f;

            if (obstacle.Type == 3 || obstacle.Type == 4)
            {
                vc3.x -= 10;
            }

            transform.localPosition = vc3;

            yield return new WaitForSeconds(0.01f);
        }
    }
}
