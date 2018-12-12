using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public AudioClip doorLocked;
    public AudioClip doorOpen;
    private AudioSource source;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Inventory.hasKey == true)
            {
                source.PlayOneShot(doorOpen);
                Debug.Log("TO DO: Play door opening animation here!");
            }
            else if (Inventory.hasKey == false)
            {
                source.PlayOneShot(doorLocked);
            }
        }
        
    }

}
