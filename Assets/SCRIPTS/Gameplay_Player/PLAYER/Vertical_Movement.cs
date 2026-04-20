using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(PlayerStats))]
[RequireComponent(typeof(Animator))]

public class Vertical_Movement : MonoBehaviour
{
    [Header("STATS")]
    [SerializeField] private PlayerStats _PlyrStts;

    [Header("Components")]
    [SerializeField] public Rigidbody2D _rb;

    [Header("OTHER SCRIPTS")]
    [SerializeField] public Ground_Detection _GrndDtct;

    [Header("Jumping")]
    [SerializeField] public short JumpCount = 0;
    [SerializeField] public short JumpCountMax = 1;

    private void Start()
    {
        // START COMPONENTS
        _rb = GetComponent<Rigidbody2D>();

        _GrndDtct = GetComponent<Ground_Detection>();
    }
   
    public void OnJump(InputAction.CallbackContext context)
    {
        if (JumpCount < JumpCountMax)  // IF STILL HAS JUMPS
        {
            if (context.performed) // JUMP HOLDED; FULL FORCE
            {
                _rb.linearVelocity = Vector2.zero;
                _rb.AddForce(Vector2.up * _PlyrStts.JumpForce);
                JumpCount++;
            }
            else if (context.canceled && _rb.linearVelocity.y > 0) // JUMP RELEASED MID AIR, LESS FORCE
            {
                _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _rb.linearVelocity.y * _PlyrStts.JumpCancelSupresion);
            }

        }
    }


}
