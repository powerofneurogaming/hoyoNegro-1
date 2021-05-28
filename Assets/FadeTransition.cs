// Soucre code created by ryanmillerca https://forum.unity.com/members/ryanmillerca.123917/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class FadeTransition : MonoBehaviour
{

    // the image you want to fade, assign in inspector
    //public List<Image> img;
    [SerializeField]
    private TextManager textManager;
    [SerializeField]
    private Button nextImageButton;
    [SerializeField]
    private GameObject textBox;
    [SerializeField]
    private Button textboxButton;
    [SerializeField]
    private List<GameObject> img;

    private int index = 0;
    public void OnClick()
    {

        if (index + 1 < img.Count)
        {
            Debug.Log("click");
            StartCoroutine(FadeImage(index));
            index++;
        }
        else
        {
            Debug.Log("end of slideshow");
            //start next scene
        }
    }
    IEnumerator FadeImage(int index)
    {
        textboxButton.interactable = false;
        nextImageButton.interactable = false;

        SpriteRenderer currentImage = img[index].GetComponent<SpriteRenderer>();
        SpriteRenderer nextImage = img[index + 1].GetComponent<SpriteRenderer>();
        nextImage.color = new Color(1,1,1,0);
        img[index].SetActive(true);
        img[index+1].SetActive(true);
        Debug.Log("start fade");
        
        // fade from opaque to transparent
        
            // loop over 1 second backwards
        for (float i = 1.5f; i >= 0; i -= Time.deltaTime)
        {
                // set color with i as alpha
                currentImage.color = new Color(1, 1, 1, i);
                yield return null;
        }
        img[index].SetActive(false);
        // fade from transparent to opaque
       
            // loop over 1 second
        for (float i = 0; i <= 2f; i += Time.deltaTime)
        {
                // set color with i as alpha
                nextImage.color = new Color(1, 1, 1, i/2f);
                yield return null;
        }
        Debug.Log("done");
        textboxButton.interactable = true;
        nextImageButton.interactable = true;

    }
    public void NextImage()
    {
        switch(textManager.state)
        {
            case TextManager.GameStates.Cartel:
                OnClick();
                textManager.state = TextManager.GameStates.CartelPt2;
                break;

            case TextManager.GameStates.CartelPt2:
                OnClick();
                textManager.state = TextManager.GameStates.Yucatan;
                break;

            case TextManager.GameStates.Yucatan:
                textManager.Part4();
                break;

            case TextManager.GameStates.VisLabNoText:
                textManager.Part7();
                break;

            case TextManager.GameStates.Google:
                OnClick();
                textManager.state = TextManager.GameStates.GooglePt2;
                break;

            case TextManager.GameStates.GooglePt2:
                OnClick();
                textManager.Part8();
                break;

            case TextManager.GameStates.End:
                //load next scene
                break;

            default:
                break;
        }
    }
}

