using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TakeMessage : MonoBehaviour
{
    public TMP_InputField InputField;
    //public GameObject DispalyTool;
    public float distance;
    //public float UserAns;
    public TextMeshProUGUI ErrorMessage_Holder;
    const string ErrorMessage = "Please check your input";
    const string CorrectMessage = "Your input is ";
    public TextMeshProUGUI ResultMessage_Holder;
    private static TakeMessage _instance;
    public static TakeMessage Instance { get { return _instance; } }
    // Start is called before the first frame update
    private void Awake()
    {
        Debug.Log("Create Data awake");
        _instance = this;
    }
    void Start()
    {
        Debug.Log("TakeMessage Start Working");
    }

    public void ResultCheck()
    {
        distance = float.Parse(InputField.text);
        if (distance.Equals(null))
        {
            ErrorMessage_Holder.text = ErrorMessage;
            Debug.Log("Please fill the distance");
        }
        else if (distance.GetType() == typeof(float))
        {
            Debug.Log("Distance=" + distance);
            ResultMessage_Holder.text = CorrectMessage + distance;
        }
        else
        {
            ResultMessage_Holder.text = "Are you read? Press Space to Start";
        }

    }
}
