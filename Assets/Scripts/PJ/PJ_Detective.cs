using System;
using System.Collections;
using UnityEngine;

public class PJ_Detective : MonoBehaviour
{
    PJ_Controller control;
    Animator anim;
    public GameObject soundManager;
    public GameObject bullet;
    public GameObject shootPos;
    public GameObject uiPlayer;
    public GameObject gameMenu;
    public GameObject barrierLeft;
    public GameObject barrierRight;

    public GameObject flashlight;
    int speed = 5;
    int index;

    [SerializeField]
    bool canShoot = true;
    [SerializeField]
    bool isReloading = false;
    [SerializeField]
    bool canReload = true;

    [SerializeField]
    int maxHP;
    [SerializeField]
    public int currentHP;
    [SerializeField]
    int totalAmmo;
    [SerializeField]
    int magazineAmmo;
    [SerializeField]
    int currentMagazineAmmo;
    [SerializeField]
    int repairWood = 5;
    [SerializeField]
    bool canRepair = false;

    private void Start()
    {
        control = new PJ_Controller(this);
        anim = GetComponent<Animator>();
        currentHP = maxHP;
        currentMagazineAmmo = magazineAmmo;
        uiPlayer.GetComponent<UI_Manager>().BarrierLifeLeft();
        uiPlayer.GetComponent<UI_Manager>().Ammo(currentMagazineAmmo, totalAmmo);
        uiPlayer.GetComponent<UI_Manager>().Wood(repairWood);
    }
    private void Update()
    {
        control.OnUpdate();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BarrierLeft" || collision.gameObject.tag == "RepairBarrierLeft")
        {
            index = 1;
            if (barrierLeft.GetComponent<BarrierLeft>().currentHP == 100)
            {
                canRepair = false;
            }
            else
            {
                canRepair = true;
            }
        }
        if (collision.gameObject.tag == "BarrierRight" || collision.gameObject.tag == "RepairBarrierRight")
        {
            index = 2;
            if (barrierRight.GetComponent<BarrierRight>().currentHP == 100)
            {
                canRepair = false;
            }
            else
            {
                canRepair = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BarrierLeft" || collision.gameObject.tag == "BarrierRight" || collision.gameObject.tag == "RepairBarrier")
        {
            canRepair = false;
        }
    }
    public void Movement(float inputX, bool stay)
    {        
        if (!stay)
        {            
            if (inputX > 0) //Going right
            {
                //soundManager.GetComponent<SoundManager>().Walk();
                transform.position += transform.right * inputX * speed * Time.deltaTime;
                anim.SetBool("Stay", false);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                flashlight.transform.position = new Vector3(flashlight.transform.position.x, flashlight.transform.position.y, -0.7f);
            }
            if (inputX < 0) //Going left
            {
                //soundManager.GetComponent<SoundManager>().Walk();
                transform.position += transform.right * inputX * -speed * Time.deltaTime;
                anim.SetBool("Stay", false);
                transform.rotation = Quaternion.Euler(0, 180, 0);
                flashlight.transform.position = new Vector3(flashlight.transform.position.x, flashlight.transform.position.y, -0.7f);
            }
        }
        else
        {            
            anim.SetBool("Stay", true);
        }    
    }
    public void Shoot()
    {
        if (canShoot && !isReloading && currentMagazineAmmo > 0)
        {
            canShoot = false;
            isReloading = true; //So the player cant reload while shooting
            soundManager.GetComponent<SoundManager>().ShootSound();
            currentMagazineAmmo--;
            uiPlayer.GetComponent<UI_Manager>().Ammo(currentMagazineAmmo, totalAmmo);
            anim.SetTrigger("Shoot");
            StartCoroutine(Wait());
            StartCoroutine(CooldownShoot());
        }
        else if (currentMagazineAmmo == 0)
        {
            Reload();
        }
    }
    IEnumerator Wait() //Sinc bullet with Shoot Animation
    {
        yield return new WaitForSeconds(0.15f);
        GameObject bulletInstantiate = Instantiate(bullet, shootPos.transform.position, shootPos.transform.rotation);
    }
    IEnumerator CooldownShoot() //Cooldown betweeen shoots
    {
        yield return new WaitForSeconds(0.3f);
        canShoot = true;
        isReloading = false;
    }

    public void GetHit(int damage)
    {
        soundManager.GetComponent<SoundManager>().GetHurt();
        currentHP -= damage;
        uiPlayer.GetComponent<UI_Manager>().PlayerLife();
        anim.SetTrigger("GetHit");
        if (currentHP <= 0)
        {
            Death();
        }
    }
    public void Death()
    {
        anim.SetBool("Death", true);
        GetComponent<PJ_Detective>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        gameMenu.GetComponent<GameplayMenu>().Death();
    }
    public void PickUpAmmo()
    {
        totalAmmo += 10;
        uiPlayer.GetComponent<UI_Manager>().Ammo(currentMagazineAmmo, totalAmmo);
    }
    public void PickUpWood()
    {
        repairWood += 1;
        uiPlayer.GetComponent<UI_Manager>().Wood(repairWood);
    }
    public void Reload()
    {
        if (currentMagazineAmmo == 7)
        {
            canReload = false;
        }
        if (!isReloading && currentMagazineAmmo != 7)
        {
            isReloading = true;
            canShoot = false;
            uiPlayer.GetComponent<UI_Manager>().Reload();
            StartCoroutine(ReloadTime());
        }       
    }
    IEnumerator ReloadTime() //Cooldown betweeen shoots
    {
        soundManager.GetComponent<SoundManager>().Reload();
        yield return new WaitForSeconds(1.5f);
        if (totalAmmo <= magazineAmmo - 1)
        {
            currentMagazineAmmo = totalAmmo;
            totalAmmo = 0;
            uiPlayer.GetComponent<UI_Manager>().Ammo(currentMagazineAmmo, totalAmmo);
            Debug.Log("ultima recarga");
        }
        else
        {
            totalAmmo = totalAmmo - (magazineAmmo - currentMagazineAmmo);
            currentMagazineAmmo = magazineAmmo;
            uiPlayer.GetComponent<UI_Manager>().Ammo(currentMagazineAmmo, totalAmmo);
        }
        canShoot = true;
        isReloading = false;
    }
    public void RepairBarrier()
    {
        if (canRepair && repairWood > 0)
        {
            repairWood--;
            uiPlayer.GetComponent<UI_Manager>().Wood(repairWood);
            if (index == 1)
            {
                barrierLeft.GetComponent<BarrierLeft>().Repair();
            }
            else
            {
                barrierRight.GetComponent<BarrierRight>().Repair();
            }
        }
    }
}
