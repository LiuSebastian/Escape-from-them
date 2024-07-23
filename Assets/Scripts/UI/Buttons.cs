using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    [SerializeField]
    private float increase;
    [SerializeField]
    private float decreaseX;
    [SerializeField]
    private float decreaseY;
    public void IncreaseSize()
    {
        transform.localScale += new Vector3(increase, increase, 0f);
    }
    public void DecreaseSize()
    {
        transform.localScale = new Vector3(decreaseX, decreaseY, 0f);
    }
}
