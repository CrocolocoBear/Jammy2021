﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickRing : MonoBehaviour
{
    Vector3 pointOfCollision;

    public Vector3 GetPointOfCollision()
    {
        return pointOfCollision;
    }

    public void SetPointOfCollision(Vector3 empty)
    {
        pointOfCollision = empty;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        int i = 1, key = 0, camCheck = 0;

        if (GetComponentInParent<KeyPickup>().GetKeys() >= 1)
        {
            pointOfCollision = collision.GetContact(0).point;

            switch (collision.gameObject.tag)
            {
                case "wall":
                    key = 1;
                    camCheck = 1;
                    i = 0;
                    GetComponentInParent<PlayerControlRingVersion>().GetRBRing().constraints = RigidbodyConstraints.FreezeAll;
                    break;
                case "Untagged":
                    key = 10;
                    camCheck = 0;
                    i = 1;
                    GetComponentInParent<PlayerControlRingVersion>().GetRBRing().constraints = RigidbodyConstraints.None;
                    break;
            }

            GetComponentInParent<PlayerControlRingVersion>().SetStickVars(i, GetComponentInParent<PlayerControlRingVersion>().transform.eulerAngles.y * camCheck, key);
        }
        else
        {
            GetComponentInParent<PlayerControlRingVersion>().GetRBRing().constraints = RigidbodyConstraints.None;
        }
    }
}