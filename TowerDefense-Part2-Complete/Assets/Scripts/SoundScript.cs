using UnityEngine;

public class SoundScript : MonoBehaviour {

	public static SoundScript Instance;
	public AudioClip selectSound;
	public AudioClip hoverSound;
	public AudioClip pauseSound;
	
	void Awake()
	{
		// Register the singleton
		if (Instance != null)
		{
			Debug.LogError("Multiple instances of SoundScript!");
		}
		Instance = this;
		DontDestroyOnLoad(transform.gameObject);
	}
	
	public void MakePauseSound()
	{
		MakeSound(pauseSound);
	}
	
	public void MakeHoverSound()
	{
		MakeSound(hoverSound);
	}
	
	public void MakeSelectSound()
	{
		MakeSound(selectSound);
	}
	
	private void MakeSound(AudioClip originalClip)
	{
		// As it is not 3D audio clip, position doesn't matter.

		if (AppData.isSoundOn){
			AudioSource source = GetComponent<AudioSource>();
			source.clip = originalClip;
			source.loop = false;
			source.Play();
		}
	}
}
