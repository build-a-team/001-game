using UnityEngine;
using System.Collections;

public class InGameItem : MonoBehaviour
{
    private ItemVO m_item;

    public ItemVO Item
    {
        get { return m_item; }
    }
	// Use this for initialization
	void Start ()
    {
        m_item = InGameData.Instance.ItemList.Find(a => a.Type == int.Parse(gameObject.name.Split('_')[1]));
    }   
}
