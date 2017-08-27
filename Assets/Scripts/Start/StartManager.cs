using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class StartManager : MonoBehaviour {

	public GameObject _wayPanel;
	public GameObject[] _stepPanels;

	private List<Sprite> list = new List<Sprite>();
	private List<GameObject> golist = new List<GameObject>();
	public List<GameObject> _weatherList = new List<GameObject>();
    public List<GameObject> _emogiList = new List<GameObject>();

	private int nStep = 0;
	private int nIndex = 0;

	private string strWeather = "";
	private string strEmogi = "";

	// Use this for initialization
	void Start () {
		// ChangeGamewayPanel( false );
	}
	
	public void ChangeGamewayPanel( bool b ) {
		if ( b ) {
			SoundManager.instance.playSound(1);
		} else {
			SoundManager.instance.playSound(2);
		}
		_wayPanel.SetActive(b);
	}

	public void StartGame() {

		if ( nStep >= _stepPanels.Length ) {

			Debug.Log(strWeather+" : "+strEmogi);
			DataManager.instance.weather = strWeather;
			DataManager.instance.mood = strEmogi;

			// DataManager.instance.StartCoroutine("ParseCoroutine");
			
			SceneManager.UnloadScene("Start");
			SceneManager.LoadScene("UI");
			SceneManager.LoadSceneAsync("InGame", LoadSceneMode.Additive);
			return;
		}

		initArray("Weathers", _weatherList);

		_stepPanels[ nStep ].SetActive( true );

		SoundManager.instance.playSound(1);
	}

	public void initArray(string str, List<GameObject> goList) {

		Sprite[] spts = Resources.LoadAll<Sprite>(str);
		
		list = new List<Sprite>();
		for (int i=0; i < spts.Length; i++) {
			list.Add( spts[i] );
		}

		golist = goList;

		nIndex = 0;
		for (int i=0; i < golist.Count; i++) {
			golist[i].GetComponent<Image>().sprite = list[nIndex+i];
		}
	}

	public void ChangeArray(bool b) {

		SoundManager.instance.playSound(0);

		int maxCount = nStep==0 ? 3 : 6;

		if ( b && nIndex+3 <= maxCount ) {
			nIndex += 3;
		}
		else if ( !b && nIndex-3 >= 0 ) {
			nIndex -= 3;
		}

		for (int i=0; i < 3; i++) {
			golist[i].SetActive(false);
		}

		for (int i=0; i < 3; i++) {
			if (nIndex+i == list.Count) return;
			golist[i].GetComponent<Image>().sprite = list[nIndex+i];
			golist[i].SetActive(true);
		}


	}

	public void ChoiceWeather(int idx) {
		SoundManager.instance.playSound(1);

		int nWeather = nIndex + idx;

		switch (nWeather) {
			case 0:
				strWeather = "cool";
				break;
			case 1:
				strWeather = "warm";
				break;
			case 2:
				strWeather = "warm";
				break;
			case 3:
				strWeather = "snowy";
				break;
			case 4:
				strWeather = "rainy";
				break;
		}

		_stepPanels[ nStep ].SetActive( false );
		nStep++;
		_stepPanels[ nStep ].SetActive( true );
		initArray("Emojis", _emogiList);
	}

	public void ChoiceEmogi(int idx) {
		SoundManager.instance.playSound(1);
		int nEmogi = nIndex + idx;

		switch (nEmogi) {
			case 0:
				strEmogi = "angry";
				break;
			case 1:
				strEmogi = "happy";
				break;
			case 2:
				strEmogi = "sad";
				break;
			case 3:
				strEmogi = "relaxed";
				break;
			case 4:
			case 5:
			case 6:
			case 7:
			case 8:
				strEmogi = "relaxed";
				break;
		}

		_stepPanels[ nStep ].SetActive( false );
		nStep++;

		StartGame();	
	}

}
