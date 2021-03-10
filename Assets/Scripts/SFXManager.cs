using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
	public string name;
	public AudioClip clip;
	private AudioSource source;
	[Range(0.5f, 1.5f)]
	[SerializeField] float pitch = 0.7f;
	[Range(0f, 1f)]
	[SerializeField] float volume = 1f;

	public void SetSource(AudioSource _source)
	{
		source = _source;
		source.clip = clip;
	}

	public void Play()
	{
		source.volume = volume;
		source.pitch = pitch;
		source.Play();
	}
}

public class SFXManager : MonoBehaviour
{
	private static SFXManager instance;
	public static SFXManager GetInstance() { return instance; }

	[SerializeField] private Sound[] sounds;
	[SerializeField] private AudioMixerGroup SFXMixerGroup;
	[SerializeField] private AudioMixerGroup BGMixerGroup;

	private void Awake()
	{
		instance = this;
		int sfxManagerCount = FindObjectsOfType<SFXManager>().Length;
		if (sfxManagerCount > 1)
		{
			Destroy(this.gameObject);
		}
		DontDestroyOnLoad(this.gameObject);
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}


	void OnDisable()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (sounds != null)
		{
			for (int i = 0; i < sounds.Length; i++)
			{
				GameObject _go = new GameObject("Sound_" + i + "_" + sounds[i].name);
				AudioSource newSource = _go.AddComponent<AudioSource>();
				sounds[i].SetSource(newSource);
				AssignMixer(newSource);
			}
		}
	}

	public void PlaySound(string _name)
	{
		for (int i = 0; i < sounds.Length; i++)
		{
			if (sounds[i].name == _name)
			{
				sounds[i].Play();
				return;
			}
		}
		Debug.Log("Sound Not found in the list " + _name);
	}

	public void AssignMixer(AudioSource source)
	{
		source.outputAudioMixerGroup = SFXMixerGroup;
	}

	public float GetSFXMixerVolume()
	{
		float vol;
		SFXMixerGroup.audioMixer.GetFloat("SFXVolume", out vol);
		return vol;
	}

	public float GetBGMixerVolume()
	{
		float vol;
		BGMixerGroup.audioMixer.GetFloat("BGVolume", out vol);
		return vol;
	}

	public void SetSFXMixerVolume(float volume)
	{
		SFXMixerGroup.audioMixer.SetFloat("SFXVolume", volume);
	}

	public void SetBGMixerVolume(float volume)
	{
		BGMixerGroup.audioMixer.SetFloat("BGVolume", volume);
	}
}
