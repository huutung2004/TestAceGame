using System;
using System.Collections;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    private int currentHealth;
    [SerializeField] private int maxHeal = 3;

    [Header("Invincibility Settings")]
    public float invincibleDuration = 2f;
    private bool isInvincible = false;

    private SpriteRenderer sr;
    //event
    public static event Action OnTakeDame;
    public static event Action OnCharacterDie;
    void Start()
    {
        currentHealth = maxHeal;
        sr = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int amount)
    {
        if (isInvincible) return;

        currentHealth -= amount;
        OnTakeDame?.Invoke();

        if (currentHealth <= 0)
        {
            OnCharacterDie?.Invoke();
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(InvincibleFade());
        }
    }

    private IEnumerator InvincibleFade()
    {
        isInvincible = true;

        float elapsed = 0f;
        float blinkInterval = 0.1f; 
        bool visible = true;

        while (elapsed < invincibleDuration)
        {
            elapsed += blinkInterval;
            visible = !visible;
            sr.enabled = visible;
            if(visible) sr.color = Color.red;
            else sr.color = Color.white;

            yield return new WaitForSeconds(blinkInterval);
        }

        sr.enabled = true;
        sr.color = Color.white;
        isInvincible = false;
    }


    void Die()
    {
        Debug.Log("Player died");
        // Destroy(gameObject);
    }
    public float GetInvinceTime()
    {
        return invincibleDuration;
    }
    public bool IsInvincible()
    {
        return isInvincible;
    }
    public int GetHeal()
    {
        return currentHealth;
    }
    public int GetMaxHeal()
    {
        return maxHeal;
    }
    
}
