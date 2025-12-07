using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance;
    [SerializeField] private Image _dialog;
    private CanvasGroup _canvasGroup;
    private void Awake()
    {
        Instance = this;
        _dialog.gameObject.SetActive(false);
        _canvasGroup = _dialog.gameObject.GetComponent<CanvasGroup>();
    }
    public void ShowDialog(string notify, float duration)
    {
        if (_dialog != null)
        {
            TMP_Text text =  _dialog.gameObject.GetComponentInChildren<TMP_Text>();
            text.text = notify;
            _dialog.gameObject.SetActive(true);
            _canvasGroup.DOFade(0, duration).OnComplete(() =>
            {
                StartCoroutine(HideDialog(duration));
            });
        }
    }
    private IEnumerator HideDialog(float duration)
    {
        yield return new WaitForSeconds(duration);
        _dialog.gameObject.SetActive(false);
    }

}
