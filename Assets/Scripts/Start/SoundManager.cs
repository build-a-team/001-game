using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public static SoundManager instance = null;

	public AudioClip[] clips;
	private AudioSource audioSource;

	void Awake()
	{
		if ( instance == null )
			instance = this;
		else if ( instance != this )
			Destroy(this);

		DontDestroyOnLoad(gameObject);
		audioSource = GetComponent<AudioSource>();
	}

	public void playSound(int type) {
		audioSource.PlayOneShot(clips[type]);
	}
}
