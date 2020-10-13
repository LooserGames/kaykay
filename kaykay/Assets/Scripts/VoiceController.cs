using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VoiceController : MonoBehaviour
{
    public List<AudioClip> gameSfx = new List<AudioClip>();
    public GameObject music;
    private void Awake()
    {
        //  if (SceneManager.sceneCountInBuildSettings == 0)
        try
        {
            DontDestroyOnLoad(music);
        }
        catch (System.Exception)
        {
            Debug.Log("VoiceController Awake metodunu kontrol et");
        }
    }
    public void playVoice(AudioSource audioSource, int sfxId,float volume=1) //0=diamond, 1=zıplama, 2=portal, 3=speedup, 4=slide
    {
        audioSource.volume = volume;
        if (sfxId == 0)
            audioSource.volume = .6f;
        else
            audioSource.volume = 1;
        audioSource.clip = gameSfx[sfxId];
        if (sfxId == 3 && !audioSource.isPlaying)
            audioSource.Play();
        else if (sfxId != 3)
            audioSource.Play();
    }
    public IEnumerator playVoice(AudioSource audioSource, int sfxId, float delaySecond, float volume = 1) //0=diamond, 1=zıplama, 2=portal, 3=speedup, 4=slide
    {
        yield return new WaitForSeconds(delaySecond);
        playVoice(audioSource, sfxId, volume);
    }
}
