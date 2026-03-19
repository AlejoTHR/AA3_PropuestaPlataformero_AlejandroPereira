using System.Collections;
using TreeEditor;
using UnityEngine;

public class PatrollMovement : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D _rb;
    public Collider2D _clldr;

    [Header("Initial Atributes")]
    public Transform[] PatrolPoints; // OBJECTIVE POSITION GAME OBJECT
    public float WalkSpeed;
    public int PatrolDestination;
    public Animator _animator;

    [Header("Collided Game Object")]
    public GameObject COLLIDED;
    public float AddJumpPower;
   
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _clldr = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();


        _animator.SetFloat("Walk", _rb.linearVelocity.magnitude);
    }

    void Update()
    {
        Walk();
    }
    
    public void Walk()
    {
        if (PatrolDestination == 0) // IF PATROL DESTINATION IS FIRST GAME OBJECT OF ARRAY
        {   // MOVE TOWARDS FIRST GAME OBJECT OF ARRAY
            transform.position = Vector2.MoveTowards(transform.position, PatrolPoints[0].position, WalkSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, PatrolPoints[0].position) < 0.2f) // IF DISTANCE IS CLOSE
            {   //  CHANGE DIRECTION AND GAME OBJECT DESTINATION
                transform.localScale = new Vector3(-1, 1, 1);
                PatrolDestination = 1;
            }

        }
        if (PatrolDestination == 1) // IF PATROL DESTINATION IS SECOND  GAME OBJECT OF ARRAY
        {   // MOVE TOWARDS SECOND GAME OBJECT OF ARRAY
            transform.position = Vector2.MoveTowards(transform.position, PatrolPoints[1].position, WalkSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, PatrolPoints[1].position) < 0.2f)// IF DISTANCE IS CLOSE
            {   //  CHANGE DIRECTION AND GAME OBJECT DESTINATION
                transform.localScale = Vector2.one;
                PatrolDestination = 0;
            }

        }
    }



}
