using System.Collections;
using UnityEngine;

public class Enemy_Waves_Magager : MonoBehaviour
{
    public GameObject uiManager;
    public GameObject gameManager;
    public GameObject[] enemy;
    public GameObject[] spawnPoint;

    public float spawnRate = 2f;
    public float timeBetweenWaves;

    delegate void Action();
    Action _MyDelegate;

    public int enemyCount;
    public int enemyCounter;
    public int totalEnemyLeft;
    int waveCount = 1;
    private void Awake()
    {
        for (int i = 1; i < 11; i++)
        {
            for (int h = 0; h < enemyCounter; h++)
            {
                totalEnemyLeft++;
            }
            enemyCounter += 4;
        }
        Debug.Log(totalEnemyLeft);
    }
    private void Start()
    {
        _MyDelegate = WaveSpawner;        
    }
    void Update()
    {
        _MyDelegate();
    }
    void WaveSpawner()
    {
        if (waveCount == 11)
        {
            Debug.Log(totalEnemyLeft);
            gameObject.SetActive(false);
        }
        else
        {
            StartCoroutine(waveSpawner());
        }
    }
    IEnumerator waveSpawner()
    {
        _MyDelegate = Sleep;
        uiManager.GetComponent<UI_Manager>().Waves(waveCount);
        for (int i = 0; i < enemyCount; i++)
        {
            if (waveCount <= 3)
            {
                GameObject enemyClone = Instantiate(enemy[0],spawnPoint[Random.Range(0,2)].transform.position,enemy[0].transform.rotation);
            }
            else //Beyong wave 3 there is a 2 out of 10 chances of spawning an armored zombie
            {
                if (Random.Range(1,11) <= 8) //Whether its a normal zombie or an armored zombie
                {
                    GameObject enemyClone = Instantiate(enemy[0], spawnPoint[Random.Range(0, 2)].transform.position, enemy[0].transform.rotation);
                }
                else
                {
                    GameObject enemyClone = Instantiate(enemy[1], spawnPoint[Random.Range(0, 2)].transform.position, enemy[1].transform.rotation);
                }
            }
            yield return new WaitForSeconds(spawnRate);
        }
        enemyCount += 4;
        waveCount += 1;

        yield return new WaitForSeconds(timeBetweenWaves);
        timeBetweenWaves -= 0.5f;
        _MyDelegate = WaveSpawner;
    }
    void Sleep()
    {

    }
}
