using UnityEngine;

public class Arrow : MonoBehaviour
{


    [Header("Objects")]
    public Rigidbody2D _rb;
    public Collider2D _colldr;
    public Movement_Controller _mvmntCnrtllr;

    [Header("Arrow Properties")]
    public float ArrowSpeed;
    public float ArrowDestroyTime;
    public Vector3 ArrowAngle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {   // START COMPONENTS
        _rb = GetComponent<Rigidbody2D>();
        _colldr = GetComponent<Collider2D>();

    }

    private void Start()
    {
        // ADDS FORCE TO INSTANTIATED ARROW

        Debug.Log("ARROW RIGGHT");
        _rb.linearVelocity = Vector2.right * ArrowSpeed;

    }

    private void Update()
    {
        // ROTATES AS FALLS
        transform.eulerAngles += ArrowAngle;

        // DESTROY AFTER TIME PASSED
        Destroy(gameObject, ArrowDestroyTime);

    }


    private void OnTriggerEnter2D(Collider2D collided)
    {
        if (collided.gameObject.CompareTag("Enemy")) // IF TOUCHES AN ENEMY
        {
            Destroy(gameObject);
        }

        if(collided.gameObject.CompareTag("Ground")) // IF TOUCHES THE GROUND
        {
            _rb.bodyType = RigidbodyType2D.Static; // ARROW STAYS IN PLACE
            _colldr.enabled = false; // ARROW COLLIDER DEACTIVATED
            ArrowAngle = Vector3.zero; // ARROW ROTATER VARIABLE STOPS ROTATING
        }



    }

}
