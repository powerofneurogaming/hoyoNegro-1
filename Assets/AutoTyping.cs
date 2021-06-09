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
    private string currentText = "";
   
    public IEnumerator ShowText(string text, float delay)
    {
        fullText = text;
        for (int i = 0; i <= text.Length; i++)
        {
            //if (lastHitKey == KeyCode.Space)
            //{
            //    lastHitKey = KeyCode.None;
            //    uiText.text = fullText;
            //    yield break;
            //}
            //else
            //{
            //    currentText = text.Substring(0, i);
            //    uiText.text = currentText;
            //    yield return new WaitForSeconds(delay);
            //}
            currentText = text.Substring(0, i);
            uiText.text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }
}
