using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour {

	public GameObject panel;

	// Use this for initialization
	void Start () {

		ChangeGamewayPanel( false );
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ChangeGamewayPanel( bool b ) {
		panel.SetActive(b);
	}

	public void startGame() {
		SceneManager.UnloadScene("Start");

		SceneManager.LoadScene("UI");
		SceneManager.LoadSceneAsync("InGame", LoadSceneMode.Additive);
	}
}
