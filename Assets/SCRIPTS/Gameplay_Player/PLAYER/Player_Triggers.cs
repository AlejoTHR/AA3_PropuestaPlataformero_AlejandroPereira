using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Player_Triggers : MonoBehaviour
{
    [Header("STATS")]
    [SerializeField] PlayerStats _PlyrStts;

    [Header("COMPONENTS")]
    public Rigidbody2D _rb;

    [Header("Repawn")]
    [SerializeField] Vector3 Checkpoint_Position;

    [Header("EVENTS")]
    public UnityEvent OnEnter;
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collided)
    {

        if (collided == null) return;

        if (collided.gameObject.CompareTag("Checkpoint"))
        {
            ChangeCheckpoint(collided);
        }
        if (collided.gameObject.CompareTag("Death"))
        {
            SendToCheckpoint();
        }
        if (collided.gameObject.CompareTag("Chest"))
        {
            Chest(collided);
        }
        if (collided.gameObject.CompareTag("Key"))
        {
            Key(collided);        
        }
    }

    public void ChangeCheckpoint(Collider2D collided)
    {
        Checkpoint_Position = collided.gameObject.transform.position;
    }

    public void SendToCheckpoint()
    {
        _rb.linearVelocity = Vector3.zero;
        transform.position = Checkpoint_Position;
    }

    public void Chest(Collider2D collided)
    {
        collided.GameObject().GetComponent<Animator>().SetBool("IsOpen", true);
        _PlyrStts.KeyCount--;
    }

    public void Key(Collider2D collided)
    {
        _PlyrStts.KeyCount++;
        Destroy(collided.gameObject);
    }

}
