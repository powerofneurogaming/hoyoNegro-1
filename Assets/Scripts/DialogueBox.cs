using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : Singleton<DialogueBox>
{
    string[] Intro = new string[]
    {
        "Greetings Explorer. I’m Probe 07 and I’m here to help you on your mission! ",
        "A recent information breach has caused the laboratory to lose a ton of data. Thankfully, our technology has enabled us to fully scan and record sites of interest to recreate them in a virtual space! ",
        "We are currently in the Hoyo Negro, located in an underwater cave of the Yucatán Penninsula.",
        "This archeological site is of significant interest because it’s where Naia, one of the oldest and most complete skeletons in the Americas was found! Several other bones of extinct species also reside here. ",
        "You guessed it, we’re on a reconnaissance mission to recollect data about these bones that were lost in the data breach. Thankfully, this will be an easy task with my help.",
        "Let’s dive down and pick up some bone parts. Every time a bone part is collected, information on the species of the bone will be unlocked in the journal."
    };
    string[] Difficulty1 = new string[]
    {
        "Do you see these orbs? Those are bone data fragments! I’ve helped you uncover a few before you arrived. Hurry and collect them by pressing the right click button! If you can’t pick them up, try moving closer."
    };
    string[] Difficulty2 = new string[]
    {
        "It seems like we still have quite a few to collect. Fortunately you brought a handy radar down here! ",
        "Press R to run the sonar. You will hear a number of beeps based on how close you are, up to 5",
        "If you happen to be close enough to one (5 beeps), the orb will reveal itself so that you can collect it. ",
        "I’ll also be around dropping some power ups to help you out! Press E to collect them."
    };
    string[] Difficulty3 = new string[]
    {
        "Phew, we are almost there! Some of the data for the bones are a bit more hidden. It seems like we will have to use the Triangulation Scanner!",
        "Once you have properly booted it up, you’ll see that areas with green triangles seem to light up. Swim towards that direction to find the bone fragments.",
        "You can only activate three at a time, so don’t forget to deactivate one once you have finished using it by holding “F”."
    };
    string[] Ending = new string[]
    {
        "Wow, we have finally recovered our lost data! To view it, go to the menu and click on “Journal”. It was a blast working with you, partner! "
    };
    List<string[]> AllDialogue = new List<string[]>();

    Text DisplayText;
    // Start is called before the first frame update
    void Start()
    {
        DisplayText = transform.Find("Text").GetComponent<Text>();
        AllDialogue.Add(Intro);
        AllDialogue.Add(Difficulty1);
        AllDialogue.Add(Difficulty2);
        AllDialogue.Add(Difficulty3);
        AllDialogue.Add(Ending);
        StartDialogue(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            nextButton();
        }
    }

    public void StartDialogue(int DialogueIndex)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<playerMovement>().enabled = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<ScannerInteraction>().enabled = false;
        StartCoroutine(ScrollText(DialogueIndex,0));
        DIndex = DialogueIndex;
    }

    int CPS = 20;
    int line = 0;

    int DIndex = 0;
    int Dline = 0;
    IEnumerator ScrollText(int Dinex, int line)
    {
        Dline = line;
        DisplayText.text = "";
        for (int i = 0; i < AllDialogue[DIndex][Dline].Length + 1; i++)
        {
            DisplayText.text = AllDialogue[DIndex][Dline].Substring(0, i);
            yield return new WaitForSeconds(1 / CPS);
        }
        yield return null;
    }

    public void nextButton()
    {
        if (DisplayText.text.Length < AllDialogue[DIndex][Dline].Length)//havent finished scrolling
        {
            Debug.Log("hard setting text");
            //hardset text
            StopAllCoroutines();
            DisplayText.text = AllDialogue[DIndex][Dline];
        }
        else
        {
            Debug.Log("text finished");
            if (Dline < AllDialogue[DIndex].Length - 1)//more lines avaliable
            {

                Dline++;
                Debug.Log("current line: " + Dline);
                StartCoroutine(ScrollText(DIndex,Dline));
            }
            else
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<playerMovement>().enabled = true;
                GameObject.FindGameObjectWithTag("Player").GetComponent<ScannerInteraction>().enabled = true;
                gameObject.SetActive(false);
                //ReturnToSelection();
            }
        }
    }
}
