using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameData;

public class DataManager : MonoBehaviour {

	public static DataManager instance = null;

	private string mood = "happy";
	private string weather = "rainy";

	void Awake()
	{
		if ( instance == null )
			instance = this;
		else if ( instance != this )
			Destroy(this);
	}

	void Start () {
	
		StartCoroutine( "ParseCoroutine" );
	}

	IEnumerator ParseCoroutine() {

		string url = "https://91igu4dgel.execute-api.ap-northeast-2.amazonaws.com/prod/tracks";

		WWWForm form = new WWWForm();
		WWW www = new WWW(url);
		yield return www;

		if (www.error == null) {

			int nMax = 0;
			int nIndex = 0;

			List<object> listData = MiniJSON.jsonDecode(www.text) as List<object>;

			for(int i = 0; i < listData.Count; i++) {

				Dictionary<string, object> dicData = listData[i] as Dictionary<string, object>;

				int nDownloadCount = int.Parse(dicData["download_count"]+"");
				if ( nDownloadCount >= nMax ) {
					nMax = nDownloadCount;
					nIndex = i;
				}
			}

			Dictionary<string, object> data = listData[nIndex] as Dictionary<string, object>;

			Debug.Log( "최고점수 : " + nMax );
			Debug.Log( "노래제목 : " + data["title"] );
			Debug.Log( "노래제목 : " + data["download_url"] );

			StartCoroutine("downloadMusic", data["download_url"]);

		} else {
			Debug.Log( www.error );
		}
	}

	IEnumerator downloadMusic(string url) {

		WWW www = new WWW(url);
		yield return www;
		AudioClip audiolist=www.audioClip;
		Debug.Log("Download over");
		System.IO.File.WriteAllBytes(Application.dataPath + "/../Assets/Ressources/audio1.wav", www.bytes);
	}

	public void sendTmp() {

		Debug.Log("Hi");

	}
}
