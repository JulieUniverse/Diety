using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public static bool hasKey = false;
    public List<GameObject> keyItems = new List<GameObject>();
    public AudioClip collectSound;
    private AudioSource source;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Key")
        {
            source.PlayOneShot(collectSound);
            keyItems.Add(other.gameObject);
            hasKey = true;
            Debug.Log("Key is in inventory!");
        }
    }

    }
