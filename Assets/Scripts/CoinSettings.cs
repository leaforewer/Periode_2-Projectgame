using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSettings : MonoBehaviour
{
    public float turnspeed = 100;

    void Update()
    {
        transform.Rotate(Vector3.up * turnspeed * Time.deltaTime);
    }
}
