using System;
using System.Collections;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public static CharacterHealth Instance { get; private set; }
    private int currentHealth;
    [SerializeField] private int maxHeal = 3;
    [SerializeField] private GameObject deathEffect;

    [Header("Invincibility Settings")]
    public float invincibleDuration = 2f;
    private bool isInvincible = false;
    [Header("GrowupSize Settings")]
    public float growupDuration = 0f;
    private bool isGrowup = false;
    private SpriteRenderer sr;
    //event
    public static event Action OnTakeDame;
    public static event Action OnCharacterDie;
    void Start()
    {
        Instance = this;
        currentHealth = maxHeal;
        sr = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (growupDuration > 0)
        {
            isGrowup = true;
            growupDuration -= Time.deltaTime;
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * 1.2f, Time.deltaTime * 5f);
        }
        else {
            isGrowup = false;
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, Time.deltaTime * 5f);
        } 
    }
    public void TakeDamage(int amount)
    {
        if (isInvincible) return;
        MusicManager.Instance.PlayMusic("hurt");

        currentHealth -= amount;
        OnTakeDame?.Invoke();

        if (currentHealth <= 0)
        {
            OnCharacterDie?.Invoke();
            if (deathEffect != null)
            {
                Instantiate(deathEffect, transform.position, Quaternion.identity);
            }
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
            if (visible) sr.color = Color.red;
            else sr.color = Color.white;

            yield return new WaitForSeconds(blinkInterval);
        }

        sr.enabled = true;
        sr.color = Color.white;
        isInvincible = false;
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
    public void SetInvince(bool invence)
    {
        isInvincible = invence;
    }
    public void ChangeGrowupTime(float duration)
    {
        growupDuration += duration;
    }
    public bool IsGrowup()
    {
        return isGrowup;
    }
}
