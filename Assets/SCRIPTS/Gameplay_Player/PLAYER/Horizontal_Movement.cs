using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(PlayerStats))]
[RequireComponent(typeof(Animator))]
public class Horizontal_Movement : MonoBehaviour
{
    [Header("STATS")]
    [SerializeField] private PlayerStats _PlyrStts;

    [Header("Components")]
    [SerializeField] public Rigidbody2D _rb;
    [SerializeField] public Animator _animator;

    [Header("Horizontal Movement")]
    Vector2 HorizontalMove;
    [SerializeField] public bool LookingRight = true;

    private void Start()
    {        // START COMPONENTS
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // RB VELOCITY FOR HORIZONTAL MOVEMENT
        _rb.linearVelocity = new Vector3(HorizontalMove.x * _PlyrStts.Speed, _rb.linearVelocity.y);
         // RESPAWN
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed) // IF KEYBINDS PRESSED TO MOVE
        {   // MOVEMENT CHANGES
            HorizontalMove.x = context.ReadValue<Vector2>().x;

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
            if(HorizontalMove.x != 0) _animator.SetBool("Running", true);

        }
        else if (context.canceled)// IF KEYBINDS RELEASED, STANDS STILL
        {
            _animator.SetBool("Running", false);

            HorizontalMove = Vector2.zero;
        }

    }

}
