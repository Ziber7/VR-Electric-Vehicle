using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlovesManager : MonoBehaviour
{
    public GameObject leftGlove;
    public GameObject rightGlove;

    public GameObject targetLeftGlove;
    public GameObject targetRightGlove;

    private MeshRenderer leftGloveRenderer;
    private MeshRenderer rightGloveRenderer;



    // Start is called before the first frame update
    void Start()
    {
        leftGloveRenderer = leftGlove.GetComponent<MeshRenderer>();
        rightGloveRenderer = rightGlove.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnGrab(bool isLeftHand)
    {
        if (isLeftHand)
        {
            leftGloveRenderer.enabled = true;
        }
        else
        {
            rightGloveRenderer.enabled = true;
        }
    }


    public void OnRelease(bool isLeftHand)
    {
        if (isLeftHand)
        {
            leftGloveRenderer.enabled = false;
        }
        else
        {
            rightGloveRenderer.enabled = false;
        }
    }

    public void OnSnap()
    {
        
    }
}
