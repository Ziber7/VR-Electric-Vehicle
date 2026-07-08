using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToogleGroup : MonoBehaviour
{
    // Public Variables 
    public List<Toggle> casing = new();
    public List<Toggle> terminal = new();
    public List<Toggle> leakage = new();
    public GameObject[] QuizGroup;
    [SerializeField] private bool[] isAnswer;
    [SerializeField] public bool[] Answer;
    public Toggle[] AnswerToggle;
    public int[] AnswerData;
    public GameObject TaskManager; 


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void initialize()
    {
        Debug.Log("Intialize");
        for (int i = 0; i < casing.Count; i++)
        {
            casing[i].isOn = false;
        }

        for (int i = 0; i < terminal.Count; i++)
        {
            terminal[i].isOn = false;
        }

        for (int i = 0; i < leakage.Count; i++)
        {
            leakage[i].isOn = false;
        }

    }

    
    public void savingValue()
    {
        BaterryDiag BD = TaskManager.GetComponent<BaterryDiag>();
        
        Debug.Log("Saving Value");
        // Checking Answer
        if (AnswerToggle[0].GetComponent<Toggle>().isOn)
        {
            Answer[0] = true;
        } else
        {
            Answer[0] = false;
        }

        if (AnswerToggle[1].GetComponent<Toggle>().isOn)
        {
            Answer[1] = true;
        } else
        {
            Answer[1] = false;
        }

        if (AnswerToggle[2].GetComponent<Toggle>().isOn)
        {
            Answer[2] = true;
        } else
        {
            Answer[2] = false;
        }

        // Saving Value of Answer
        for (int a = 0; a < Answer.Length; a ++)
        {
            BD.InspectionAnswer[a] = Answer[a];
        }
        
    }

    public void answerCasing(int a)
    {
        isAnswer[0] = true;

        if (casing[a].isOn)
        {
            AnswerData[0] = a;
            savingValue();
        }

        ToggleGroup TG = QuizGroup[0].GetComponent<ToggleGroup>();
        TG.allowSwitchOff = false;

        BaterryDiag BD = TaskManager.GetComponent<BaterryDiag>();
        BD.Step3(0); 
    }

    public void answerTerminal(int a)
    {
        isAnswer[1] = true;

        if (terminal[a].isOn)
        {
            AnswerData[1] = a;
            savingValue();
        }

        ToggleGroup TG = QuizGroup[1].GetComponent<ToggleGroup>();
        TG.allowSwitchOff = false;

        BaterryDiag BD = TaskManager.GetComponent<BaterryDiag>();
        BD.Step3(1); 
    }

    public void answerLeakage(int a)
    {
        isAnswer[2] = true;

        if (leakage[a].isOn)
        {
            AnswerData[2] = a;
            savingValue();
        }

        ToggleGroup TG = QuizGroup[2].GetComponent<ToggleGroup>();
        TG.allowSwitchOff = false;

        BaterryDiag BD = TaskManager.GetComponent<BaterryDiag>();
        BD.Step3(2); 
    }


}
