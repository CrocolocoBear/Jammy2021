using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypewriterEffect : MonoBehaviour
{

    Text dialogue;
    string text;
    GameObject background;
    float textDelay;
    float soundLength;
    float initialDelay;

    bool tutorial = false;

    string[] script = { //Diary Script
                        "“Hey give that back” *Playful Noises* ,Ah the old days back when " +
                        "school was easy and losing a diary was the most of her worries.",
                        //Family Photo Script
                        "“OK, smile everyone” *Flash noise*. Things were tense for a while, " +
                        "before she left for College. She never got the full story, but it wasn't " +
                        "a happy time. She never quite could understand her choices but leaving " +
                        "was something she was proud of. ",
                        //Laptop Script
                        "“Look we can keep all your pictures on this now,” *Tapping noises on keyboard*. " +
                        "Someone she always felt close to was her grandma selma. " +
                        "Time spent with her felt like a lifetime and was always her escape from " +
                        "the world. Her laptop wallpaper always had her smiling right back at her " +
                        "whenever she needed her most.",
                        //Oven Script
                        "*ding of oven timer finishing* ”Mmhh, that smells good mum” “wait til it’s cooler honey, or you’ll burn your mouth” " +
                        "The taste of home baked cookies, it's " +
                        "a sweet smell touching every corner of the house. " +
                        "They never come out quite like mum makes.",
                        //Shoebox Script
                        "“Just remember when something important happens keep it in here.” *Sounds like shoebox being pulled out* " +
                        "Her dad meant a lot to her; she always saw him as her inspiration while growing up. " +
                        "What was kept in the dark was left there for a reason.",
                        //Fan Script
                        "*laughter* “I often sit and wish that I could be a kite up in the sky.”" +
                        "Oh how free it must be. Head in the clouds, but tied down as always. " +
                        "What happens when the little girl lets go?",
                        //Medicine Cabinet Script
                        "*beeping heart monitor slowly dying* *sobbing* " +
                        "It’s sad, when something that's so present goes away. She had a lot of heart, " +
                        "but maybe that wasn't such a good thing.",
                        //Mug Script
                        "*happy birthday song is being sung* The mug almost didn’t make it into the gift " +
                        "pile before she left. It came in the mail with a cheerful note. " +
                        "He probably didn’t even write it.",
                        //error message
                        "Error"};

    string climbTut =   "Click left mouse button to throw your keyring" + "\n" + 
                        "Your keyring can stick to surfaces if you have enough keys" + "\n" +
                        "Click right mouse button to pull yourself to your keyring" + "\n" +
                        "Moving on in life is the key to everything";
    string moveTut = "WASD - Move" + "\n" + "Spacebar - Jump" + "\n" + "Mouse to look around"; 

    AudioSource audioController;

    public AudioClip[] voices;
    public AudioClip[] effects;
    public AudioClip[] currents;

    void Awake()
    {
        dialogue = GetComponent<Text>();
        background = GameObject.FindWithTag("DBack");
        background.SetActive(false);
        audioController = gameObject.GetComponent<AudioSource>();

        StartCoroutine("MoveTutTimer");
    }

    IEnumerator ShowText()
    {
        yield return new WaitForSeconds(initialDelay);

        dialogue.text = text;

        //foreach(char c in text)
        //{
        //    dialogue.text += c;
        //    yield return new WaitForSeconds(textDelay);
        //}

        yield return new WaitForSeconds(soundLength);

        ClearText();
        if(tutorial)
        {
            dialogue.text = text;
            text = climbTut;
            //foreach (char c in text)
            //{
            //    dialogue.text += c;
            //    yield return new WaitForSeconds(textDelay);
            //}

            yield return new WaitForSeconds(2.0f);

            ClearText();
        }
        background.SetActive(false);
    }

    public void PickText(string name)
    {
        switch(name)
        {
            //The Diary
            case "KeyThree":
                text = script[0];
                currents[0] = effects[0];
                currents[1] = effects[1];
                currents[2] = voices[0];
                textDelay = 0.03f;
                initialDelay = 0.5f;
                soundLength = 20.5f;
                tutorial = false;
                break;
            //The family Photo
            case "KeyTwo":
                text = script[1];
                currents[0] = effects[2];
                currents[1] = effects[3];
                currents[2] = voices[1];
                textDelay = 0.05f;
                initialDelay = 0.2f;
                soundLength = 24.5f;
                tutorial = false;
                break;
            //The Laptop
            case "KeyFour":
                text = script[2];
                currents[0] = effects[4];
                currents[1] = effects[5];
                currents[2] = voices[2];
                textDelay = 0.06f;
                initialDelay = 0.5f;
                soundLength = 25.0f;
                tutorial = false;
                break;
            //The Oven
            case "KeySix":
                text = script[3];
                currents[0] = effects[6];
                currents[1] = effects[7];
                currents[2] = voices[3];
                textDelay = 0.03f;
                initialDelay = 0.0f;
                soundLength = 22.0f;
                tutorial = false;
                break;
            //the Shoebox
            case "KeyOne":
                text = script[4];
                currents[0] = effects[8];
                currents[1] = effects[9];
                currents[2] = voices[4];
                textDelay = 0.03f;
                initialDelay = 0.0f;
                soundLength = 20.0f;
                tutorial = true;
                break;
            //The Fan
            case "KeySeven":
                text = script[5];
                currents[0] = effects[10];
                currents[1] = effects[11];
                currents[2] = voices[5];
                textDelay = 0.03f;
                initialDelay = 0.0f;
                soundLength = 22.0f;
                tutorial = false;
                break;
            //The Medicine Cabinet
            case "KeyEight":
                text = script[6];
                currents[0] = effects[12];
                currents[1] = effects[13];
                currents[2] = voices[6];
                textDelay = 0.1f;
                initialDelay = 0.0f;
                soundLength = 23.0f;
                tutorial = false;
                break;
            //The Mug
            case "KeyFive":
                text = script[7];
                currents[0] = effects[14];
                currents[1] = effects[15];
                currents[2] = voices[7];
                textDelay = 0.06f;
                initialDelay = 0.2f;
                soundLength = 24.3f;
                tutorial = false;
                break;
            default:
                text = script[8];
                currents[0] = effects[16];
                currents[1] = effects[17];
                currents[2] = voices[8];
                textDelay = 0.03f;
                initialDelay = 0.0f;
                soundLength = 22.0f;
                tutorial = false;
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
        StartCoroutine("PlayAudio");
    }

    IEnumerator PlayAudio()
    {
        for (int i = 0; i < currents.Length; i++)
        {
            audioController.clip = currents[i];

            audioController.Play();

            while (audioController.isPlaying)
            {
                yield return null;
            }
        }
    }

    public void MoveTut()
    {
        text = moveTut;
        textDelay = 0.03f;
        initialDelay = 0.0f;
        soundLength = 13.0f;
        background.SetActive(true);
        StartCoroutine("ShowText");
    }

    IEnumerator MoveTutTimer()
    {
        yield return new WaitForSeconds(3.0f);
        MoveTut();
    }
}