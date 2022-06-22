using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ButtonMashing : MonoBehaviour
{
    public Timer timer;
    public Animator normAnim;
    public Animator ghostAnim;
    public Material enlightened, nolight;
    public GameObject djCarpet, guitarCarpet, keyboardCarpet, drumsCarpet;
    [Space]
    [Header("Keys")]

    //Keys
    public KeyCode[] keys;
    public List<string> keysToPress;
    public TextMeshProUGUI[] keyText;
    public int currentType;


    public int keyToPress;
    public int currentKeyToPress;
    private bool gameisOver;

    //Sprites
    public GameObject[] instruments;
    public GameObject[] backKeys;
    public List<Sprite> instrumentSprite;
    public List<int> instrumentsToPress;

    public int instrumentToUse; // 0 Guitar 1 Keyboard 2 Drums
    public int currentInstrumentToPress;

    [Space]
    [Header("Canvas")]
    public int score;
    public TextMeshProUGUI scoreText;


    [Space]
    [Header("Trigger")]
    public int triggerNumber;

    [Space]
    [Header("Audio")]
    public AudioSource keyOk;
    // Start is called before the first frame update
    void Start()
    {
        timer = FindObjectOfType<Timer>();
        StartGame();
        gameisOver = false;
        AudioManager.instance.PlayMusic("DiscoMusic");
    }

    public void StartGame()
    {
        triggerNumber = 4;
        if (GameManager.isGhost)
        {
            foreach (TextMeshProUGUI key in keyText)
            {
                key.gameObject.SetActive(false);
            }
            foreach(GameObject backkey in backKeys)
            {
                backkey.SetActive(false);
            }
            foreach (GameObject instrument in instruments)
            {
                instrumentToUse = Random.Range(0, 3);
                instrument.GetComponent<Image>().sprite = instrumentSprite[instrumentToUse];
                instrumentsToPress.Add(instrumentToUse);
            }
            drumsCarpet.GetComponent<MeshRenderer>().material = enlightened;
            guitarCarpet.GetComponent<MeshRenderer>().material = enlightened;
            keyboardCarpet.GetComponent<MeshRenderer>().material = enlightened;
            currentInstrumentToPress = 0;
        }
        else
        {
            currentType = 2;
            drumsCarpet.GetComponent<MeshRenderer>().material = enlightened;
            guitarCarpet.GetComponent<MeshRenderer>().material = enlightened;
            keyboardCarpet.GetComponent<MeshRenderer>().material = enlightened;
            djCarpet.GetComponent<MeshRenderer>().material = nolight;
            foreach (TextMeshProUGUI key in keyText)
            {
                key.gameObject.SetActive(false);
            }
            foreach (GameObject backkey in backKeys)
            {
                backkey.SetActive(false);
            }
            instrumentsToPress.Clear();
            foreach (GameObject instrument in instruments)
            {
                instrument.SetActive(true);
                instrumentToUse = Random.Range(0, 3);
                instrument.GetComponent<Image>().sprite = instrumentSprite[instrumentToUse];
                instrumentsToPress.Add(instrumentToUse);
            }
            currentInstrumentToPress = 0;
        }

        StartCoroutine("TimerOn");
    }

    public void RestartGame(bool correct)
    {
        triggerNumber = 4;
        if (GameManager.isGhost)
        {
            foreach (TextMeshProUGUI key in keyText)
            {
                key.gameObject.SetActive(false);
            }
            foreach (GameObject backkey in backKeys)
            {
                backkey.SetActive(false);
            }
            instrumentsToPress.Clear();
            foreach (GameObject instrument in instruments)
            {
                instrument.SetActive(true);
                instrumentToUse = Random.Range(0, 3);
                instrument.GetComponent<Image>().sprite = instrumentSprite[instrumentToUse];
                instrumentsToPress.Add(instrumentToUse);
            }
            currentInstrumentToPress = 0;
        }
        else
        {
            if (correct)
            {
                if (currentType == 1)
                {
                    currentType++;
                }
                else if (currentType == 2)
                {
                    currentType--;
                }
            }
            if (currentType == 1)
            {
                drumsCarpet.GetComponent<MeshRenderer>().material = nolight;
                guitarCarpet.GetComponent<MeshRenderer>().material = nolight;
                keyboardCarpet.GetComponent<MeshRenderer>().material = nolight;
                djCarpet.GetComponent<MeshRenderer>().material = enlightened;
                foreach (GameObject instrument in instruments)
                {
                    instrument.SetActive(false);
                }
                foreach (GameObject backkey in backKeys)
                {
                    backkey.SetActive(true);
                }
                keysToPress.Clear();
                foreach (TextMeshProUGUI text in keyText)
                {
                    text.gameObject.SetActive(true);
                    keyToPress = Random.Range(0, 8);
                    string key = keys[keyToPress].ToString();
                    text.text = key;
                    keysToPress.Add(key);
                }
                currentKeyToPress = 0;
            }
            else if (currentType == 2)
            {
                drumsCarpet.GetComponent<MeshRenderer>().material = enlightened;
                guitarCarpet.GetComponent<MeshRenderer>().material = enlightened;
                keyboardCarpet.GetComponent<MeshRenderer>().material = enlightened;
                djCarpet.GetComponent<MeshRenderer>().material = nolight;
                foreach (TextMeshProUGUI key in keyText)
                {
                    key.gameObject.SetActive(false);
                }
                instrumentsToPress.Clear();
                foreach (GameObject instrument in instruments)
                {
                    instrument.SetActive(true);
                    instrumentToUse = Random.Range(0, 3);
                    instrument.GetComponent<Image>().sprite = instrumentSprite[instrumentToUse];
                    instrumentsToPress.Add(instrumentToUse);
                }
                currentInstrumentToPress = 0;
            }
           
        }
    }



    IEnumerator TimerOn()
    {
        yield return new WaitForSeconds(1);
        FindObjectOfType<Timer>().StartTimer();

    }
    // Update is called once per frame
    void Update()
    {
        if (gameisOver == false)
        {
            if (GameManager.isGhost)
            {

                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (triggerNumber == 4)
                    {
                        return;
                    }
                    if (instrumentsToPress[currentInstrumentToPress] == triggerNumber)
                    {
                        instruments[currentInstrumentToPress].gameObject.SetActive(false);
                        normAnim.SetTrigger("Interact");
                        ghostAnim.SetTrigger("Interact");
                        switch (triggerNumber)
                        {
                            case 0:
                                AudioManager.instance.PlaySFX("Guitar");
                                break;
                            case 1:
                                AudioManager.instance.PlaySFX("Keyboard");
                                break;
                            case 2:
                                AudioManager.instance.PlaySFX("Drums");
                                break;
                            default:
                                break;
                        }
                        if (currentInstrumentToPress <= 2)
                        {
                            currentInstrumentToPress++;
                        }
                        else
                        {
                            score = score + 4;
                            scoreText.text = score.ToString();
                            AudioManager.instance.PlaySFX("GameCompleted");
                            //Sonido de correcto
                            RestartGame(true);
                        }
                    }
                    else
                    {
                        RestartGame(false);
                    }
                }
            }

            else
            {
                if (currentType == 1)
                {
                    if (triggerNumber == 3)
                    {

                        if (Input.anyKeyDown)
                        {
                            if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1) || Input.GetKey(KeyCode.W) ||
                                Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
                            {
                                return;
                            }
                            else if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), keysToPress[currentKeyToPress])))
                            {
                                normAnim.SetTrigger("Interact");
                                ghostAnim.SetTrigger("Interact");

                                AudioManager.instance.PlaySFX("DJ", keyOk);
                                keyText[currentKeyToPress].gameObject.SetActive(false);
                                if (currentKeyToPress <= 2)
                                {
                                    currentKeyToPress++;
                                }
                                else
                                {
                                    score = score + 4;
                                    scoreText.text = score.ToString();
                                    //Sonido de correcto
                                    RestartGame(true);
                                }
                            }
                            else
                            {
                                RestartGame(false);
                            }
                        }
                    }
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if (triggerNumber == 4)
                        {
                            return;
                        }
                        if (instrumentsToPress[currentInstrumentToPress] == triggerNumber)
                        {
                            instruments[currentInstrumentToPress].gameObject.SetActive(false);
                            normAnim.SetTrigger("Interact");
                            ghostAnim.SetTrigger("Interact");
                            switch (triggerNumber)
                            {
                                case 0:
                                    AudioManager.instance.PlaySFX("Guitar", keyOk);
                                    break;
                                case 1:
                                    AudioManager.instance.PlaySFX("Keyboard", keyOk);
                                    break;
                                case 2:
                                    AudioManager.instance.PlaySFX("Drums", keyOk);
                                    break;
                                default:
                                    break;
                            }
                            if (currentInstrumentToPress <= 2)
                            {
                                currentInstrumentToPress++;
                            }
                            else
                            {
                                score = score + 4;
                                scoreText.text = score.ToString();
                                AudioManager.instance.PlaySFX("GameCompleted");
                                //Sonido de correcto
                                RestartGame(true);
                            }
                        }
                        else
                        {
                            RestartGame(false);
                        }
                    }
                }
                
            }
            if (timer.timer <= 0)
            {
                timer.uiTimer.text = "00:000";
                timer.StopTimer();
                AudioManager.instance.PlaySFX("GameCompleted");
                if (!gameisOver)
                {
                    StartCoroutine(EndGame());
                }
                gameisOver = true;
            }
        }
        
    }
    IEnumerator EndGame()
    {
        GameManager.DJDone = true;
        AudioManager.instance.VolumeDown();
        GameManager.gamesCompleted++;
        yield return new WaitForSeconds(3);
        LevelLoader.instance.PlayPress(1);
        yield return null;
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Instrument")
        {
            triggerNumber = WhatTrigger(other.gameObject);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Instrument")
        {
            triggerNumber = 4;
        }
    }

    public int WhatTrigger(GameObject other)
    {
        if (other.gameObject.name == "DrumsTrigger")
        {
            return 2;
        }
        else if(other.gameObject.name == "DJTrigger")
        {
            return 3;
        }
        else if (other.gameObject.name == "GuitarTrigger")
        {
            return 0;
        }
        else if (other.gameObject.name == "KeyboardTrigger")
        {
            return 1;
        }
        else
        {
            return default;
        }
    }
}
