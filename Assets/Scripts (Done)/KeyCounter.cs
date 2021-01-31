using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyCounter : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject gameOverScreen;
    public int noOfKeys = 0;
    TextMeshProUGUI mtext;
    bool gameOverStarted = false;

    private void Start()
    {
        mtext = this.GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        UpdateKeys();
        UpdateKeyCounter();
        if (noOfKeys == 8 && gameOverStarted == false)
        {
            gameOverStarted = true;
            StartCoroutine(GameOverDelay());
            FinishGame();
        }
    }

    private void UpdateKeys()
    {
        if (player != null)
        {
            noOfKeys = player.GetComponent<KeyPickup>().GetKeys();
        }
    }

    private void UpdateKeyCounter()
    {
        mtext.text = noOfKeys.ToString();
    }

    private void FinishGame()
    {
        gameOverScreen.GetComponent<GameOverScreen>().GameOver = true;
    }

    IEnumerator GameOverDelay()
    {
        yield return new WaitForSeconds(15);
    }
}
