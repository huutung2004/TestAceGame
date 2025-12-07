using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterMovenemt : MonoBehaviour
{
    [SerializeField] private float _movespeed = 5f;
    [SerializeField] private float _jumpforce = 5f;
    private Rigidbody2D _rigidbody;
    private BoxCollider2D _colider;
    private bool isGrounded;
    private Animator _animator;
    private CharacterHealth _characterHeal;
    private bool isKnocked;
    private float lastDir = 1f;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _characterHeal = GetComponent<CharacterHealth>();
        _colider = GetComponent<BoxCollider2D>();

    }
    private void Update()
    {
        if (isKnocked) return;
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
        if (collision.gameObject.CompareTag("Trap"))
        {
            _characterHeal.TakeDamage(1);
        }
        if (!collision.collider.CompareTag("Enemy") || _characterHeal.IsInvincible()) return;
        _characterHeal.TakeDamage(1);
        float dir = (transform.position.x < transform.position.x) ? -1 : 1;
        Vector2 knockForce = new Vector2(dir * 2f, 4f);
        KnockBack(knockForce, _characterHeal.GetInvinceTime());

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
        if (UIInputController.Instance.isMoveLeft && isGrounded)
        {
            _animator.Play("run_l");
        }
        if (UIInputController.Instance.isMoveRight && isGrounded)
        {
            _animator.Play("run");
        }
        if (!UIInputController.Instance.isMoveLeft && !UIInputController.Instance.isMoveRight && isGrounded)
        {
            if (lastDir == 1)
                _animator.Play("idle");
            else _animator.Play("idle_l");
        }
        if (!isGrounded)
        {
            if (lastDir == 1)
                _animator.Play("jum");
            else _animator.Play("jum_l");
        }
    }
    public void KnockBack(Vector2 force, float duration)
    {
        if (!isKnocked)
            StartCoroutine(Knock(force, duration));
    }
    private IEnumerator Knock(Vector2 force, float time)
    {
        isKnocked = true;
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.AddForce(force, ForceMode2D.Impulse);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Character"), LayerMask.NameToLayer("Enemy"), true);
        yield return new WaitForSeconds(time / 2);
        isKnocked = false;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Character"), LayerMask.NameToLayer("Enemy"), false);
    }
}
