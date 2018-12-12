using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public CharacterController2D controller;
    float horizontalMove = 0f;
    public float runSpeed = 40f;
    bool jump = false;
    public AudioClip jumpSound;
    private AudioSource source;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }
	void Update () {

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        
        if (Input.GetButtonDown("Fire1"))
        {
            controller.Pooping();
        }

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            source.PlayOneShot(jumpSound);
        }
    }
	
	void FixedUpdate () {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
