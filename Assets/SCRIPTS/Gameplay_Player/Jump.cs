using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.InputSystem;

public class Jump : MonoBehaviour
{
    [Header("Objects")]
    public Rigidbody2D _rb;
    public Collider2D _clldr;
    public Animator _animator;

    [Header("Jumping")]
    public float JumpForce;
    public float JumpCancelSupresion = 0.5f;
    public short JumpCount = 0;

    [Header("Grounded")]
    public bool IsGrounded;
    public float GroundedCheckSize;
    public float GroundedCheckRadius;
    public LayerMask mask;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _clldr = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        Debug.DrawRay(transform.position, Vector2.down * GroundedCheckSize, Color.red);

        RaycastHit2D hit = Physics2D.CircleCast(transform.position, GroundedCheckRadius, Vector2.down, GroundedCheckSize, mask);
        

        if (hit.collider == null)
        {
            IsGrounded = false;
        }
        else
        {
            IsGrounded = true;
            JumpCount = 0;

            Debug.DrawRay(transform.position, Vector2.down * GroundedCheckSize, Color.green);
        }




    }

    // JUMP
    public void OnJump(InputAction.CallbackContext context)
    {

        if(JumpCount < 1) 
            {
            if (context.performed) // JUMP HOLDED; FULL FORCE
            {
                _rb.AddForce(Vector2.up * JumpForce);
                JumpCount++;

            }
            else if (context.canceled && _rb.linearVelocity.y > 0) // JUMP RELEASED MID AIR, LESS FORCE
            {
                _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _rb.linearVelocity.y * JumpCancelSupresion);
            }
        }

    }

    private void OnDrawGizmosSelected()
    {

        Gizmos.color = IsGrounded? Color.green: Color.red;

        Gizmos.DrawWireSphere(transform.position + Vector3.down * GroundedCheckSize, GroundedCheckRadius);
    }


}
