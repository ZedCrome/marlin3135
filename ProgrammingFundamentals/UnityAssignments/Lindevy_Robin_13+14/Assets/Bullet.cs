using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public AudioSource firstAudioSource;
    public AudioSource secondAudioSource;
    AudioSource audio;

    private void Start()
    {
        audio = gameObject.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        audio.clip = secondAudioSource;
        GetComponent<AudioSource>().Play();
        Destroy(gameObject, 0.5f);
        

        //Blow stuff up
    }
}
