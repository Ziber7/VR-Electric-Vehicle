using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToogleView : MonoBehaviour
{
    public GameObject FullExterior;
    public GameObject HalfExterior;
    // Start is called before the first frame update
    void Start()
    {
        FullExterior.SetActive(true);
        HalfExterior.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FullView()
    {
        FullExterior.SetActive(true);
        HalfExterior.SetActive(false);
    }

    public void HalfView()
    {
        FullExterior.SetActive(false);
        HalfExterior.SetActive(true);
    }

    public void NoExterior()
    {
        FullExterior.SetActive(false);
        HalfExterior.SetActive(false);
    }
}
