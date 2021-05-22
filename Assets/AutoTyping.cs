using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AutoTyping : MonoBehaviour
{

    private TMP_Text messageText;

    private void Awake()
    {
        messageText = transform.Find("Text (TMP)").GetComponent<TMP_Text>();
    }

    private void Start()
    {
        messageText.text = "Hello World";
    }
}
