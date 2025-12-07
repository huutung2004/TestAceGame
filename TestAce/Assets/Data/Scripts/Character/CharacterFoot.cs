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
    }
}
