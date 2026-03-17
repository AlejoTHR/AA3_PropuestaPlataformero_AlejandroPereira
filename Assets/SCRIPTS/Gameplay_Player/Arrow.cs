using UnityEngine;

public class Arrow : MonoBehaviour
{

    [Header("Objects")]
    public Rigidbody2D _rb;
    public Movement_Controller _mvmntCnrtllr;

    [Header("Arrow Properties")]
    public float ArrowSpeed;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {   // START COMPONENTS
        _rb = GetComponent<Rigidbody2D>();
        
        // START ARROW
        if(_mvmntCnrtllr.LookingRight) _rb.AddForce(Vector2.right * ArrowSpeed);
        else _rb.AddForce(Vector2.right * ArrowSpeed*-1);

    }

    private void Update()
    {

        Destroy(gameObject, 5f);
    }


    private void OnTriggerEnter2D(Collider2D collided)
    {
        _rb.linearVelocity = Vector2.zero;

        if(collided.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }

    }

}
