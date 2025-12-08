using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFoot : MonoBehaviour
{
    private CharacterHealth character;
    private void Awake()
    {
        character = GetComponentInParent<CharacterHealth>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy == null) return;
        enemy.OnStomped(character);
        StartCoroutine(InvinceTime(1f));
    }
    private IEnumerator InvinceTime(float time) {
        CharacterHealth.Instance.SetInvince(true);
        yield return new WaitForSeconds(time);
        CharacterHealth.Instance.SetInvince(false);

    }
    //private void OnTriggerStay2D(Collider2D other)
    //{
    //    if (!other.CompareTag("Enemy")) return;
    //    Enemy enemy = other.GetComponent<Enemy>();
    //    if (enemy == null) return;
    //    enemy.OnStomped(character);
    //}
}
