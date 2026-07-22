using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GlassesManager : MonoBehaviour
{
    public GameObject GlassesGrab;
    public GameObject GlassesChild;
    public GameObject GlassesHover;

    public bool isGrab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGrab)
        {
            // Position and rotataion mirror
            GlassesGrab.transform.position = GlassesChild.transform.position;
            GlassesGrab.transform.rotation = GlassesChild.transform.rotation;
        };
    }

    public void OnGrab()
    {
        isGrab = true;
    }

    public void OnRelease()
    {
        isGrab = false;
    }

    public void AfterPractice()
    {
        GlassesGrab.SetActive(true);
        GlassesChild.SetActive(false);
        GlassesHover.SetActive(false);
    }

    // On Collision
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == GlassesHover)
        {
            // XR Grab
            XRGrabInteractable XRGrab = GlassesGrab.GetComponent<XRGrabInteractable>();
            XRGrab.enabled = false;

            // Position and rotataion mirror
            GlassesGrab.transform.position = GlassesHover.transform.position;
            GlassesGrab.transform.rotation = GlassesHover.transform.rotation;

            GlassesHover.SetActive(false);

            isGrab = true;
        }
    }


}
