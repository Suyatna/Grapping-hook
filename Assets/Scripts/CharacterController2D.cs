using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float jumpForce = 400f;
    [Range(0, 1)] [SerializeField] private float crouchSpeed = 0.36f;
    [Range(0, 0.3f)] [SerializeField] private float movementSmoothing = 0.05f;
    [SerializeField] private bool airControl = false;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform ceilingCheck;
    [SerializeField] private Collider2D crouchDisableCollider2D;

    private const float GroundRadius = 0.2f;
    private const float CeilingRadius = 0.2f;
    private bool _grounded;
    private bool _facingRight = true;
    private Rigidbody2D _rigidBody;
    private Vector3 _velocity = Vector3.zero;

    [Header("Events")] [Space] public UnityEvent onLandEvent;
    
    [System.Serializable] public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent onCrouchEvent;

    private bool _wasCrouching = false;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();

        if (onLandEvent == null)
        {
            onLandEvent = new UnityEvent();
        }

        if (onCrouchEvent == null)
        {
            onCrouchEvent = new BoolEvent();
        }
    }

    private void FixedUpdate()
    {
        bool wasGrounded = _grounded;
        _grounded = false;
        
        // The player is grounded if a circle cast to the ground check position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(groundCheck.position, GroundRadius, whatIsGround);
        for (int i = 0; i < collider2Ds.Length; i++)
        {
            if (collider2Ds[i].gameObject != gameObject)
            {
                _grounded = true;

                if (!wasGrounded && _rigidBody.velocity.y < 0)
                { 
                    onLandEvent.Invoke();   
                }
            }
        }
    }

    public void Move(float move, bool crouch, bool jump)
    {
        // If crouching, check to see if the character can stand up
        if (!crouch)
        {
            if (Physics2D.OverlapCircle(ceilingCheck.position, CeilingRadius, whatIsGround))
            {
                crouch = true;
            }
        }

        // Only control the player if grounded or airControl is turned on
        if (_grounded || airControl)
        {
            if (crouch)
            {
                if (!_wasCrouching)
                {
                    _wasCrouching = true;
                    onCrouchEvent.Invoke(true);
                }
                
                // Reduce the speed by the crouchSpeed multiplier
                move *= crouchSpeed;
                
                // Disable one of the collider when crouching
                if (crouchDisableCollider2D != null)
                {
                    crouchDisableCollider2D.enabled = false;
                }
            }
            else
            {
                // Enable the collider when not crouching
                if (crouchDisableCollider2D != null)
                {
                    crouchDisableCollider2D.enabled = false;
                }

                if (_wasCrouching)
                {
                    _wasCrouching = false;
                    onCrouchEvent.Invoke(false);
                }
            }
            
            // Move the character by finding the target velocity
            var velocity = _rigidBody.velocity;
            Vector3 targetVelocity = new Vector2(move * 10f, velocity.y);
            
            // And then smoothing it out and applying it to the character
            _rigidBody.velocity =
                Vector3.SmoothDamp(velocity, targetVelocity, ref _velocity, movementSmoothing);
            
            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !_facingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && _facingRight)
            {
                // ... flip the player.
                Flip();
            }
        }
        
        // If the player should jump...
        if (_grounded && jump)
        {
            // Add a vertical force to the player.
            _grounded = false;
            _rigidBody.AddForce(new Vector2(0f, jumpForce));
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        _facingRight = !_facingRight;
        
        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
