using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxHit : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemyList = new List<Enemy>();
    private void OnCollisionEnter2D(Collision2D collision)
    {
        HitBox hitBox = gameObject.GetComponentInChildren<HitBox>();
        if(hitBox != null)
        {
            if (hitBox.CanHit())
            {
                foreach (Enemy enemy in _enemyList)
                {
                    enemy.OnStomped(CharacterHealth.Instance);
                }
            }
        }
        if (collision.collider.CompareTag("Enemy"))
        {
            _enemyList.Add(collision.collider.GetComponent<Enemy>());
           
        }
        else Debug.Log("Not Enemy");
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            _enemyList.Remove(collision.collider.GetComponent<Enemy>());
        }
    }

}
