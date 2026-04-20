using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class TriggerController : MonoBehaviour
{
    public string[] tags;
    public UnityEvent OnEnter;
    public UnityEvent OnExit;
    public UnityEvent OnStay;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (tags.Contains(collision.tag))
        {
            OnEnter.Invoke();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (tags.Contains(collision.tag))
        {
            OnExit.Invoke();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (tags.Contains(collision.tag))
        {
            OnStay.Invoke();
        }
    }
}