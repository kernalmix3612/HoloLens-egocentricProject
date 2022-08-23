using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRandom : MonoBehaviour
{
    // Start is called before the first frame update
    private static CreateRandom _instance;
    int[] randomArray = new int[30];
    private void Awake()
    {
        Debug.Log("Create Random awake");
        _instance = this;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
