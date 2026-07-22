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
    //[SerializeField] private Material handMaterial;

    [SerializeField] private GameObject LeftHand;
    [SerializeField] private GameObject RightHand;
    [SerializeField] private Material GloveMaterial;

    public GameObject Gloves;
    public GameObject Glasses;
    public GameObject WearedGlasses;

    [Header("Step 2")]
    public GameObject ArrowHint;
    public GameObject OpenLatchUI;
    public GameObject OpenButtonUI;
    public GameObject ToogleViewManager;
    public GameObject WarningGlovesUI;

   [Header("Step 3")]
    // Inspection Answer
    public bool[] InspectionAnswer;


    [Header("Step 4")]
    // 

    public GameObject table;
    public GameObject wire;
    public GameObject OBDScanner;
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

    [Header("Finished Objects")]
    public GameObject[] GlovesUnwear;
    public GameObject GlassesUnwear;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip Step1Audio;
    public AudioClip Step4Audio;
    public AudioClip WarningGloves;
        

    // Start is called before the first frame update
    void Start()
    {
        SlidePage.sprite = SlideSprites[0];
        // handMaterial.color = StartColor;

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


        // Initialize Audio Manager
        // Cache the AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();


        // Testing Audio
        PlayOneShotAudio(Step1Audio);
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

        Debug.Log("index Step : " + indexStep);

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
                // Set Outline
                Outline OL = OBDScanner.GetComponent<Outline>();
                OL.enabled = true;

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

                // Change material of the hand to glove material
                SkinnedMeshRenderer leftHandRenderer = LeftHand.GetComponent<SkinnedMeshRenderer>();
                leftHandRenderer.material = GloveMaterial;
                SkinnedMeshRenderer rightHandRenderer = RightHand.GetComponent<SkinnedMeshRenderer>();
                rightHandRenderer.material = GloveMaterial;

                Gloves.SetActive(false);

                CheckMark[0].SetActive(true);
                TaskGroups[0].TaskStatus[0] = true;
                checkTask();

                // Turn off Warning UI if weared in Step 2
                WarningGlovesUI.SetActive(false);
                OpenButtonUI.SetActive(true);

                break;
            
            case  1:

                // Wear Safety Glasses  

                Glasses.SetActive(false);

                CheckMark[1].SetActive(true);
                TaskGroups[0].TaskStatus[1] = true;
                WearedGlasses.SetActive(true);
                checkTask();

                // Because the Gloves is Optional
                nextButton.interactable = true;
                break;
            default:
            break;
        }
    }

    public void Step2()
    {
        if (TaskGroups[0].TaskStatus[0])
        {
            // Already wear gloves
            ToogleView TV = ToogleViewManager.GetComponent<ToogleView>();
            TV.NoExterior();
            TaskGroups[1].TaskStatus[0] = true;
            OpenLatchUI.SetActive(false);
            CheckMark[3].SetActive(true);
            checkTask();
        } else
        {
            // Not Wearing Gloves
            OpenButtonUI.SetActive(false);
            WarningGlovesUI.SetActive(true);
            PlayOneShotAudio(WarningGloves);
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

    public void GrabOBDScanner()
    {
        // Set Outline
        Outline OL = OBDScanner.GetComponent<Outline>();

        if (OL.enabled)
        {
            // Only played once
            PlayOneShotAudio(Step4Audio);
        }

        // Remove Outline On Grab
        OL.enabled = false;

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
                // Wrong Answer
                TaskGroups[2].TaskResultImage[i].enabled = true;
                TaskGroups[2].TaskResultImage[i].sprite = IncorrectAnswer;
            }
        }

        // Quiz Answer 6
        if (QuizAnswer[0].GetComponent<Toggle>().isOn)
        {
            TaskGroups[5].TaskResultImage[0].enabled = true;
            TaskGroups[5].TaskResultImage[0].sprite = CheckHiRes;
        } else
        {
            // Wrong Answer
            TaskGroups[5].TaskResultImage[0].enabled = true;
            TaskGroups[5].TaskResultImage[0].sprite = IncorrectAnswer;
        }


        // Set Equipment Unwear
        for (int i = 0; i < GlovesUnwear.Length; i++)
        {
            GlovesUnwear[i].SetActive(true);
        }

        GlassesManager GM = GlassesUnwear.GetComponent<GlassesManager>();
        GM.AfterPractice();

    }

    public void PlayOneShotAudio(AudioClip clip)
    {
        float volumeScale = 1f;
        if (clip == null) return;

        // Lazy-init: grab or add an AudioSource on this GameObject
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        // Stop previous clip before playing the new one
        audioSource.Stop();

        // Use Play() instead of PlayOneShot() so isPlaying tracks correctly
        // PlayOneShot creates a separate voice that ignores Stop/Play state
        audioSource.clip = clip;
        audioSource.volume = volumeScale;
        audioSource.loop = false;
        audioSource.Play();
    }

}
