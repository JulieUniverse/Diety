using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;							// Amount of force added when the player jumps.
	[Range(0, 1)] [SerializeField] private float m_RollSpeed = .36f;			// Amount of maxSpeed applied to rolling movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;							// A position marking where to check for ceilings
	[SerializeField] private BoxCollider2D m_RollDisableCollider;               // A collider that will be disabled when rolling
    [SerializeField] public float MoveSpeed;                                    // Forward movement of the character.
    private Animator m_Animator;
    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;
    public int currentPoops = 3;
    private int maxPoop = 3;
    public Transform butt;
    public GameObject poopPrefab;
    public AudioClip fart;
    public AudioClip noPoop;
    private AudioSource source;
    [Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnRollEvent;
	private bool m_wasRolling = false;

	private void Awake()
	{
        source = GetComponent<AudioSource>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnRollEvent == null)
			OnRollEvent = new BoolEvent();
	}


    private void FixedUpdate()
	{
        bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
	}


    public void Pooping()
    {
        if (currentPoops <= maxPoop && currentPoops != 0)
        {
            source.PlayOneShot(fart);
            Instantiate<GameObject>(poopPrefab, butt.position, butt.rotation);
            currentPoops = currentPoops - 1;
            Debug.Log(currentPoops);
        }
        else if (currentPoops <= 0)
        {
            source.PlayOneShot(noPoop);
            Debug.Log("You runnin' on empty pal!");
            Debug.Log(currentPoops);
        }
    }

    public void Move(float move, bool roll, bool jump)
    {
        // If rolling, check to see if the character can stand up
        if (!roll)
        {
            // If the character has a ceiling preventing them from standing up, keep them rolling
            if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
            {
                roll = true;
            }
        }

        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {

            // If rolling
            if (roll)
            {
                if (!m_wasRolling)
                {
                    m_wasRolling = true;
                    OnRollEvent.Invoke(true);
                }

                // Reduce the speed by the rollSpeed multiplier
                move *= m_RollSpeed;

                // Disable one of the colliders when crouching
                if (m_RollDisableCollider != null)
                    m_RollDisableCollider.enabled = false;
            } else
            {
                // Enable the collider when not crouching
                if (m_RollDisableCollider != null)
                    m_RollDisableCollider.enabled = true;

                if (m_wasRolling)
                {
                    m_wasRolling = false;
                    OnRollEvent.Invoke(false);
                }
            }

            

        // Move the character by finding the target velocity
        Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
		}

            // If the player should jump...
            if (m_Grounded && jump)
            {
                // Add a vertical force to the player.
                m_Grounded = false;
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            }
        }


        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
