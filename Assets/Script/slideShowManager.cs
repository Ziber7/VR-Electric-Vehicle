using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slideShowManager : MonoBehaviour
{
    public Button nextButton;
    public Button prevButton;
    private int indexPage;

    public Image SlidePage;
    public Sprite[] SlideSprites;


    // Start is called before the first frame update
    void Start()
    {
        openPageNew();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openPageNew()
    {
        // Set all inactive
        SlidePage.sprite = SlideSprites[0];

        // Button Prev Interactable False
        prevButton.interactable = false;
        nextButton.interactable = true;
        indexPage = 0;

    }

    public void nextPage()
    {
        // To Avoid double click
        nextButton.interactable = false;

        // Increment sprite index of array
        indexPage = indexPage + 1;
        SlidePage.sprite = SlideSprites[indexPage];

        if (indexPage == (SlideSprites.Length - 1))
        {
            nextButton.interactable = false;
        } 
        else
        {
            nextButton.interactable = true;
        } 


        prevButton.interactable = true;
    }

    public void prevPage()
    {
        // To Avoid double click
        prevButton.interactable = false;


        // 
        indexPage = indexPage - 1;
        SlidePage.sprite = SlideSprites[indexPage];

        if (indexPage == 0)
        {
            prevButton.interactable = false;
        }
        else
        {
            prevButton.interactable = true;
        }

        nextButton.interactable = true;
    }
}
