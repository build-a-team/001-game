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
    }

    // Update is called once per frame
    void Update()
    {
        if (m_nJumpCnt < 2 && Input.GetMouseButtonDown(0))
        {
            Jump();
        }        
    }

    private IEnumerator CharacterMove()
    {
         while(true)
        {
            InGameManager.Instance.Character.Position += InGameManager.Instance.Character.Speed * 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void Init()
    {
        
    }

    public void Jump()
    {
        m_rigidbody.velocity = Vector2.up * 300;

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
                InGameManager.Instance.Character.Hp--;
                FlashImage();
            }
        }
        if (col.CompareTag("Item") || col.CompareTag("Gold"))
        {

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
    
}
