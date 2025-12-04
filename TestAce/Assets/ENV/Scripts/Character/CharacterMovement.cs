using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterMovenemt : MonoBehaviour
{
    [SerializeField] private float _movespeed = 5f;
    [SerializeField] private float _jumpforce = 5f;
    private Rigidbody2D _rigidbody;
    private bool isGrounded;
    private Animator _animator;
    private float lastDir = 1f;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

    }
    private void Update()
    {
        float moveX = 0f;
        if (UIInputController.Instance.isMoveRight)
        {
            moveX = 1f;
            lastDir = 1f;
        }
        if (UIInputController.Instance.isMoveLeft)
        {
            moveX = -1f;
            lastDir = -1f;
        }
        Vector2 movement = new Vector2(moveX * _movespeed, _rigidbody.velocity.y);
        _rigidbody.velocity = movement;
        if (UIInputController.Instance.isJump && isGrounded)
        {
            _rigidbody.AddForce(Vector2.up * _jumpforce, ForceMode2D.Impulse);
            UIInputController.Instance.isJump = false;
        }
        HandleAnimation(lastDir);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    private void HandleAnimation(float moveX)
    {
        if( UIInputController.Instance.isMoveLeft && isGrounded)
        {
           _animator.Play("run_l");
        }
        if (UIInputController.Instance.isMoveRight && isGrounded)
        {
            _animator.Play("run");
        }
        if (!UIInputController.Instance.isMoveLeft && !UIInputController.Instance.isMoveRight && isGrounded)
        {
            if(lastDir == 1)
            _animator.Play("idle");
            else _animator.Play("idle_l");
        }
        if(!isGrounded)
        {
            if(lastDir == 1)
            _animator.Play("jum");
            else _animator.Play("jum_l");
        }
    }
}
