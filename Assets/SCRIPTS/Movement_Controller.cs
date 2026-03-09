using UnityEngine;
using UnityEngine.InputSystem;

public class Movement_Controller: MonoBehaviour
{
    [Header("Objects")]
    public Rigidbody2D _rb;
    public Collider2D _clldr;

    [Header("Input Variables")]
    public float Speed;
    public Vector2 HorizontalMove;

    [Header("Direction Reader")]
    public bool LookingRight = true;
    public Vector2 TurnedAround;

    [Header("Movement Variables")]
    public Vector2 DebugMove;

    [Header("Jumping")]
    public float JumpForce;
    public float JumpCancelSupresion = 0.5f;
    public Vector2 JumpDirection;

    private void Awake()
    {   // START COMPONENTS
        _rb = GetComponent<Rigidbody2D>();
        _clldr = GetComponent<Collider2D>();


    }

    private void Update()
    {
        DebugMove = _rb.linearVelocity;
        // SEE FUNCTION, TURNING SPRITE AROUND
        TurnAround();



        // RB VELOCITY FOR MOVEMENT
        _rb.linearVelocity = new Vector3(HorizontalMove.x * Speed, _rb.linearVelocity.y);



    }

    // MOVEMENT
    public void OnMove(InputAction.CallbackContext context)
    {
        if(context.performed) // IF KEYBINDS PRESSED TO MOVE
        {   // MOVEMENT CHANGES
            HorizontalMove = context.ReadValue<Vector2>();

            if(HorizontalMove.x > 0 )
            { // IF LAST INPUT IS RIGHT, LOOKS RIGHT AND KEEPS LOOKING RIGTH
                LookingRight = true;
            }
            else
            { // IF LAST INPUT IS LEFT, KEEPS LOKING LEFT
                LookingRight = false;
            }
        }

        else if(context.canceled)// IF KEYBINDS RELEASED, STANDS STILL
        {
            HorizontalMove = Vector2.zero;
        }

    }
    // JUMP
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed) // JUMP HOLDED; FULL FORCE
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, JumpForce);
        }
        else if (context.canceled) // JUMP RELEASED MID AIR, LESS FORCE
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _rb.linearVelocity.y * JumpCancelSupresion);

        }
    }

    // TURNING AROUND
    public void TurnAround()
    {
        if(!LookingRight) // IF IS NOT LOOKING RIGHT
        {
            transform.localScale = new Vector2 (-1,1); // LOOKS LEFT
        }
        else // IF IS LOOKING RIGHT
        {
            transform.localScale = new Vector2(1, 1); // LOOKS RIGHT
        }
    }

    



}
