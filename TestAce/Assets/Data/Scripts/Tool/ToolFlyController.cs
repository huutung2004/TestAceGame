using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolFlyController : MonoBehaviour
{
    [SerializeField] private float _bounceForce = 2f;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(Vector2.up * _bounceForce, ForceMode2D.Impulse);
        }
    }
}
