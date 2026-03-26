using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] Collider2D _clldr;
    [SerializeField] Transform _PlayerPosition;

    [SerializeField] float arrowSpeed;
    [SerializeField] float arrowAngle;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _clldr = GetComponent<Collider2D>();
    }

    private void Update()
    {
        _rb.AddForce(transform.forward * arrowSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_clldr != null) return;

        Debug.Log(collision.gameObject.name);

        if(collision.transform.CompareTag("Ground")) 
        {
            _rb.bodyType = RigidbodyType2D.Static;
        }

    }

}
