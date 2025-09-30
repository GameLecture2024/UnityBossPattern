using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IDamagable
{
    public Controls controls;
    Rigidbody2D rb;

    [Header("Jump")]
    [SerializeField] float jumpPower = 5f;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float groundCheckDistance = 1f;

    private bool IsJump;

    public Action<bool> OnFire;
    private AudioSource audiosource;

    [field:SerializeField] public int CurrentHealth { get; set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audiosource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        controls = new Controls();
        controls.Player.Enable();

        controls.Player.Jump.performed += HandleJump;
        controls.Player.Jump.canceled += HandleJumpCancled;
        controls.Player.Fire.performed += OnFirePerformed;
        controls.Player.Fire.canceled += OnFireCanceld;
    }

    private void OnDisable()
    {
        controls.Player.Jump.performed -= HandleJump;
        controls.Player.Jump.canceled -= HandleJumpCancled;
        controls.Player.Fire.performed -= OnFirePerformed;
        controls.Player.Fire.canceled -= OnFireCanceld;
        controls.Player.Disable();
    }

    private void OnFirePerformed(InputAction.CallbackContext context)
    {
        OnFire?.Invoke(true);
    }

    private void OnFireCanceld(InputAction.CallbackContext context)
    {
        OnFire?.Invoke(false);
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float dir = controls.Player.Move.ReadValue<float>();

        rb.linearVelocity = new Vector2(dir * 5, rb.linearVelocityY);  

        // audiosource 소스가 끝나고 난 후에 실행하도록 만드는 코드
    }

    private void HandleJump(InputAction.CallbackContext context)
    {
        if(IsGround() && !IsJump)
        {
            IsJump = true;
            audiosource.clip = Resources.Load<AudioClip>("Sound/PlayerJump");
            audiosource.Play();
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }      
    }

    private void HandleJumpCancled(InputAction.CallbackContext context)
    {
        IsJump = false;
    }

    private bool IsGround()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundMask); // 3을 GroundCheckDistance
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position, 
            transform.position + (Vector3)(Vector2.down * groundCheckDistance)); // new Vector3( 0, -1 * 3, 0)
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;

        // Die
    }
}
