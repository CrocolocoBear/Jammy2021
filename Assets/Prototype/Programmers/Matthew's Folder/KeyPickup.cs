﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public GameObject[] keys;

    int keysCollected = 0;

    void Awake()
    {
        keys = GameObject.FindGameObjectsWithTag("KeyBack");

        for (int i = 0; i < keys.Length; i++)
        {
            keys[i].SetActive(false);
        }
    }

    public void Pickup(string name)
    {
        for (int i = 0; i < keys.Length; i++)
        {
            if (keys[i].name == name)
            {
                keys[i].SetActive(true);
            }
        }

        keysCollected++;
        Debug.Log(keysCollected);
    }
}