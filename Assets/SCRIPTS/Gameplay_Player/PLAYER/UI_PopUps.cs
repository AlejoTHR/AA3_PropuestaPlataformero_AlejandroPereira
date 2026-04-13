using UnityEngine;
using UnityEngine.Events;


[RequireComponent (typeof(Collider2D))]
public class UI_PopUps : MonoBehaviour
{

    [Header("Components")]
    public UnityEvent onTriggerEnter;
    public UnityEvent onTriggerExit;
    public UnityEvent onTriggerStay;
    [Header("Filters By tag")]
    public string tag;

    private void OnTriggerEnter2D(Collider2D collided)
    {
        if (collided == null) return;
        if(collided.gameObject.CompareTag(tag))
        {
            onTriggerEnter.Invoke();
        }

    }

    private void OnTriggerExit2D(Collider2D uncollided)
    {
        if (uncollided == null) return;
        if (uncollided.gameObject.CompareTag(tag))
        {
            onTriggerExit.Invoke();
        }
    }

    private void OnTriggerStay2D(Collider2D collided)
    {
        if (collided == null) return;
        if (collided.gameObject.CompareTag(tag))
        {
            onTriggerExit.Invoke();
        }
    }

}
