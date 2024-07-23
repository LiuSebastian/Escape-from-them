using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierLeft : MonoBehaviour
{
    Animator anim;
    [SerializeField]
    int maxHP;
    [SerializeField]
    public int currentHP;
    public GameObject repairObject;
    public GameObject ui;
    private void Start()
    {
        anim = GetComponent<Animator>();
        currentHP = maxHP;
        ui.GetComponent<UI_Manager>().BarrierLifeLeft();
    }
    public void GetHit(int damage)
    {
        currentHP -= damage;
        ui.GetComponent<UI_Manager>().BarrierLifeLeft();
        if (currentHP >= 75)
        {
            anim.SetTrigger("Max Health");
        }
        else if (currentHP <= 74 && currentHP >= 25)
        {
            anim.SetTrigger("Half Health");
        }
        else
        {
            anim.SetTrigger("Low Health");
        }
        if (currentHP <= 0)
        {
            anim.SetTrigger("Destroyed");
            Destroyed();
        }
    }
    public void Repair()
    {
        if (currentHP == 0)
        {
            repairObject.SetActive(false);
            this.GetComponent<BoxCollider2D>().enabled = true;
        }
        currentHP += 50;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
        GetHit(0);
    }
    private void Destroyed()
    {
        currentHP = 0;
        this.GetComponent<BoxCollider2D>().enabled = false;
        repairObject.SetActive(true);
    }
}
