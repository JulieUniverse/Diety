using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class TouchControls : MonoBehaviour
{

    public CharacterController2D controller;
    float horizontalMove = 0f;
    public float runSpeed = 40f;
    bool jump = false;

    void Update()
    {

        horizontalMove = CrossPlatformInputManager.GetAxisRaw("Horizontal") * runSpeed;
        

    }

    public void Jump()
    {
        jump = true;
    }
    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}