using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;

    public float moveSpeed = 40f;
    
    private float _horizontalMove = 0f;
    private bool _jump = false;
    
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int IsJump = Animator.StringToHash("IsJump");
    private static readonly int IsLanding = Animator.StringToHash("IsLanding");

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.GameIsPause)
        {
            _horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;
            
            animator.SetFloat(Speed, Mathf.Abs(_horizontalMove));
        
            if (Input.GetButtonDown("Jump"))
            {
                _jump = true;
                animator.SetBool(IsJump, true);
            }
        }
    }

    private void FixedUpdate()
    {
        controller.Move(_horizontalMove * Time.fixedDeltaTime, false, _jump);
        _jump = false;
    }

    public void OnLanding()
    {
        animator.SetBool(IsJump, false);
    }
}
