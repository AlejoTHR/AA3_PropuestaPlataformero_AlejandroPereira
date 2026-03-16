using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("Objects")]
    public Rigidbody2D _rb;
    public Collider2D _clldr;
    public Animator _animator;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // START COMPONENTS
        _rb = GetComponent<Rigidbody2D>();
        _clldr = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
