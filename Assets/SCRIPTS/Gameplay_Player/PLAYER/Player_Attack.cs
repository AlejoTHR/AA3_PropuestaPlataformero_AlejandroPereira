using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(PlayerStats))]
[RequireComponent(typeof(Animator))]
public class Player_Attack : MonoBehaviour
{
    [Header("STATS")]
    [SerializeField] private PlayerStats _PlyrStts;

    [Header("Components")]
    [SerializeField] public Rigidbody2D _rb;
    [SerializeField] public Animator _animator;

    [Header("OTHER SCRIPTS")]
    [SerializeField] public Ground_Detection _GrndDtct;

    [Header("Attk Points")]
    [SerializeField] public Transform sideAttkPoint;
    [SerializeField] public Transform downAttkPoint;


    private void Start()
    {
        // START COMPONENTS
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        _GrndDtct = GetComponent<Ground_Detection>();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed && _GrndDtct.IsGrounded)
        {
            _animator.SetBool("Attacked", true);
        }
        if (context.performed && !_GrndDtct.IsGrounded && (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)))
        {
            _animator.SetBool("AttackedDown", true);
        }
    }
    public void StartDamageSide()// CALLED IN ANIMATOR
    {
        Collider2D enemyhit = Physics2D.OverlapCircle(sideAttkPoint.position, _PlyrStts.radiusSide, _PlyrStts.enemies);

        if (enemyhit != null)
        {
            Debug.Log(_PlyrStts.damage);

            enemyhit.GetComponent<Controller_Enemy>().health -= _PlyrStts.damage;
        }
    }
    public void StartDamageDown()// CALLED IN ANIMATOR
    {
        Collider2D enemyhit = Physics2D.OverlapCircle(downAttkPoint.position, _PlyrStts.radiusSide, _PlyrStts.enemies);

        if (enemyhit != null)
        {
            Debug.Log(_PlyrStts.damage);

            enemyhit.GetComponent<Controller_Enemy>().health -= _PlyrStts.damage;
        }
    }

    public void PogoStart() // CALLED IN ANIMATOR
    {
        Collider2D enemyhit = Physics2D.OverlapCircle(downAttkPoint.position, _PlyrStts.radiusSide, _PlyrStts.enemies);
        if (enemyhit != null)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0);
            _rb.AddForce(Vector2.up * _PlyrStts.JumpForce);
        }
    }
    void FinishAttkAnim_Sideattk() { _animator.SetBool("Attacked", false); } // CALLED IN ANIMATOR
    void FinishAttkAnim_Downattk() { _animator.SetBool("AttackedDown", false); } // CALLED IN ANIMATOR

    private void OnDrawGizmos() // VISIBLE RANGE IN SCENE
    {
        Gizmos.DrawWireSphere(sideAttkPoint.position, _PlyrStts.radiusSide);
        Gizmos.DrawWireSphere(downAttkPoint.position, _PlyrStts.radiusDown);
    }


}
