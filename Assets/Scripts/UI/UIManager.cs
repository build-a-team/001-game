using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

    [SerializeField]
    private Transform m_trnfCanvas;
    private Transform m_trnfHpPanel;
    private Text m_txtScore;

    private Image m_imgFace;

    [SerializeField]
    private Sprite m_sprtCharacter1;
    [SerializeField]
    private Sprite m_sprtCharacter2;

    // Use this for initialization
    void Start ()
    {
        InGameManager.Instance.OnCharacterHpChange += ChangeHp;
        InGameManager.Instance.OnItemGet += ScoreUp;
        Transform trnfTop = m_trnfCanvas.FindChild("TopPanel");
        m_trnfHpPanel = trnfTop.GetChild(0).GetChild(0);
        m_txtScore = trnfTop.GetChild(1).GetChild(0).GetComponent<Text>();
        m_imgFace = trnfTop.GetChild(0).GetComponent<Image>();

        ScoreUp(0);
        ChangeHp(0);
        ChangeFace(InGameManager.Instance.Character.Id); 
    }


	public void Jump()
    {
        SoundManager.instance.playSound(0);

        InGameManager.Instance.JumpButtonPress();
    }

    private void ChangeHp(int nValue)
    {
        if(InGameManager.Instance.Character.Hp == InGameManager.Instance.Character.MaxHp && InGameManager.Instance.Character.Hp == 5)
        {
            return;
        }
        else if(InGameManager.Instance.Character.Hp == InGameManager.Instance.Character.MaxHp && InGameManager.Instance.Character.Hp == 5)
        {
            transform.GetChild(4).GetComponent<Image>().sprite = m_trnfHpPanel.GetChild(4).GetComponent<Button>().spriteState.disabledSprite;
        }
        else
        {
            for (int i = 0; i < m_trnfHpPanel.childCount; i++)
            {
                m_trnfHpPanel.GetChild(i).GetComponent<Image>().sprite = m_trnfHpPanel.GetChild(i).GetComponent<Button>().spriteState.disabledSprite;
            }

            for (int i = 0; i < InGameManager.Instance.Character.Hp; i++)
            {
                m_trnfHpPanel.GetChild(i).GetComponent<Image>().sprite = m_trnfHpPanel.GetChild(i).GetComponent<Button>().spriteState.highlightedSprite;
            }
        }

        
    }

    private void ScoreUp(int nScore)
    {
        m_txtScore.text = nScore.ToString();
    }

    public void GameStop(bool bIsOn)
    {
        SoundManager.instance.playSound(0);
        Time.timeScale = Time.timeScale == 1 ? 0 : 1;
    }

    public void ChangeFace(int nType)
    {
        if (nType == 1)
            m_imgFace.sprite = m_sprtCharacter1;
        else
            m_imgFace.sprite = m_sprtCharacter2;
    }
}
