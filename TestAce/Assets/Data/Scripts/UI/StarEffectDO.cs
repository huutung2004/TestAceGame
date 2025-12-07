using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class StarEffectDO : MonoBehaviour
{
    public static StarEffectDO Instance { get; private set; }
    public Image star1;
    public Image star2;
    public Image star3;
    public Image coin;
    public TMP_Text _cointText;

    public float delayBetweenStars = 0.25f;

    private void Awake()
    {
        Instance = this;
    }
    public void PlayStars(int starCount)
    {
        if (starCount >= 1) StartCoroutine(ShowStar(star1, 0));
        if (starCount >= 2) StartCoroutine(ShowStar(star2, delayBetweenStars));
        if (starCount >= 3) StartCoroutine(ShowStar(star3, delayBetweenStars * 2));
        StartCoroutine(ShowStar(coin, delayBetweenStars * 2));
    }

    private IEnumerator ShowStar(Image star, float delay)
    {
        yield return new WaitForSeconds(delay);

        // Thiết lập ban đầu
        star.color = new Color(1, 1, 1, 0);
        star.transform.localScale = Vector3.zero;

        Sequence seq = DOTween.Sequence();

        // Fade + scale cùng lúc
        seq.Append(star.DOFade(1f, 0.35f));
        seq.Join(star.transform.DOScale(1f, 0.35f).SetEase(Ease.OutBack));

        // Nhún nhẹ
        seq.Append(star.transform.DOPunchScale(
            new Vector3(0.2f, 0.2f, 0), 0.25f, 10, 1f));
    }
    public void PlayCoinReward(int reward)
    {
        int currentValue = 0;

        DOTween.To(() => currentValue, x =>
        {
            currentValue = x;
            _cointText.text = currentValue.ToString();
        },
        reward,                    
        3f
        ).SetEase(Ease.OutQuad);
    }

}
