using DG.Tweening;
using UnityEngine;

public class TakeFruits : MonoBehaviour
{
    [SerializeField] private float flyUpDistance = 1.5f;
    [SerializeField] private float duration = 0.5f;

    private bool isTaken = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isTaken) return;

        if (collision.CompareTag("Player") && FruitsManager.Instance != null)
        {
            isTaken = true;

            FruitsManager.Instance.TakeFruit(1);

            PlayCollectEffect();
        }
    }

    private void PlayCollectEffect()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        transform.DOMoveY(transform.position.y + flyUpDistance, duration)
            .SetEase(Ease.OutQuad);

        sr.DOFade(0f, duration)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                Destroy(gameObject);
            });
    }
}
