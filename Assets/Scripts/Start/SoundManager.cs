using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public static SoundManager instance = null;

	public AudioClip[] clips;
	public AudioSource audioSource;
	public AudioSource effSource;

	void Awake()
	{
		if ( instance == null )
			instance = this;
		else if ( instance != this )
			Destroy(this);

		DontDestroyOnLoad(gameObject);
	}

	public void PlaySound(int type) {
		if (type == 1) {
			audioSource.PlayOneShot(clips[0]);
			effSource.PlayScheduled(5000);
		}
		else {
			audioSource.PlayOneShot(clips[type]);
		}
	}
}
