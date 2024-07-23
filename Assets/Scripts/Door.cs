using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    Animator anim;
    public GameObject soundManager;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            soundManager.GetComponent<SoundManager>().OpenDoors();
            anim.SetTrigger("Open");
            Debug.Log("entre");
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
