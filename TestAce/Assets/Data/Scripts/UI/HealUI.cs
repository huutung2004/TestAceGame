using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealUI : MonoBehaviour
{
    [SerializeField] private Image _healbar;
    private CharacterHealth heal;
    private float targetHeal;
    private void Start()
    {
        Init();
    }
    private void Init()
    {
       StartCoroutine(FindCharacter());
    }
    private void OnEnable()
    {
        CharacterHealth.OnTakeDame += UpdateUI;
        EndLevelManagerUI.OnPlay += Init;
    }
    private void OnDisable()
    {
        CharacterHealth.OnTakeDame -= UpdateUI;
        EndLevelManagerUI.OnPlay -= Init;
    }
    private void UpdateUI()
    {
        if (heal == null)
        {
            Init();
            if (heal == null)
            {
                Debug.Log("Player chưa spawn");
                return;
            }
        }
        targetHeal = (float)heal.GetHeal() / heal.GetMaxHeal();
        Debug.Log($"target {targetHeal}");
        StopAllCoroutines();
        StartCoroutine(SmoothFill(targetHeal));
    }
    private IEnumerator SmoothFill(float targetHeal)
    {
        while (Mathf.Abs(_healbar.fillAmount - targetHeal) > 0.001f)
        {
            _healbar.fillAmount = Mathf.Lerp(_healbar.fillAmount, targetHeal, Time.deltaTime * 5f);
            yield return null;
        }

        _healbar.fillAmount = targetHeal;
    }
    private IEnumerator FindCharacter()
    {
        yield return null;
        GameObject obj = GameObject.FindGameObjectWithTag("Player");
        if (obj != null)
        {

            heal = obj.GetComponent<CharacterHealth>();
            _healbar.fillAmount = 1;
        }
    }
}
