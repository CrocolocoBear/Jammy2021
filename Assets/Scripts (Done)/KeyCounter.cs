using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyCounter : MonoBehaviour
{
    GameObject player;
    public int noOfKeys = 0;
    TextMeshProUGUI mtext;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        mtext = this.GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        UpdateKeys();
        UpdateKeyCounter();
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
}
