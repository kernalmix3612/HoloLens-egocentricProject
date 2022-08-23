using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timemanager : MonoBehaviour
{
    public float timer_f;
    private bool timegoing;
    private void Awake()
    {
        Debug.Log("Timer awake");
    }
    void Start()
    {
        Debug.Log("Time Working");
    }
    void timer() {
            timer_f += Time.deltaTime;
    }
    void timestop() {
    }
}
