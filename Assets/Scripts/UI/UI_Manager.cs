using UnityEngine.UI;
using UnityEngine;
using TMPro;
public class UI_Manager : MonoBehaviour
{
    public TextMeshProUGUI waveCounterText;
    public TextMeshProUGUI ammoCounterText;
    public TextMeshProUGUI woodCounterText;

    public Slider sliderPlayerHealth;
    public Slider sliderBarrierHealthLeft;
    public Slider sliderBarrierHealthRight;
    public Slider sliderReload;
    public GameObject sliderReloadBar;
    public GameObject player;
    public GameObject barrierLeft;
    public GameObject barrierRight;

    delegate void Delegate();
    Delegate _MyDelegate;
    private void Awake()
    {
        _MyDelegate = Sleep;
    }
    private void Update()
    {
        _MyDelegate();
    }
    public void Waves(int counter)
    {
        waveCounterText.text = "Wave: " + counter.ToString();
    }
    public void PlayerLife()
    {
        sliderPlayerHealth.value = player.GetComponent<PJ_Detective>().currentHP;
    }
    public void BarrierLifeLeft()
    {
        sliderBarrierHealthLeft.value = barrierLeft.GetComponent<BarrierLeft>().currentHP;
    }
    public void BarrierLifeRight()
    {
        sliderBarrierHealthRight.value = barrierRight.GetComponent<BarrierRight>().currentHP;
    }
    public void Ammo(int magazineAmmo, int totalAmmo)
    {
        ammoCounterText.text = magazineAmmo + "/" + totalAmmo.ToString();
    }
    public void Wood(int wood)
    {
        woodCounterText.text = wood.ToString();
    }
    public void Reload()
    {
        _MyDelegate = Reload;
        sliderReloadBar.SetActive(true);
        sliderReload.value += 1 * Time.deltaTime;
        if (sliderReload.value >= 1.5f)
        {
            sliderReload.value = 0;
            sliderReloadBar.SetActive(false);
            _MyDelegate = Sleep;
        }
    }
    public void Sleep()
    {
        //Do nothing
    }
}
