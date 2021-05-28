using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AutoTyping : MonoBehaviour
{
    [SerializeField]
    private TMP_Text uiText;
    string fullText;
    private string textToWrite;
    private string currentText = "";
    private int charIndex;
    private float timePerChar;
    private float timer;
    private KeyCode lastHitKey;

    //public void AddWriter(TMP_Text uiText, string textToWrite, float timePerChar)
    //{
    //    this.uiText = uiText;
    //    this.textToWrite = textToWrite;
    //    this.timePerChar = timePerChar;
    //    charIndex = 0;
    //}
    public IEnumerator ShowText(string text, float delay)
    {
        fullText = text;
        for (int i = 0; i <= text.Length; i++)
        {
            if (lastHitKey == KeyCode.Space)
            {
                lastHitKey = KeyCode.None;
                uiText.text = fullText;
                yield break;
            }
            else
            {
                currentText = text.Substring(0, i);
                uiText.text = currentText;
                yield return new WaitForSeconds(delay);
            }
            //currentText = text.Substring(0, i);
            //uiText.text = currentText;
            //yield return new WaitForSeconds(delay);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            lastHitKey = KeyCode.Space;

        }
        //if (uiText != null)
        //{
        //    timer -= Time.deltaTime;
        //    while (timer <= 0f)
        //    {
        //        timer += timePerChar;
        //        charIndex++;
        //        uiText.text = textToWrite.Substring(0, charIndex);

        //        if (charIndex >= textToWrite.Length)
        //        {
        //            uiText = null;
        //            return;
        //        }
        //    }
        //}
    }

}
