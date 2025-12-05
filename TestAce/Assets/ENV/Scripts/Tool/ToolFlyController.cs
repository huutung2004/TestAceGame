using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolFlyController : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D rb =  collision.gameObject.GetComponent<Rigidbody2D>();
        if(rb != null)
        {
            Debug.Log("Flying Tool Triggered");
            rb.AddForce(new Vector2(0, 10f));
        }
    }
}
