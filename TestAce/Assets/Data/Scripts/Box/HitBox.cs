using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitBox : MonoBehaviour
{
    
    [SerializeField] private int _health = 1;
    private bool isHit;
    private bool _hit2 = true;
    private Animator animator;
    [SerializeField] private GameObject _paraticalBreak;
    private void Start()
    {
        animator = GetComponentInParent<Animator>();
        if(animator == null)
        {
            Debug.LogError("Animator component not found in parent GameObject.");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && _hit2 && animator!=null && CharacterHealth.Instance.IsGrowup())
        {
            animator.Play("hit_box");
            isHit = true;
            animator.transform.DOMoveY(animator.transform.position.y + 0.3f, 0.2f).SetLoops(2,LoopType.Yoyo);
            animator.transform.DOScale(new Vector3(0.9f, 0.9f, 0.9f), 0.1f).SetLoops(2, LoopType.Yoyo);
            Debug.Log("Hit true");
            _health--;
            StartCoroutine(TimeHit());
            StartCoroutine(TimeHit2());
        }
    }
    private IEnumerator TimeHit()
    {
        var obj = ParaticalPool.Instance.Get();
            obj.transform.position = animator.transform.position;
        yield return new WaitForSeconds(0.3f);
        ParaticalPool.Instance.ReturnToPool(obj);
        isHit = false;
        if (_health <= 0) {

            Destroy(animator.gameObject); 
        }
        Debug.Log("Hit false");
        
    }
    private IEnumerator TimeHit2()
    {
        _hit2 = false;
        yield return new WaitForSeconds(0.7f);
        animator.transform.DOMoveY(animator.transform.position.y - 0.3f, 0.025f).SetLoops(2, LoopType.Yoyo);
        _hit2 = true;
        Debug.Log("Hit true again");
    }
    public bool CanHit()
    {
        return isHit;
    }
}
