using UnityEngine;

/// <summary>
/// Menangani input gerak: W/S maju-mundur sesuai arah hadap karakter,
/// A/D membelokkan karakter, Shift untuk lari, Space untuk lompat.
/// FR-02, FR-03, FR-04, FR-05, NFR-03
///
/// Animator parameters:
///   Speed      (float) : 0=idle, 0.5=walk, 1=run
///   IsGrounded (bool)  : true saat di tanah
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _walkSpeed = 5f;
    [SerializeField] private float _runSpeed  = 10f;
    [SerializeField] private float _rotationSpeed = 240f;

    [Header("Jump & Gravity")]
    [SerializeField] private float _jumpHeight = 1.5f;
    [SerializeField] private float _gravity    = -20f;

    private CharacterController _characterController;
    private Animator _animator;
    private Vector3 _velocity;
    private bool _isGrounded;

    private static readonly int _speedHash      = Animator.StringToHash("Speed");
    private static readonly int _isGroundedHash = Animator.StringToHash("IsGrounded");
    private static readonly int _jumpHash       = Animator.StringToHash("JumpTrigger");

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        HandleGroundCheck();
        HandleMovement();
        HandleJump();
        ApplyGravity();
    }

    private void HandleGroundCheck()
    {
        _isGrounded = _characterController.isGrounded;
        if (_isGrounded && _velocity.y < 0f)
            _velocity.y = -2f;

        if (_animator != null)
            _animator.SetBool(_isGroundedHash, _isGrounded);
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); // A/D: belok kiri/kanan
        float vertical   = Input.GetAxisRaw("Vertical");   // W/S: maju/mundur

        // Belok sesuai arah hadap karakter (bukan sumbu dunia)
        if (Mathf.Abs(horizontal) >= 0.1f)
        {
            float turnAngle = horizontal * _rotationSpeed * Time.deltaTime;
            transform.Rotate(0f, turnAngle, 0f);
        }

        // Maju/mundur mengikuti arah hadap karakter (FR-03)
        if (Mathf.Abs(vertical) >= 0.1f)
        {
            bool isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
            float speed    = isRunning ? _runSpeed : _walkSpeed;

            Vector3 direction = transform.forward * Mathf.Sign(vertical);
            _characterController.Move(direction * speed * Time.deltaTime);

            float normalizedSpeed = isRunning ? 1f : 0.5f;
            if (_animator != null)
                _animator.SetFloat(_speedHash, normalizedSpeed, 0.1f, Time.deltaTime);
        }
        else
        {
            if (_animator != null)
                _animator.SetFloat(_speedHash, 0f, 0.1f, Time.deltaTime);
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
            if (_animator != null)
                _animator.SetTrigger(_jumpHash);
        }
    }

    private void ApplyGravity()
    {
        _velocity.y += _gravity * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime);
    }

    public void SetInputEnabled(bool enabled)
    {
        this.enabled = enabled;
        if (!enabled)
        {
            _velocity.y = 0f;
            if (_animator != null)
                _animator.SetFloat(_speedHash, 0f);
        }
    }
}
