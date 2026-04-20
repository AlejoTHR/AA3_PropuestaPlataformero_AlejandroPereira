using System.Security;
using UnityEngine;

public class Controller_Enemy : MonoBehaviour
{

    [Header("COMPONENTS")]
    public Animator _animator;
    public Collider2D _clldr;

    [Header("ATRIBUTES")]
    [SerializeField] private float currentHealth;
    public float health;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _clldr = GetComponent<Collider2D>();
        currentHealth =  health;
    }

    private void Update()
    {
        _animator.SetFloat("Life", currentHealth);
        
        if(health < currentHealth)
        {
            currentHealth = health; // HAELTH READER
            _animator.SetBool("Attaked", true);
        }
        if(health <= 0)
        {
            IsDead();
        }
    }

    public void IsDead()
    {
        health = 0;
        _clldr.enabled = false;
        Debug.Log("THIS ENEMY IS DEATH");
    }

    public void FinishedAnimAttaked() { _animator.SetBool("Attaked", false); }
}
