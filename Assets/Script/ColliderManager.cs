using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderManager : MonoBehaviour
{
    Rigidbody Rb;
    public GameObject objSelf;

    [SerializeField] Transform originPosition;
    [SerializeField] Transform obj;

    private Vector3 oriPos;
    private Quaternion oriRot;
    public AudioSource audioSource;
    public AudioClip[] arrAudio;
    public string CollideWithTag;

    public GameObject TaskManager;
    public int IndexTaskGroup;
    public int TaskIndex;
    public int TaskScenario;


    // Start is called before the first frame update
    void Start()
    {
        //HandColl = GetComponent<Collider>();
        Rb = GetComponent<Rigidbody>();

        // Store origin position
        originPosition = gameObject.GetComponent<Transform>();
        oriPos = originPosition.position;
        oriRot = originPosition.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        obj = gameObject.GetComponent<Transform>();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collide : " + other.gameObject.tag);
        if (other.gameObject.tag == CollideWithTag)
        {
            TaskDone();
        }
    }


    public void ResetPos()
    {
        Debug.Log("Reset Pos");

        obj.transform.position = oriPos;
        obj.transform.rotation = oriRot;
    }

    public void TaskDone()
    {
        objSelf.SetActive(false);
        audioSource.clip = arrAudio[0];
        audioSource.Play();

        switch (TaskScenario)
        {
            case 0: 

                switch (IndexTaskGroup)
                {
                    case 0:
                        BaterryDiag BD = TaskManager.GetComponent<BaterryDiag>();
                        BD.TaskGroups[IndexTaskGroup].TaskStatus[TaskIndex] = true;
                        BD.checkTask();

                        if (TaskIndex == 1)
                        {
                            BD.Step0(1);
                        }

                        break;
                    
                    case 3: // Connect OBD Scanner;
                        BaterryDiag BD3 = TaskManager.GetComponent<BaterryDiag>();
                        BD3.Step4();
                        ResetPos();
                        objSelf.SetActive(false);
                        break;
                    
                    default:
                        break;
                }

                break;

            default:
                break;
        }
            
    }


}
