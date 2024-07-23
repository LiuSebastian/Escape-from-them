using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource shoot;
    public AudioSource reload;
    public AudioSource pickUp;
    public AudioSource getHurt;
    public AudioSource doors;
    public void ShootSound()
    {
        shoot.Play();
    }
    public void Reload()
    {
        reload.Play();
    }
    public void PickUp()
    {
        pickUp.Play();
    }
    public void GetHurt()
    {
        getHurt.Play();
    }
    public void OpenDoors()
    {
        doors.Play();
    }
}
