using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(PlayerStats))]
[RequireComponent(typeof(Animator))]
public class Ground_Detection : MonoBehaviour
{
    [Header("STATS")]
    [SerializeField] private PlayerStats _PlyrStts;

    [Header("Components")]
    [SerializeField] public Rigidbody2D _rb;
    [SerializeField] public Animator _animator;

    [Header("OTHER SCRIPTS")]
    [SerializeField] Vertical_Movement _VrtclMvmnt;

    [Header("Grounded")]
    [SerializeField] public bool IsGrounded;
    [SerializeField] public Transform GroundedChecPosition;



    private void Start()
    {
        // START COMPONENTS
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        _VrtclMvmnt = GetComponent<Vertical_Movement>();
    }

    private void Update()
    {
        JumpDetection();
        // ANIMATOR PArameTER SETS
        _animator.SetBool("Jumped", !IsGrounded);
    }
    private void JumpDetection()
    {
        // DRAWS RAY CHECK
        Debug.DrawRay(transform.position, Vector2.down * _PlyrStts.GroundedCheckSize, Color.red);
        // CIRCLE CAST HITS ANOTHER COLLIDER
        RaycastHit2D hit = Physics2D.CircleCast(GroundedChecPosition.position, _PlyrStts.GroundedCheckRadius, Vector2.down, _PlyrStts.GroundedCheckSize, _PlyrStts.mask);

        // IF NOTHING COLLIDES
        if (hit.collider == null) IsGrounded = false;

        // IF SOMETHING DOES COLLIEDS
        else if (hit.collider.CompareTag("Ground"))
        {
            IsGrounded = true; // IS TOUCHING THE GROUND (SHOULD BE)
            _VrtclMvmnt.JumpCount = 0; // RESET JUMP COUNT

            Debug.DrawRay(transform.position, Vector2.down * _PlyrStts.GroundedCheckSize, Color.green); // CICLE CAST COLOR GREEN
            _animator.SetBool("AttackedDown", false);
        }
        else if (hit.collider.CompareTag("Spring") && !IsGrounded)
        {
            _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, _PlyrStts.Bounce); // JUMPS FROM ENEMY
            _VrtclMvmnt.JumpCount = 0; // RESET JUMP COUNT
            _animator.SetBool("AttackedDown", false);
        }

    }

    // DRAW GROUND CHECK
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = IsGrounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(GroundedChecPosition.position + Vector3.down * _PlyrStts.GroundedCheckSize, _PlyrStts.GroundedCheckRadius);
    }
}
