using UnityEngine;
using System.Collections;

public class InGameManager : MonoBehaviour
{
    public static InGameManager Instance = null;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
