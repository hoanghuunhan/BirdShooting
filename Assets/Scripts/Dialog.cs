using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public Text titleText;
    public Text contentText;

    public void UpdateDialog(string title, string content)
    {
        if (titleText != null)
            titleText.text = title;
        if(contentText != null)
            contentText.text = content;
    }

    public void Show(bool isShow)
    {
        gameObject.SetActive(isShow);

    }
}
