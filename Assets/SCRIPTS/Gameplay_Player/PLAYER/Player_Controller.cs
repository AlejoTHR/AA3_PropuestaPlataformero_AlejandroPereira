using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent (typeof(Collider2D))]
[RequireComponent (typeof(PlayerStats))]
[RequireComponent (typeof(Animator))]
public class Player_Controller : MonoBehaviour
{
    [Header("STATS")]
    [SerializeField] private PlayerStats _PlyrStts;

    [Header("Components")]
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] Collider2D _clldr;
    [SerializeField] Animator _animator;

    [Header("Horizontal Movement")]
    Vector2 HorizontalMove;
    [SerializeField] public bool LookingRight = true;

    [Header("Jumping")]
    [SerializeField] short JumpCount = 0;
    short JumpCountMax = 1;

    [Header("Grounded")]
    [SerializeField] bool IsGrounded;
    [SerializeField] Transform GroundedChecPosition;

    [Header("Repawn")]
    [SerializeField] Vector3 Checkpoint_Position;


    [Header("Attk Points")]
    public Transform sideAttkPoint;
    public Transform downAttkPoint;
    public float radiusSide;
    public float radiusDown;
    public LayerMask enemies;

    /*
    [Header("Arrow Prefab")]
    public GameObject arrow;
    */

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
        Checkpoint_Position = transform.position;
    }


    private void Update()
    {
        if (_rb == null || _clldr == null || _animator == null) Debug.LogWarning("Faltan Componentes");
        JumpDetection();

        // RB VELOCITY FOR HORIZONTAL MOVEMENT
        _rb.linearVelocity = new Vector3(HorizontalMove.x * _PlyrStts.Speed, _rb.linearVelocity.y);

        // ANIMATOR PArameTER SETS
        _animator.SetBool("Jumped", !IsGrounded);

    }

    #region HORIZONTAL MOVEMENT
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed) // IF KEYBINDS PRESSED TO MOVE
        {   // MOVEMENT CHANGES

            _animator.SetBool("Running", true);

            HorizontalMove = context.ReadValue<Vector2>();
            Vector3 TurningAround = new Vector3(0, 180, 0);
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
            _animator.SetBool("Running", false);

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
        else if(hit.collider.CompareTag("Ground"))
        {
            IsGrounded = true; // IS TOUCHING THE GROUND (SHOULD BE)
            JumpCount = 0; // RESET JUMP COUNT

            Debug.DrawRay(transform.position, Vector2.down * _PlyrStts.GroundedCheckSize, Color.green); // CICLE CAST COLOR GREEN
            _animator.SetBool("AttackedDown", false);
        }
        else if(hit.collider.CompareTag("Spring") && !IsGrounded)
        {
            _rb.linearVelocity = new Vector3 (_rb.linearVelocity.x, _PlyrStts.Bounce); // JUMPS FROM ENEMY
            JumpCount = 0; // RESET JUMP COUNT
            _animator.SetBool("AttackedDown", false);
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

        if (context.performed && IsGrounded)
        {
            Collider2D[] enemyhit = Physics2D.OverlapCircleAll(sideAttkPoint.position, radiusSide, enemies);

            if (enemyhit != null)
            {
                Debug.Log("ENEMY HIT");
            }

            _animator.SetBool("Attacked", true);

        }
        if (context.performed && !IsGrounded && (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)))
        {
            _animator.SetBool("AttackedDown", true);

        }
    }
    void PogoStart()
    {
        Collider2D enemyhit = Physics2D.OverlapCircle(downAttkPoint.position, radiusSide, enemies);
        if (enemyhit != null)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0);
            _rb.AddForce(Vector2.up * _PlyrStts.JumpForce);
        }
    }
    public void FinishAttkAnim_Sideattk() 
    {
        _animator.SetBool("Attacked", false); 
    }
    public void FinishAttkAnim_Downattk() 
    {
        _animator.SetBool("AttackedDown", false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(sideAttkPoint.position, radiusSide);
        Gizmos.DrawWireSphere(downAttkPoint.position, radiusDown);
    }
    #endregion

    #region ON TRIGGER ENTER'S
    private void OnTriggerEnter2D(Collider2D collided)
    {
        if (collided == null) return;

        #region DEATH ZONE n CHECKPOINTS
        if (collided.gameObject.CompareTag("Death")) // DEATH ZONE COLLSIION
        {
            transform.position = Checkpoint_Position;
        }
        if(collided.gameObject.CompareTag("Checkpoint")) // CHECK POINT COLLISIOn
        {
            Checkpoint_Position = collided.gameObject.GetComponent<Transform>().position; ;
        }
        #endregion

        #region KEYS
        if (collided.gameObject.CompareTag("Key")) // KEY COLLSIION
        {
            _PlyrStts.KeyCount++;
            Destroy(collided.gameObject);
        }

        #endregion

        #region CHESTS
        if (collided.gameObject.CompareTag("Chest") && _PlyrStts.KeyCount > 0) // KEY COLLSIION
        {
            collided.GetComponent<Animator>().SetBool("IsOpen", true);
            _PlyrStts.KeyCount--;
        }

        #endregion

    }
    #endregion

    #region Pause
    void _PAUSE()
    {
        Time.timeScale = 0f;

    }

    #endregion
}
