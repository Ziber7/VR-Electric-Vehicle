using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


[System.Serializable]
public class TaskGroup
{
    public string TaskName;
    public bool[] TaskStatus;
    public Image[] TaskResultImage;
}

public class BaterryDiag : MonoBehaviour
{
    [Header("Task Config")]
    public TaskGroup[] TaskGroups;
    public GameObject ViewConfig;
    public Sprite CheckHiRes;
    public GameObject ClipboardResult;


    [Header("UI Control")]

    public Image SlidePage;
    public Sprite[] SlideSprites;
    public Sprite ResultResult;
    public GameObject[] UIPageStep;
    public GameObject[] CheckMark;
    public Button nextButton;
    public Button prevButton;
    private int indexStep;
    public GameObject WarningIncomplete;
    public GameObject FinishButton;

    [Header("Step 1")]
    [SerializeField] private Material handMaterial;
    public Color StartColor;
    public Color customColor;
    public GameObject Gloves;
    public GameObject Glasses;
    public GameObject WearedGlasses;

    [Header("Step 2")]
    public GameObject ArrowHint;
    public GameObject OpenLatchUI;

   [Header("Step 3")]
    // Inspection Answer
    public bool[] InspectionAnswer;


    [Header("Step 4")]
    // 

    public GameObject table;
    public GameObject wire;
    //public GameObject ODS;
    public GameObject OBDScannerHover;
    public GameObject OBDScannerConnect;

    [Header("Step 5")]
    public GameObject OBDScreen;
    public bool[] ReadDiagStatus;
    public GameObject[] CheckMarkRead;

    [Header("Step 6")]
    public GameObject QuizAnswers;
    public Toggle[] QuizAnswer;
    public GameObject AnswerHint;
    public TextMeshProUGUI TextHint;
    public Image AnswerType;
    public Sprite CorrectAnswer;
    public Sprite IncorrectAnswer;
    



    // Start is called before the first frame update
    void Start()
    {
        SlidePage.sprite = SlideSprites[0];
        handMaterial.color = StartColor;

        // Button Prev Interactable False
        prevButton.interactable = false;
        nextButton.interactable = false;

        // UI Acc Step
        for (int i = 0; i < UIPageStep.Length; i++)
        {
           UIPageStep[i].SetActive(false);
        }

        // Page 1
        UIPageStep[0].SetActive(true);


        FinishButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void nextStep()
    {
        // To Avoid double click
        nextButton.interactable = false;

        // Increment sprite index of array
        indexStep = indexStep + 1;


        if (indexStep == SlideSprites.Length)
        {
            // Result
            nextButton.interactable = false;
            FinishButton.SetActive(true);
        } 
        else
        {
            SlidePage.sprite = SlideSprites[indexStep];
            nextButton.interactable = false;

             // UI Acc Step
            for (int i = 0; i < UIPageStep.Length; i++)
            {
                UIPageStep[i].SetActive(false);
            }

            // Page 1
            UIPageStep[indexStep].SetActive(true);

        };


        // Switching steps / Initilaize stage in the step
        switch (indexStep)
        {
            case 1:
                ArrowHint.SetActive(true);
                OpenLatchUI.SetActive(true);
                break;

            case 2:
                ArrowHint.SetActive(false);
                ToogleGroup TG = UIPageStep[2].GetComponent<ToogleGroup>();
                //TG.initialize();
                break;

            case 3:
                ToogleView TV = ViewConfig.GetComponent<ToogleView>();
                TV.FullView();

                table.SetActive(true);
                wire.SetActive(true);
                OBDScannerHover.SetActive(true);
                break;

            case 4:
                // Read is optional
                nextButton.interactable = true;
                // TaskGroups[4].TaskStatus[0] = true;
                OBDScreen.SetActive(true);
                checkTask ();
                break;
            default:
                break;
        }
    }

    public void checkTask ()
    {
        int taskCompleted = 0;

        for (int i = 0; i < TaskGroups[indexStep].TaskStatus.Length; i ++)
        {
            if (TaskGroups[indexStep].TaskStatus[i])
            {
                taskCompleted += 1;
            }
        };

        Debug.Log("taskCompleted : " + taskCompleted + " Length : " + TaskGroups[indexStep].TaskStatus.Length);

        // Check Task
        if (taskCompleted == TaskGroups[indexStep].TaskStatus.Length)
        {
            nextButton.interactable = true;
            Debug.Log("Complete");
        } else
        {   
            Debug.Log("Not Complete");
        };
    }
    

    public void Step0 (int a)
    {
        switch (a)
        {
            case 0:
                // Change material glove
                handMaterial.color = customColor;

                Gloves.SetActive(false);

                CheckMark[0].SetActive(true);
                TaskGroups[0].TaskStatus[0] = true;
                checkTask();

                break;
            
            case  1:

                // Wear Safety Glasses  

                Glasses.SetActive(false);

                CheckMark[1].SetActive(true);
                TaskGroups[0].TaskStatus[1] = true;
                WearedGlasses.SetActive(true);
                checkTask();
                break;
            default:
            break;
        }
    }

    public void Step3(int a)
    {
        // Visual Inspection
        TaskGroups[2].TaskStatus[a] = true;
        checkTask();
    }

    public void Step4()
    {
        OBDScannerHover.SetActive(false);
        TaskGroups[3].TaskStatus[0] = true;
        OBDScannerConnect.SetActive(true);
        CheckMark[2].SetActive(true);
        checkTask();
    }

    public void ReadingDiag(int a)
    {
        ReadDiagStatus[a] = true;
        CheckMarkRead[a].SetActive(true);

        // Set Task status (optional)
        TaskGroups[4].TaskStatus[a] = true;
    }

    public void ConditionQuiz()
    {
        ToggleGroup TG = QuizAnswers.GetComponent<ToggleGroup>();
        TG.allowSwitchOff = false;
        AnswerHint.SetActive(true);
        if (QuizAnswer[0].GetComponent<Toggle>().isOn)
        {
            // Correct
            AnswerType.sprite = CorrectAnswer;
            TextHint.text = "SoH masih tinggi, semua parameter dalam rentang normal, baterai layak digunakan tanpa tindakan lanjut.";
        } 
        else
        {
            // Incorrect
            AnswerType.sprite = IncorrectAnswer;
            TextHint.text = "Pengamatanmu terhadap SoH masih kurang cermat, mohon tinjau ulang jawaban.";
        }

        // Set task done
        TaskGroups[5].TaskStatus[0] = true;
        checkTask();
    }

    public void SetTaskDone(int a)
    {
        TaskGroups[indexStep].TaskStatus[a] = true;
        checkTask();
    }

    public void ResultPractice()
    {
        FinishButton.SetActive(false);
        UIPageStep[indexStep - 1].SetActive(false);
        SlidePage.sprite = ResultResult;

        // Set clipboard active
        ClipboardResult.SetActive(true);

        // Check Result All
        for (int i = 0; i < TaskGroups.Length; i ++)
        {
            for(int a = 0; a < TaskGroups[i].TaskStatus.Length; a ++)
            {
                if (TaskGroups[i].TaskStatus[a] == true)
                {
                    TaskGroups[i].TaskResultImage[a].enabled = true;
                    TaskGroups[i].TaskResultImage[a].sprite = CheckHiRes;

                }
            }
        }

        // Inspection Answer (Not Using The Task Status)
        for (int i = 0; i < InspectionAnswer.Length; i++)
        {
            if (InspectionAnswer[i] == true)
            {
                TaskGroups[2].TaskResultImage[i].enabled = true;
                TaskGroups[2].TaskResultImage[i].sprite = CheckHiRes;
            } else
            {
                TaskGroups[2].TaskResultImage[i].enabled = true;
                // wrong
            }
        }

        // Quiz Answer 6
        if (QuizAnswer[0].GetComponent<Toggle>().isOn)
        {
            TaskGroups[5].TaskResultImage[0].enabled = true;
            TaskGroups[5].TaskResultImage[0].sprite = CheckHiRes;
        } else
        {
            TaskGroups[5].TaskResultImage[0].enabled = true;
            // wrong
        }



    }



}
