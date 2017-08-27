using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameData;

public class DataManager : MonoBehaviour {

	public static DataManager instance = null;

	public string mood = "";
	public string weather = "";


	void Awake()
	{
		if ( instance == null )
			instance = this;
		else if ( instance != this )
			Destroy(this);

	}

	public IEnumerator ParseCoroutine() {

		string url = "https://91igu4dgel.execute-api.ap-northeast-2.amazonaws.com/prod/tracks/suggestions?mood="+mood+"&weather="+weather+"&count=20";

		Debug.Log(url);

		WWWForm form = new WWWForm();
		WWW www = new WWW(url);
		yield return www;

		if (www.error == null) {

			int nMax = 0;
			int nIndex = 0;

			List<object> listArray = MiniJSON.jsonDecode(www.text) as List<object>;
			// Dictionary<string, object> dictData = listArray["track"] as Dictionary<string, object>;

			// List<object> trackData = dictData["track"] as List<object>;
			// Debug.Log(listArray.Count);

			for(int i = 0; i < listArray.Count; i++) {

				Dictionary<string, object> dicData = listArray[i] as Dictionary<string, object>;
			// 	Debug.Log(dicData.Count);

				// for (int j = 0; j < dicData.Count; j++) {
				// 	Dictionary<string, object> dictData = dicData["track"] as Dictionary<string, object>;	
				// 	Debug.Log(dicData.Values);		
				// }

				// int nDownloadCount = int.Parse(dicData["playback_count"]+"");
				// if ( nDownloadCount >= nMax ) {
				// 	nMax = nDownloadCount;
				// 	nIndex = i;
				// }
			}

			// Dictionary<string, object> data = trackData[nIndex] as Dictionary<string, object>;

			// Debug.Log( "최고점수 : " + nMax );
			// Debug.Log( "노래제목 : " + data["title"] );
			// Debug.Log( "노래제목 : " + data["download_url"] );

			// StartCoroutine("downloadMusic", data["download_url"]);

		} else {
			Debug.Log( www.error );
		}
	}

	IEnumerator downloadMusic(string url) {

		WWW www = new WWW(url);
		yield return www;
		AudioClip audiolist=www.audioClip;
		Debug.Log("Download over");
		// System.IO.File.WriteAllBytes(Application.dataPath + "/../Assets/Ressources/audio1.wav", www.bytes);

	}

	

}
