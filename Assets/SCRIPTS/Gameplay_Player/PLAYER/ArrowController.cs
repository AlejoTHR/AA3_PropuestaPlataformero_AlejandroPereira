using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class ArrowController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] Collider2D _clldr;

    private void Awake()
    {   // INSTANTIATES COMPONENTS
        _rb = GetComponent<Rigidbody2D>();
        _clldr = GetComponent<Collider2D>();
    }


    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (_clldr == null) return;
        if(collision.transform.CompareTag("Ground")) // IF TOUCHES THE GROUND
        {
            _rb.bodyType = RigidbodyType2D.Static; // ARROW STAYS IN PLACE
            _clldr.enabled = false; // ARROW COLLIDER DEACTIVATED
            Destroy(gameObject, 1f); // DESTROY AFTER 1 SECOND
        }

    }

}
