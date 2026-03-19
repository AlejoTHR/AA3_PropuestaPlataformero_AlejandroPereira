using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements.Experimental;

public class Movement_Controller: MonoBehaviour
{
    [Header("Objects")]
    public Rigidbody2D _rb;
    public Collider2D _clldr;
    public Animator _animator;
    public Canvas _canvas;

    [Header("Input Variables")]
    public float Speed;
    public Vector2 HorizontalMove;

    [Header("Direction Reader")]
    public bool LookingRight = true;

    [Header("Debug")]
    public Vector2 DebugMove;

    [Header("Jump Kill")]
    public GameObject _collided;
    public float AddedJumpPower;


    private void Awake()
    {   // START COMPONENTS
        _rb = GetComponent<Rigidbody2D>();
        _clldr = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        DebugMove = _rb.linearVelocity;

        // RB VELOCITY FOR MOVEMENT
        _rb.linearVelocity = new Vector3(HorizontalMove.x * Speed, _rb.linearVelocity.y);

        // ANIMATOR SETS
        _animator.SetFloat("Running", _rb.linearVelocity.magnitude);
    }



    // MOVEMENT
    public void OnMove(InputAction.CallbackContext context)
    {
        if(context.performed) // IF KEYBINDS PRESSED TO MOVE
        {   // MOVEMENT CHANGES
            HorizontalMove = context.ReadValue<Vector2>();

            if (HorizontalMove.x > 0 )
            { // IF LAST INPUT IS RIGHT, LOOKS RIGHT AND KEEPS LOOKING RIGTH
                LookingRight = true;
                // SEE FUNCTION, TURNING SPRITE AROUND
                TurnAround();
            }
            else
            { // IF LAST INPUT IS LEFT, KEEPS LOKING LEFT
                LookingRight = false;
                // SEE FUNCTION, TURNING SPRITE AROUND
                TurnAround();
            }
        }

        else if(context.canceled)// IF KEYBINDS RELEASED, STANDS STILL
        {
            HorizontalMove = Vector2.zero;
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

    private void OnCollisionEnter2D(Collision2D collided)
    {
        _collided = collided.gameObject; // COLLIDED GAME OBJECT

        if(collided.gameObject.CompareTag("Spring"))
        {
            PatrollMovement _ptrllr = GetComponent<PatrollMovement>();
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, AddedJumpPower);
        }

    }



    public void Pause() // PAUSE
    {
        _canvas.enabled = true;
        Time.timeScale = 0f;

    }


}
