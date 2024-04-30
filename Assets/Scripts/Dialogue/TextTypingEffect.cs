using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTypingEffect : MonoBehaviour
{
    public Text dialogueText;
    public float typingSpeed = 0.05f; // ������ʾ�ٶ�

    private string fullText;

    void Start()
    {
        fullText = dialogueText.text;
        dialogueText.text = ""; // ����ı�
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        for (int i = 0; i < fullText.Length; i++)
        {
            dialogueText.text += fullText[i];
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
