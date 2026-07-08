using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ToggleHover : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    public GameObject HoverCanvas;
    public TextMeshProUGUI ToolTip;
    public string textHover;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        HoverCanvas.SetActive(true);
        ToolTip.text = textHover;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HoverCanvas.SetActive(false);
    }
}
