﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypewriterEffect : MonoBehaviour
{

    Text dialogue;
    string text;
    GameObject background;
    float textDelay = 0.03f;
    float soundLength;

    string[] script = {"“Hey give that back” *Playful Noises* , Ah the old days back when " +
                        "school was easy and losing a diary was the most of her worries. " +
                        "Alexa, queue DePreSsioN.",
                        "“ OK smile everyone” *Flash noise*. Things were tense for a while, " +
                        "before she left for College. She never got the full story, but it wasn't " +
                        "a happy time. She never quite could understand her choices but leaving " +
                        "was something she was proud of. ",
                        "“Look we can keep all your pictures on this now,” *Tapping noises on " +
                        "keyboard*. Someone she always felt close to was her grandma selma. " +
                        "Time spent with her felt like a lifetime and was always her escape from " +
                        "the world. Her laptop wallpaper always had her smiling right back at her " +
                        "whenever she needed her most."};

    AudioSource audioController;

    public AudioClip[] voices;

    void Awake()
    {
        dialogue = GetComponent<Text>();
        background = GameObject.FindWithTag("DBack");
        background.SetActive(false);
        audioController = gameObject.GetComponent<AudioSource>();
    }

    IEnumerator ShowText()
    {
        foreach(char c in text)
        {
            dialogue.text += c;
            yield return new WaitForSeconds(textDelay);
        }

        yield return new WaitForSeconds(soundLength);

        ClearText();
        background.SetActive(false);
    }

    public void PickText(string name)
    {
        switch(name)
        {
            case "KeyOne":
                text = script[0];
                audioController.clip = voices[0];
                soundLength = 5.5f;
                break;
            case "KeyTwo":
                text = script[1];
                audioController.clip = voices[1];
                soundLength = 6.0f;
                break;
            case "KeyThree":
                text = script[2];
                audioController.clip = voices[2];
                soundLength = 1.0f;
                break;
        }
    }

    public void ClearText()
    {
        dialogue.text = " ";
    }

    public void StartText()
    {
        background.SetActive(true);
        StartCoroutine("ShowText");
        audioController.Play();
    }
}