using System.Collections;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    delegate void Action();
    Action _MyDelegate;
    public GameObject bullets;
    public GameObject waveManager;
    public GameObject gameplayMenu;
    [SerializeField]
    int totalEnemyLeft;
    Vector3 bulletSpawn;
    private void Start()
    {
        _MyDelegate = SpawnBullets;
        totalEnemyLeft = waveManager.GetComponent<Enemy_Waves_Magager>().totalEnemyLeft;
        Debug.Log(totalEnemyLeft);
    }
    private void Update()
    {
        _MyDelegate();
    }
    public void SpawnBullets()
    {
        _MyDelegate = Sleep;
        bulletSpawn = new Vector3(Random.Range(-65, 66), transform.position.y, 0);
        GameObject instantiateBullet = Instantiate(bullets, bulletSpawn, bullets.transform.rotation);
        StartCoroutine(Wait());
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(20);
        _MyDelegate = SpawnBullets;
    }
    public void EnemiesKilled()
    {
        totalEnemyLeft--;
        if (totalEnemyLeft == 0)
        {
            Win();
        }
    }
    public void Win()
    {
        gameplayMenu.GetComponent<GameplayMenu>().Win();
    }
    public void Sleep()
    {

    }

}
