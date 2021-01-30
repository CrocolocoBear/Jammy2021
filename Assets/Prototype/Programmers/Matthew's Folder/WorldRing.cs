using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldRing : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerControllerPickup>().KeyPickup(gameObject.name);

            Destroy(gameObject);
        }
    }
}
