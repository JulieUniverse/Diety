using UnityEngine;

public class MenuSound : MonoBehaviour {
    public AudioClip startMusic;
    AudioSource source;
    StartMenu menu;
	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
        menu = FindObjectOfType<StartMenu>();
        Music();
	}

    void Music()
    {
        source.PlayOneShot(startMusic);

    }

    public void ToggleSound()
    {
        if (source.enabled == false)
        {
            source.enabled = true;
            Music();
            menu.UnmuteOverride();
        } else if (source.enabled == true)
        {
            source.enabled = false;
            menu.MuteOverride();
        }
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown("m"))
        {
            ToggleSound();
        }
	}
}
