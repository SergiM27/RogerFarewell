using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI text;
    public string[] sentences;
    public int index;
    public float typingSpeedInGame;
    public bool gameStarted, ableToSkip;
    public bool dialogueOn;

    private GameObject objectText;


    // Start is called before the first frame update
    void Start()
    {
        ableToSkip = false;
        objectText = GameObject.Find("ObjectText");
        gameStarted = false;
        text.text = "";
        Invoke("StartDialogue", 3.5f);
    }

    public void StartDialogue()
    {
        StartCoroutine(Type());
        ableToSkip = true;
    }
    IEnumerator Type()
    {
        text.text = "";
            if (gameStarted == false)
            {
                foreach (char letter in sentences[index].ToCharArray())
                {
                    text.text += letter;
                    yield return new WaitForSeconds(typingSpeedInGame);
                }

                if (index >= sentences.Length - 1)
                {
                    yield return new WaitForSeconds(7.0f);
                    //Que empieze la partida
                    gameStarted = true;
                    NextSentence();
                }
                else
                {
                yield return new WaitForSeconds(5.0f);
                text.text = "";
                yield return new WaitForSeconds(2.0f);
                    NextSentence();
                }
            }

    }
    public void NextSentence()
    {
        if(index < sentences.Length - 1)
        {
            index++;
            text.text = "";
            StartCoroutine(Type());
        }
        else
        {
            index = 0;
            text.text = "";
            StartCoroutine(Type());
        }
    }

}
