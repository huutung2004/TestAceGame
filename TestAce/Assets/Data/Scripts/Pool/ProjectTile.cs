using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectTile : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    //-1 left 1 right
    private int direction;
    private int damage;
    private float lifetime = 0.5f;

    public void Init(int direction, int damage, Transform transform)
    {
        this.direction = direction;
        this.damage = damage;
        this.transform.position = transform.position;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(direction, 0) * speed;
        CancelInvoke();
        Invoke(nameof(ReturnToPool), lifetime);
    }

    private void ReturnToPool()
    {
        CancelInvoke();
        ProjectTilePool.Instance.ReturnProjectTile(this);
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CharacterHealth characterHealth = collision.gameObject.GetComponent<CharacterHealth>();
            characterHealth.TakeDamage(damage);
            ReturnToPool();
            Vector2 knockForce = new Vector2(direction * 2f, 4f);
            collision.gameObject.GetComponent<CharacterMovenemt>().KnockBack(knockForce, characterHealth.GetInvinceTime());
        }
        else if (collision != null)
        {
            ReturnToPool();
        }
    }
}
