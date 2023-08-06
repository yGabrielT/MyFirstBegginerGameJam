using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    [SerializeField] private AudioClip[] randomClip;
    [SerializeField] private AudioClip OneClip;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.spatialBlend = 1f;
        audioSource.volume = .4f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayingRandomAudio()
    {
        if(audioSource != null)
        {
            var numb = Random.Range(0, randomClip.Length);
            audioSource.PlayOneShot(randomClip[numb]);
        }
        
    }

    public void playNormalAudio()
    {
        audioSource.PlayOneShot(OneClip);
    }
}
