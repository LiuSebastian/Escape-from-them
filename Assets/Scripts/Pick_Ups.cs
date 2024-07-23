using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick_Ups : MonoBehaviour
{
    public GameObject player;
    public GameObject soundManager;
    [SerializeField]
    int numberIndex;
    private void Awake()
    {
        player = FindObjectOfType<PJ_Detective>().gameObject;
        soundManager = FindObjectOfType<SoundManager>().gameObject;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            switch (numberIndex)
            {
                case 1:
                    PickUpAmmo();
                    break;
                case 2:
                    PickUpWood();
                    break;
            }
        }

    }
    void PickUpAmmo()
    {
        soundManager.GetComponent<SoundManager>().PickUp();
        player.GetComponent<PJ_Detective>().PickUpAmmo();
        Destroy(gameObject);
    }
    void PickUpWood()
    {
        soundManager.GetComponent<SoundManager>().PickUp();
        player.GetComponent<PJ_Detective>().PickUpWood();
        Destroy(gameObject);
    }
}

