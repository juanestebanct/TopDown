using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource AudioSourceShoot;

    public AudioClip Shoot;
    public AudioClip PickUp;
    public AudioClip UiButton;
    public AudioClip Explocion;

    public AudioClip SpawnEnemy;
    public AudioClip SpawnRay;

    private void Awake()
    {
        instance = this;
    }

    public void PlayClip(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
