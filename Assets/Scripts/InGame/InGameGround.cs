using UnityEngine;
using System.Collections;

public class InGameGround : MonoBehaviour
{
    private Animator m_ani;
    private int m_nGameSpeed = 40;
	// Use this for initialization
	void Start () {
        m_ani = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(InGameData.Instance.Speed != m_nGameSpeed)
        {
            m_nGameSpeed = InGameData.Instance.Speed;

            m_ani.speed = (float)m_nGameSpeed / 40.0f;
        }
	}
}
