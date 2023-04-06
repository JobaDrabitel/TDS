using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayBack : MonoBehaviour
{
    [SerializeField] private AudioClip[] _audioClips;
    private AudioSource _audioSource;
    private float _musicVolume = 1f;
    public float MusicVolume { get => _musicVolume; }
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.loop = true;
        SetMusicVolume();
        StartCoroutine(PlayMusic());
    }
    private IEnumerator PlayMusic()
    {
        if (_audioSource != null)
        {
            while (true)
            {
                int currentClip = 0;
                _audioSource.clip = _audioClips[currentClip];
                _audioSource.Play();
                yield return new WaitForSeconds(_audioClips[currentClip].length);
                currentClip++;
                if (currentClip >= _audioClips.Length)
                    currentClip = 0;
            }
        }
    }
    public void SetMusicVolume()=>_audioSource.volume = PlayerPrefs.GetFloat("MusicVolume");

}
