using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    [Header("Objects")]
    public Rigidbody2D _rb;
    public Collider2D _clldr;
    public Animator _animator;
    public Movement_Controller _mvmntCnrtllr;

    [Header("Arrow Prefab")]
    public GameObject arrow;

    [Header("Arrow Instantiate properties")]
    public float arrowOffsetSpanw;
    public float shootCooldown;
    public float shootCooldownMax;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // START COMPONENTS
        _rb = GetComponent<Rigidbody2D>();
        _clldr = GetComponent<Collider2D>();
        _mvmntCnrtllr = GetComponent<Movement_Controller>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        shootCooldown += Time.time/10000;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {


        if (context.performed && shootCooldown >= shootCooldownMax)
        {
            // SETS ARROW INSTANTIATE OFFSET
            arrowOffsetSpanw = (_mvmntCnrtllr.LookingRight)? 1 : -1;

            // ARROW ORIGIN POSITION
            Vector3 ArrowOriginPosition = new Vector3(transform.position.x + arrowOffsetSpanw, transform.position.y);

            // CREATES GAME OBJECT ARROW WITH PREFAB AND ORIGIN VECTOR
            GameObject newArrow = Instantiate(arrow, ArrowOriginPosition, transform.rotation);
            
            
            shootCooldown = 0;
        }



    }

}
