using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundZombie : MonoBehaviour
{
    public AudioSource zombieDeath;
    public AudioSource zombieSound;
    public void ZombieDeath()
    {
        zombieDeath.Play();
    }
    public void MuteSound()
    {
        zombieSound.GetComponent<AudioSource>().mute = true;
    }
}
