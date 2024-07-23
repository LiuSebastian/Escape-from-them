using System.Collections;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    SpriteRenderer sp;
    public Transform target;
    public GameObject soundManager;
    delegate void Delegate();
    Delegate _MyDelegate;
    public GameObject gameManager;
    public GameObject player;
    public GameObject barrierRight;
    public GameObject barrierLeft;
    public GameObject[] drop;

    [SerializeField]
    float speed;
    [SerializeField]
    int maxHP = 3;
    [SerializeField]
    int currentHP;
    [SerializeField]
    int damage;
    bool attacking = false;
    public bool notDestroyed;
    int index;
    public bool prueba = false;
    private void Awake()
    {
        player = FindObjectOfType<PJ_Detective>().gameObject;
        gameManager = FindObjectOfType<Game_Manager>().gameObject;
        target = FindObjectOfType<PJ_Detective>().transform;
        barrierRight = FindObjectOfType<BarrierRight>().gameObject;
        barrierLeft = FindObjectOfType<BarrierLeft>().gameObject;
    }
    private void Start()
    {
        currentHP = maxHP;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        _MyDelegate = Movement;
    }
    private void Update()
    {
        _MyDelegate();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            GetHit();
        } 
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _MyDelegate = Attack;
        }
        if (collision.gameObject.tag == "BarrierLeft")
        {
            _MyDelegate = DestroyBarrier;
            index = 1;
        }
        if (collision.gameObject.tag == "BarrierRight")
        {
            _MyDelegate = DestroyBarrier;
            index = 2;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "BarrierLeft" || collision.gameObject.tag == "BarrierRight")
        {
            _MyDelegate = Movement;
        }       
    }
    private void Movement()
    {
        if (transform.position.x < target.position.x) //If player is on his left side
        {
            rb.velocity = new Vector2(speed, 0);
            sp.flipX = false;
        }
        else if (transform.position.x > target.position.x)//If player is on his right side
        {
            rb.velocity = new Vector2(-speed, 0);
            sp.flipX = true;
        }
    }
    private void Attack()
    {
        if (!attacking)
        {
            rb.velocity = new Vector2(0, 0);
            anim.SetTrigger("Attack");
            player.GetComponent<PJ_Detective>().GetHit(damage);
            attacking = true;
            StartCoroutine(CdBetweenAttacks());
        }
    }
    private void DestroyBarrier()
    {
        if (!attacking)
        {
            rb.velocity = new Vector2(0, 0);
            anim.SetTrigger("Attack");
            if (index == 1)
            {
                barrierLeft.GetComponent<BarrierLeft>().GetHit(damage);
            }
            else
            {
                barrierRight.GetComponent<BarrierRight>().GetHit(damage);                
            }
            attacking = true;
            StartCoroutine(CdBetweenAttacks());
        }
    }
    IEnumerator CdBetweenAttacks()
    {
        yield return new WaitForSeconds(2);
        attacking = false;
    }
    public void GetHit()
    {
        currentHP--;
        if (currentHP <= 0)
        {
            Death();
        }
    }
    public void Death()
    {
        gameManager.GetComponent<Game_Manager>().EnemiesKilled();
        soundManager.GetComponent<SoundZombie>().ZombieDeath();
        soundManager.GetComponent<SoundZombie>().MuteSound();
        StartCoroutine(Wait());
        anim.SetTrigger("Death");
        if (Random.Range(1,11) < 8)
        {
            GameObject dropInstantiate = Instantiate(drop[Random.Range(0,2)], new Vector3(transform.position.x, -2.8f, transform.position.z), transform.rotation);
        }
        Destroy(gameObject, 5);
        GetComponent<Zombie>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
        soundManager.SetActive(false);
    }
}
