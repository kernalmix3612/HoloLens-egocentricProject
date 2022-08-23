using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Data;
using TMPro;
using System.Text;



#if WINDOWS_UWP
using Windows.Storage;
using Windows.System;
using System.Threading.Tasks;
using Windows.Storage.Streams;
#endif

public class Experience : MonoBehaviour
{
    public TextAsset inputExpeDataFile;
    public GameObject SolidCube;
    public GameObject WireFrameCube;
    private string[] conditions;
    public List<float> dist = new List<float>();// Object distance
    public List<string> Rendertype = new List<string>();//object render
    public float applydistance;//Distance we apply
    public string applycondition; //Condtition we apply
    private bool spacekeypress = false;
    private int SpacekeyPressCount = 0;
    int ClickingTime = 0;
    public int seconds;
    //List<int> Nums = new List<int>();//List for 0~30
    int[] randomArray = new int[30];//random List for 0~30
    public int p; //read List-X value
    //public TMP_InputField ResultField;
    public TextMeshPro ResultField;//input show
    string myString = "";//user's input
    public float distance;
    //Timecount Timecount;//time count
    private int PartCount = 0;//Participant Count
    private int TrailNum;
    float timer_f = 0f;
    private bool timegoing;
    DataTable dt = new DataTable();
    public TextMeshProUGUI ResultMessage_Holder;
    public TextMeshProUGUI Alert_Message;
    //TakeMessage TakeMessage;
    // Start is called before the first frame update
    void Start()
    {
        //ResultField.Select();
        ResultMessage_Holder.text = "Welcome to AR Experience! You are participant No."+ PartCount;
        //Alert_Message.text = "Here will show your result";
        SolidCube.transform.position = new Vector3(0, -0.95f, 1.5f);
        WireFrameCube.transform.position = new Vector3(0, -0.95f, 2.0f);
        SolidCube.SetActive(false);
        WireFrameCube.SetActive(false);
        conditions = inputExpeDataFile.text.Split(new char[] { '\r' }); //List experience condition
        dt.Columns.AddRange(new DataColumn[6] { new DataColumn("Participants"), new DataColumn("TrailNum"), new DataColumn("Distance"), new DataColumn("Render"), new DataColumn("result"), new DataColumn("time") });

        for (int i = 1; i < 31; i++)
        {
            string[] vs = conditions[i].Split(new char[] { ',' });
            //print(conditions[i]);
            float transvalue = float.Parse(vs[1]);//Find Object Distance
            //print(transvalue.GetType());
            dist.Add(transvalue);//Add all ditance value in to distance list
            Rendertype.Add(vs[2]);//Add all render type in to render list
        }
        //for (int i = 0; i < 30; i++)
        //{
        //    Nums.Add(i);
        //}
        
        Debug.Log("StartAPP");
    }

    private void randomdistance() {
        System.Random rnd = new System.Random();  //generate random number
        for (int i = 0; i < 30; i++)
        {
            randomArray[i] = rnd.Next(0, 30);   //generate random number，the range will be 0~29

            for (int j = 0; j < i; j++)
            {
                while (randomArray[j] == randomArray[i] && (randomArray[i] - randomArray[j]) / 5 == 0) //Check if there is a duplicate of the previously generated value, and if so, regenerate
                {
                    j = 0;  //If there is a repetition, set the variable j to 0 and check again (because there is still a possibility of repetition)
                    randomArray[i] = rnd.Next(0, 30);   //regenerate random number，the range will be 0~29
                }
            }
        }
    }

    private void OriginalCondition()// Back to start
    {
        //ResultField.Select();
        
        ResultMessage_Holder.text = "Welcome to AR Experience! You are participant No." + PartCount;
        SolidCube.transform.position = new Vector3(0, -0.95f, 2.5f);
        WireFrameCube.transform.position = new Vector3(0, -0.95f, 3.0f);
        SolidCube.SetActive(false);
        WireFrameCube.SetActive(false);
        conditions = inputExpeDataFile.text.Split(new char[] { '\r' }); //List experience condition
        //dt.Columns.AddRange(new DataColumn[4] { new DataColumn("Participants"), new DataColumn("TrailNum"), new DataColumn("result"), new DataColumn("time") });
        ClickingTime = 0;
        SpacekeyPressCount = 0;

        for (int i = 1; i < 31; i++)
        {
            string[] vs = conditions[i].Split(new char[] { ',' });
            //print(conditions[i]);
            float transvalue = float.Parse(vs[1]);//Object Distance
            //print(transvalue.GetType());
            dist.Add(transvalue);
            Rendertype.Add(vs[2]);//
        }
        System.Random rnd = new System.Random();  //generate random number
        for (int i = 0; i < 30; i++)
        {
            randomArray[i] = rnd.Next(0, 30);   //generate random number，the range will be 0~29

            for (int j = 0; j < i; j++)
            {
                while (randomArray[j] == randomArray[i] && (randomArray[i] - randomArray[i]) / 5 == 0)    //Check if there is a duplicate of the previously generated value, and if so, regenerate
                {
                    j = 0;  //If there is a repetition, set the variable j to 0 and check again (because there is still a possibility of repetition)
                    randomArray[i] = rnd.Next(0, 29);   //regenerate random number，the range will be 0~29
                }
            }
        }
        Debug.Log("StartAPP");

    }

    private void ApplyCondition(float distance, string rendering)
    {
        if (rendering.Equals("s"))
        {
            SolidCube.SetActive(true);
            WireFrameCube.SetActive(false);
            SolidCube.transform.position = new Vector3(0, -0.95f, distance);
        }
        else if (rendering.Equals("w"))
        {
            SolidCube.SetActive(false);
            WireFrameCube.SetActive(true);
            WireFrameCube.transform.position = new Vector3(0, -0.95f, distance);
        }
        else
        {
            SolidCube.SetActive(false);
            WireFrameCube.SetActive(false);
            SolidCube.transform.position = new Vector3(0, -0.95f, 2.5f);
            WireFrameCube.transform.position = new Vector3(0, -0.95f, 3.0f);
        }
    }

    public void SaveDATA(DataTable msheet)// Create CSV method
    {
        string filename = "Experience" + System.DateTime.Now.ToString("MM-dd-yy_hh-mm-ss") + ".csv";
        //string filePath = Application.dataPath + "\\data.csv";
        string filePath = System.IO.Path.Combine(Application.persistentDataPath,filename); //Before is dataPath
        if (msheet.Rows.Count < 1)

            return;
        int rowCount = msheet.Rows.Count;
        int colCount = msheet.Columns.Count;
        StringBuilder stringBulider = new StringBuilder();
        for (int i = 0; i < msheet.Columns.Count; i++)
        {
            stringBulider.Append(msheet.Columns[i].ColumnName + ",");
        }
        stringBulider.Append("\r\n");
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < colCount; j++)
            {
                stringBulider.Append(msheet.Rows[i][j] + ",");
            }
            stringBulider.Append("\r\n");
        }
        using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        {
            using (TextWriter textWriter = new StreamWriter(fileStream, Encoding.UTF8))
            {
                textWriter.Write(stringBulider.ToString());
            }
        }
    }

    public void filldata(float res, float tim)
    {
        //seconds = Timecount.timeplaying;
        //timer += Time.deltaTime;
        // turn seconds in float to int
        //seconds = (int)(timer % 60);
        if (dt.Rows.Count <= 29)
        {
            dt.Rows.Add(PartCount, TrailNum,applydistance,applycondition, res, tim);//add ParticipantsCount, TrailNum, result and time
            //Debug.Log("Distance is " + res);
            //Debug.Log("Time is " + tim);
           // Debug.Log("Participant" + PartCount);
            string RW = dt.Rows.Count.ToString();
            Debug.Log("Row count = " + RW);
            //print(UserResult.ToString());
            //Debug.Log("Distance="+UserResult);
        }
        //if (dt.Rows.Count % 30 == 0)
        //{
           // //Debug.Log("It's full");
            //Debug.Log("Next One Please");
        //}
        if (dt.Rows.Count == 30)
        {
            ResultMessage_Holder.text = "Thanks! Press Tab and pass to Next people";
            PartCount += 1;// Count Participants
            SaveDATA(dt);
            string RWFull = dt.Rows.Count.ToString();
            //Debug.Log(RWFull);
            /*RemoveResult(dt);*/// wait for test

        }
    }
    public void SwitchDistance()
    {
        if (ClickingTime < 30)
        {
            ClickingTime += 1;
            int render_condition = ClickingTime - 1;
            //print("Clicking time = " + ClickingTime);
            Debug.Log("Condition=" + randomArray[render_condition]);
            TrailNum = randomArray[render_condition];//Trail number
            applydistance = dist[TrailNum];//Put random distance into applydistance
            applycondition = Rendertype[TrailNum];// put rendertype into applycondition
        }
        else if (ClickingTime == 30)
        {
            ClickingTime = 0;
            //for (int i = 0; i < 30; i++)
            //{
            //    Nums.Add(i);
            //}
        }
        ApplyCondition(applydistance, applycondition);//Change distance and render type
        //UserAns = ((float)ResultiInputField.GetComponent<float>());
        //filldata(UserAns);
    }
    public void printarray() {
        foreach(int human in randomArray)
        {
            Debug.Log(human);
        }
    }
    // Update is called once per frame
    void Update()
    {
        //ResultField.Select();

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Debug.Log("Backspace pressed");
            if (myString.Length > 0)
            {
                myString = myString.Substring(0, myString.Length - 1);
            }
            else
            {
                myString = "";
            }
        }
        //else {
        //    myString = "";
        //}

        if (Input.inputString != "" )
            myString += Input.inputString;

        ResultField.text = myString;

        if (Input.GetKeyDown(KeyCode.Delete))
        {
            //myString = "";
            myString = myString.Substring(0, myString.Length - 1);
        }

        if (Input.GetKeyDown(KeyCode.Space)) //measure key button
        {
            SpacekeyPressCount += 1;
            spacekeypress = true;
            Debug.Log("SpaceKeyPress");
        };

        if (timegoing)//Time counting
        {
            timer_f += Time.deltaTime;
            //timer_i = (int)timer_f;
            //Debug.Log(timer_f);
        }
        if (Input.GetKeyDown(KeyCode.Tab)) {
            //Start();
            //dt.Reset();
            OriginalCondition();
            dt.Rows.Clear();
        }
    }

    void FixedUpdate()
    {
        if (SpacekeyPressCount == 1 && spacekeypress == true)
        {
            ResultMessage_Holder.text = "Press space to start";
            //TakeMessage.Instance.ResultCheck();
            SolidCube.SetActive(true);
            WireFrameCube.SetActive(true);
            Debug.Log("All are enabled");
            spacekeypress = false;
            //ResultField.Select();
            randomdistance();
            printarray();
        }
        if (SpacekeyPressCount == 2 && spacekeypress == true)
        {
            //ResultField.Select();
            ResultMessage_Holder.text = ClickingTime + " : " + "How far it is? Enter the distance and press Space button";
            //TakeMessage.Instance.ResultCheck();
            myString = "";
            timegoing = true;
            SwitchDistance();
            //timer_f += Time.deltaTime;
            spacekeypress = false;
            //Debug.Log(timer_f);
            //Timecount.istance.timestart();
        }
        if (SpacekeyPressCount == 3 && spacekeypress == true && ResultField.text != null)
        {
            //TakeMessage.Instance.ResultCheck();
            //ResultField.Select();
            distance = float.Parse(ResultField.text);
            ResultMessage_Holder.text = ClickingTime+" : "+"Your input = " +distance+ " Press Space button to continue";
            //ResultCheck(distance);
            timegoing = false;
            filldata(distance, timer_f);//write distance and time
            spacekeypress = false;
            SpacekeyPressCount = 1;
            ResultField.text = "Please input your guess";
            //Timecount.istance.endtime();
            timer_f = 0;
        }

    }
}
