using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using static System.Net.WebRequestMethods;

public class AudioSystem : MonoBehaviour
{


    private string audioUrl = "https://pagalfree.com/musics/128-Singham%20Again%20Title%20Track%20-%20Singham%20Again%20128%20Kbps.mp3";
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(DownloadAudio(audioUrl));
    }
    IEnumerator DownloadAudio(string url)
    {
        using (UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG))
        {
            yield return request.SendWebRequest();
            if(request.result == UnityWebRequest.Result.Success)
            {
                AudioClip clip = DownloadHandlerAudioClip.GetContent(request);
                if (clip == null)
                {
                    Debug.LogError("AudioClip is null. Ensure the file is in a supported format.");
                }
                else
                {
                    Debug.Log($"AudioClip loaded: {clip.name}, {clip.length} seconds, {clip.frequency} Hz");
                }
              //  clip.LoadAudioData();
                audioSource.clip = clip;

            }
            else
            {
                Debug.LogError($"failed to download audio: {request.error}");
            }
        }
    }
    public void PlayAudio()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
