using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldKey : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<PlayerControlRingVersion>().charRing.activeSelf == true)
            {
                other.gameObject.GetComponent<KeyPickup>().Pickup(gameObject.name);

                GameObject.FindWithTag("Dialogue").GetComponent<TypewriterEffect>().PickText(gameObject.name);
                GameObject.FindWithTag("Dialogue").GetComponent<TypewriterEffect>().StartText();

                Destroy(gameObject);
            }
        }
    }
}
