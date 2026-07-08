using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ComponentConfig : MonoBehaviour
{
    private Transform head;
    private GameObject selfObj;
    private Outline OL;
    public GameObject UIHover;
    // Start is called before the first frame update
    void Start()
    {
        selfObj = this.gameObject;
        Outline OL = selfObj.GetComponent<Outline>();
        OL.enabled = false;

        head = Camera.main.transform;
        UIHover.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (UIHover.activeSelf)
        {
            UIHover.transform.LookAt(new Vector3 (head.position.x, head.position.y, head.position.z));
            UIHover.transform.forward *= -1;
        }
    }

    public void switchOutline(bool s)
    {
        Outline OL = selfObj.GetComponent<Outline>();
        OL.enabled = s;
        UIHover.SetActive(s);
    }

    public void Selected()
    {
        Debug.Log("Tes Selected");
    }
}
