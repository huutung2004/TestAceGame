using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class FruitsManager : MonoBehaviour
{
    public static FruitsManager Instance { get; private set; }

    private int currentFruits = 0;

    [SerializeField] TMP_Text _fruitCount;

    private void Awake()
    {
        Instance = this;
    }

    public void TakeFruit(int count)
    {
        currentFruits += count;
        RefreshUI();
    }

    public int GetCurrentFruits()
    {
        return currentFruits;
    }

    private Tween countTween;
    private Tween punchTween;

    private void RefreshUI()
    {
        countTween?.Kill();
        punchTween?.Kill();

        int endValue = currentFruits;

        int startValue;
        if (!int.TryParse(_fruitCount.text, out startValue))
            startValue = 0;

        if (startValue == endValue)
            startValue = Mathf.Max(0, endValue - 1);

        countTween = DOTween.To(() => startValue, x =>
        {
            _fruitCount.text = x.ToString();
        }, endValue, 0.3f)
        .SetEase(Ease.OutQuad);

        _fruitCount.transform.localScale = Vector3.one;

        punchTween = _fruitCount.transform
            .DOScale(1.35f, 0.12f)
            .SetLoops(2, LoopType.Yoyo)
            .SetEase(Ease.OutBack);
    }


    public void ResetFruit()
    {
        currentFruits = 0;
        RefreshUI();
    }
}
