using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent (typeof(CapsuleCollider2D))]
[RequireComponent (typeof(PlayerStats))]
[RequireComponent (typeof(Animator))]

public class Player_Controller : MonoBehaviour
{
    [SerializeField] private PlayerStats _PlyrStts;

    [Header("Components")]
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] Collider2D _clldr;
    [SerializeField] Animator _animator;

    [Header("Horizontal Movement")]
    Vector2 HorizontalMove;
    Vector3 TurningAround = new Vector3(0,180,0);
    [SerializeField] bool LookingRight = true;

    [Header("Jumping")]
    [SerializeField] short JumpCount = 0;
    short JumpCountMax = 1;

    [Header("Grounded")]
    [SerializeField] bool IsGrounded;
    [SerializeField] Transform GroundedChecPosition;

    [Header("Arrow Prefab")]
    public GameObject arrow;

    [Header("Arrow Instantiate properties")]
    [SerializeField] float shootCooldown;

    private void Awake()
    {   // START COMPONENTS
        _rb = GetComponent<Rigidbody2D>();
        _clldr = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();
        if (_PlyrStts == null)
        {
            Debug.LogWarning("Someone forgot to assign the  Player_Stats to the player!");
            return;
        }
    }


    private void Update()
    {
        if (_rb == null || _clldr == null || _animator == null) Debug.LogWarning("Faltan Componentes");

        shootCooldown += Time.time / 1000; // SHOOT COOLDOWN

        JumpDetection();

        // RB VELOCITY FOR HORIZONTAL MOVEMENT
        _rb.linearVelocity = new Vector3(HorizontalMove.x * _PlyrStts.Speed, _rb.linearVelocity.y);

        // ANIMATOR PArameTER SETS
        _animator.SetFloat("Running", _rb.linearVelocity.magnitude);

    }

    #region HORIZONTAL MOVEMENT
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed) // IF KEYBINDS PRESSED TO MOVE
        {   // MOVEMENT CHANGES
            HorizontalMove = context.ReadValue<Vector2>();

            if (HorizontalMove.x > 0)
            { // IF LAST INPUT IS RIGHT, LOOKS RIGHT AND KEEPS LOOKING RIGTH
                if (!LookingRight) transform.Rotate(TurningAround);
                LookingRight = true;
            }
            else
            { // IF LAST INPUT IS LEFT, KEEPS LOKING LEFT
                if (LookingRight) transform.Rotate(TurningAround);
                LookingRight = false;

            }
        }
        else if (context.canceled)// IF KEYBINDS RELEASED, STANDS STILL
        {
            HorizontalMove = Vector2.zero;
        }

    }
    #endregion

    #region JUMPING
    // JUMP DETECTION
    private void JumpDetection()
    {
        // DRAWS RAY CHECK
        Debug.DrawRay(transform.position, Vector2.down * _PlyrStts.GroundedCheckSize, Color.red);
        // CIRCLE CAST HITS ANOTHER COLLIDER
        RaycastHit2D hit = Physics2D.CircleCast(GroundedChecPosition.position, _PlyrStts.GroundedCheckRadius, Vector2.down, _PlyrStts.GroundedCheckSize, _PlyrStts.mask);
        
        // IF NOTHING COLLIDES
        if (hit.collider == null) IsGrounded = false;

        // IF SOMETHING DOES COLLIEDS
        else
        {
            IsGrounded = true; // IS TOUCHING THE GROUND (SHOULD BE)
            JumpCount = 0; // RESET JUMP COUNT

            Debug.DrawRay(transform.position, Vector2.down * _PlyrStts.GroundedCheckSize, Color.green); // CICLE CAST COLOR GREEN
        }
    }
    // JUMP
    public void OnJump(InputAction.CallbackContext context)
    {
        if (JumpCount < JumpCountMax)  // IF STILL HAS JUMPS
        {
            if (context.performed) // JUMP HOLDED; FULL FORCE
            {
                _rb.AddForce(Vector2.up * _PlyrStts.JumpForce);
                JumpCount++;
            }
            else if (context.canceled && _rb.linearVelocity.y > 0) // JUMP RELEASED MID AIR, LESS FORCE
            {
                _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _rb.linearVelocity.y * _PlyrStts.JumpCancelSupresion);
            }
        }
    }
    // DRAW GROUND CHECK
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = IsGrounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(GroundedChecPosition.position + Vector3.down * _PlyrStts.GroundedCheckSize, _PlyrStts.GroundedCheckRadius);
    }
    #endregion

    #region ATTACK
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed && shootCooldown >= _PlyrStts.shootCooldownMax)
        {
            // CREATES GAME OBJECT ARROW WITH PREFAB AND ORIGIN VECTOR
            GameObject newArrow = Instantiate(arrow, transform.position, transform.rotation);
            newArrow.GetComponent<Rigidbody2D>().AddForce(transform.forward * 10000);
            shootCooldown = 0; // RESETS COOLDOWN
        }

    }
    #endregion

}
