using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class InGameCharacterController : MonoBehaviour {


    private int m_nJumpCnt = 0;
    private Image m_img;
    private bool m_bInvincible = false;

    private Rigidbody2D m_rigidbody;
    // Use this for initialization
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_img = GetComponent<Image>();
        StartCoroutine(CharacterMove());

        InGameManager.Instance.OnJumpButtonPressed += Jump;
    }

    private IEnumerator CharacterMove()
    {
         while(true)
        {
            yield return new WaitUntil(() => InGameManager.Instance != null && InGameManager.Instance.Character != null);

            InGameManager.Instance.Character.Position += InGameManager.Instance.Character.Speed * 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void Init()
    {
        
    }

    public void Jump()
    {
        if (m_nJumpCnt >= 2)
            return;

        m_rigidbody.velocity = Vector2.up * 13;

        m_nJumpCnt++;
        //m_dtJump.tween.Restart();

    }

     void OnCollisionEnter2D(Collision2D col)
    {
        
        if (col.collider.CompareTag("Ground"))
        {
            if (m_nJumpCnt > 1)
                m_nJumpCnt = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (m_bInvincible == false)
        {

            if (col.CompareTag("Obstacle"))
            {
                float fVal = Random.Range(0.0f, 1.0f);

                if (fVal > InGameManager.Instance.Character.Evade)
                {
                    InGameManager.Instance.Character.Hp--;
                    FlashImage();
                    InGameManager.Instance.CharacterHpChange(-1);
                }
                else
                {
                    FlashRed();
                }
            }
        }
        if (col.CompareTag("Item"))
        {
            InGameItem item = col.GetComponent<InGameItem>();
            InGameManager.Instance.ItemAcquire(item.Item.Type);
            if(item.Item.HpUp)
            {
                if(InGameManager.Instance.Character.Hp < InGameManager.Instance.Character.MaxHp)
                {
                    InGameManager.Instance.Character.Hp++;
                    InGameManager.Instance.CharacterHpChange(1);
                }
            }
            if(item.Item.SpeedUp)
            {
                InGameData.Instance.Speed += 20;
            }
        }
    }
    private void FlashImage()
    {
        StartCoroutine(StartFlash());
    }

    private IEnumerator StartFlash()
    {
        m_bInvincible = true;
        m_img.CrossFadeAlpha(0.5f, 0.25f, false);
        yield return new WaitForSeconds(0.25f);
        m_img.CrossFadeAlpha(1.0f, 0.25f, false);
        yield return new WaitForSeconds(0.25f);
        m_img.CrossFadeAlpha(0.5f, 0.25f, false);
        yield return new WaitForSeconds(0.25f);
        m_img.CrossFadeAlpha(1.0f, 0.25f, false);
        yield return new WaitForSeconds(0.25f);
        m_img.CrossFadeAlpha(0.5f, 0.25f, false);
        yield return new WaitForSeconds(0.25f);
        m_img.CrossFadeAlpha(1.0f, 0.25f, false);
        yield return new WaitForSeconds(0.25f);
        m_img.CrossFadeAlpha(0.5f, 0.25f, false);
        yield return new WaitForSeconds(0.25f);
        m_img.CrossFadeAlpha(1.0f, 0.25f, false);
        yield return new WaitForSeconds(0.25f);
        m_bInvincible = false;
    }

    private void FlashRed()
    {
        StartCoroutine(FlashRedRoutine());
    }

    private IEnumerator FlashRedRoutine()
    {
        m_img.CrossFadeColor(Color.red, 0.5f, false, false);
        yield return new WaitForSeconds(0.25f);
        m_img.CrossFadeColor(Color.white, 0.5f, false, false);
        yield return new WaitForSeconds(0.25f);
    }
    
}
