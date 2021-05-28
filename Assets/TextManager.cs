using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextManager : MonoBehaviour
{
    [SerializeField]
    private AudioManager audio;
    [SerializeField]
    private FadeTransition fade;
    [SerializeField]
    private AutoTyping autoType;
    [SerializeField]
    private GameObject textbox;
    [SerializeField]
    private GameObject nextImageButton;
    [SerializeField]
    private TMP_Text messageText;
    [SerializeField]
    private float FontSize;
    private List<string> allText;
    private int index;
    private int audioIndex;
    private float textSpeed;

    private void Start()
    {
        index = 0;
        audioIndex = 0;
        textSpeed = .04f;
        allText = new List<string>(10);
        //messageText.text = "Hello World";
        Part1();
        StartCoroutine(autoType.ShowText(allText[index], textSpeed));
        audio.PlayClip(audioIndex);
        //messageText.text = allText[index];
    }
    public enum GameStates
    {
        VisualLab,
        NewIceAge,
        Cartel,
        CartelPt2,
        Yucatan,
        YucatanPt2,
        VisLab1,
        VisLab2,
        VisLabNoText,
        VisLab2Pt2,
        Google,
        GooglePt2,
        YucatanPen,
        InsideCave,
        End
    }
    public GameStates state;
    public void Part1()
    {
        state = GameStates.VisualLab;
        allText.Add("Hi There! Nice to meet you. My name is Dominique Rissolo, " +
                    "and I’m an archaeologist with the Qualcomm Institute at the University of California, San Diego.");

        allText.Add("I heard that you’re an aspiring scientist interested in learning " +
                    "more about the ancient history of the Americas and I know the perfect site " +
                    "for you to explore…it’s called Hoyo Negro, or the “black hole.”");

        allText.Add("Let me give you a little background on the site!");

    }

    public void Part2()
    {
        allText.Clear();
        messageText.text = ""; 
        state = GameStates.NewIceAge;   index = 0; audioIndex++;

        allText.Add("During the last Ice Age – over 12,000 years ago – " +
                    "huge ice sheets covered much of the northern portion of our planet.");
        allText.Add("As a result, sea levels worldwide were up to 300 feet lower than they are today.");

        allText.Add("The first peoples to migrate into the Americas from northeast Asia would have encountered " +
                    "massive glaciers on land, but they were able to access coastal areas – including those containing caves – " +
                    "that are now underwater…after a warming planet caused the ice sheets to melt.");
        StartCoroutine(autoType.ShowText(allText[index], textSpeed));
        audio.PlayClip(audioIndex);

    }
    public void Part3()
    {
        allText.Clear();
        messageText.text = "";
        state = GameStates.Cartel; index = 0; audioIndex++;

        allText.Add("On the Yucatán Peninsula of Mexico, surface water would have been scarce during " +
                    "the end of the last Ice Age, and animals and humans would have ventured underground in search of water to drink.");
        
        allText.Add("Wandering the dark passageways of a cave, several animals – " +
                    "including now extinct species of megafauna, like gomphotheres, giant ground sloths, " +
                    "short-faced bears, and sabertoothed cats – fell off the edge of a subterranean cliff into 100-foot-deep pit we call “Hoyo Negro.”");

        StartCoroutine(autoType.ShowText(allText[index], textSpeed));
        audio.PlayClip(audioIndex);

    }
    public void Part4()
    {
        allText.Clear();
        messageText.text = "";
        state = GameStates.YucatanPt2; index = 0; audioIndex++;
        textbox.SetActive(true);
        nextImageButton.SetActive(false);
        allText.Add("Today, all of Hoyo Negro is now underwater. And the bone of these amazing animals and " +
            "the skeleton of a young woman – and descendent of first peoples to enter the New World – " +
            "can be found in the dark reaches of this flooded cave.");

        StartCoroutine(autoType.ShowText(allText[index], textSpeed));
        audio.PlayClip(audioIndex);

    }
    public void Part5()
    {
        allText.Clear();
        messageText.text = "";
        state = GameStates.VisLab1; index = 0;
        allText.Add("At the Qualcomm Institute, scientists can take virtual 3D 'dives' into " +
            "Hoyo Negro in immersive visualization systems like the SunCAVE.");

        StartCoroutine(autoType.ShowText(allText[index], textSpeed));
    }
    public void Part6()
    {
        allText.Clear();
        messageText.text = "";
        state = GameStates.VisLab2; index = 0;
        allText.Add("Hoyo Negro Project scientists and engineers can also explore 3D data together on the WAVE at Qualcomm Institute.");

        StartCoroutine(autoType.ShowText(allText[index], textSpeed));
    }
    public void Part7()
    {
        allText.Clear();
        messageText.text = "";
        state = GameStates.VisLab2Pt2; index = 0; audioIndex++;
        textbox.SetActive(true);
        nextImageButton.SetActive(false);
        allText.Add("As an underwater cave explorer, what kinds of fossils can you find by searching the cave?");
        allText.Add("Are you ready for this exciting journey? I’ll meet you there, along with all the tools that we’ll need!");

        StartCoroutine(autoType.ShowText(allText[index], textSpeed));
        audio.PlayClip(audioIndex);

    }
    public void Part8()
    {
        allText.Clear();
        messageText.text = "";
        state = GameStates.YucatanPen; index = 0;
        textbox.SetActive(true);
        nextImageButton.SetActive(false);
        allText.Add("Yucatán Peninsula, Mexico");

        StartCoroutine(autoType.ShowText(allText[index], textSpeed));
    }
    public void Part9()
    {
        allText.Clear();
        messageText.text = "";
        state = GameStates.InsideCave; index = 0; audioIndex++;
        allText.Add("We’ve finally arrived at the site of Hoyo Negro located on the Yucatán Peninsula in Mexico. " +
                    "Let’s get ready to dive into the cave and discover Hoyo Negro!");

        StartCoroutine(autoType.ShowText(allText[index], textSpeed));
        audio.PlayClip(audioIndex);

    }

    public void NextText()
    {
        index++;
        if (index < allText.Count)
        {
            audioIndex++;

            messageText.text = "";
            messageText.fontSize = FontSize;
            messageText.alignment = TextAlignmentOptions.Left;
            messageText.alignment = TextAlignmentOptions.MidlineLeft;
            StartCoroutine(autoType.ShowText(allText[index], textSpeed));
            audio.PlayClip(audioIndex);

        }
        else if (index == allText.Count) //when on the last message, turn off panel and trigger event
        {
            switch (state)
            {
                case GameStates.VisualLab:
                    fade.OnClick();
                    Part2();
                    break;

                case GameStates.NewIceAge:
                    fade.OnClick();
                    Part3();
                    break;

                case GameStates.Cartel:
                    textbox.SetActive(false);
                    nextImageButton.SetActive(true);
                    break;

                case GameStates.YucatanPt2:
                    fade.OnClick();
                    Part5();
                    break;

                case GameStates.VisLab1:
                    fade.OnClick();
                    Part6();
                    break;

                case GameStates.VisLab2:
                    textbox.SetActive(false);
                    nextImageButton.SetActive(true);
                    state = GameStates.VisLabNoText;
                    break;

                case GameStates.VisLab2Pt2:
                    fade.OnClick();
                    textbox.SetActive(false);
                    nextImageButton.SetActive(true);
                    state = GameStates.Google;
                    break;

                case GameStates.YucatanPen:
                    fade.OnClick();
                    Part9();
                    break;

                case GameStates.InsideCave:
                    textbox.SetActive(false);
                    nextImageButton.SetActive(true);
                    state = GameStates.End;
                    break;

                default:
                    break;
            }
        }
    }
}
