using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class Enemy : MonoBehaviour
{
    public Tilemap tilemap;
    public Vector3Int pointA;
    public Vector3Int pointB;

    public float speed = 2f;
    public float idleTime = 1.5f;
    private Animator animator;
    [HideInInspector] public Vector3 worldA;
    [HideInInspector] public Vector3 worldB;
    [SerializeField] private int heal = 1;
    public bool isShell = false;
    protected EnemyState currentState;

    public void ChangeState(EnemyState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }
    protected virtual void Start()
    {
        worldA = tilemap.CellToWorld(pointA);
        worldB = tilemap.CellToWorld(pointB);

        ChangeState(new EnemyIdleState(this, worldA));
    }

    protected virtual void Update()
    {
        currentState?.Update();
    }
    public Animator GetAnimator()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
        return animator;
    }
    public void PlayIdle(bool isLeft)
    {
        if (IsShell()) {
            string state = isLeft ? "shellHit_l" : "shellHit_r";
            PlayAnimationSafe(state);
            return;
           };
        string stateName = isLeft ? "idle" : "idle_r";
        PlayAnimationSafe(stateName);
    }

    public void PlayRun(bool isLeft)
    {
        if (IsShell())
        {
            string state = isLeft ? "shellHit_l" : "shellhit_r";
            PlayAnimationSafe(state);
            return;
        }
        ;
        string stateName = isLeft ? "run" : "run_r";
        PlayAnimationSafe(stateName);
    }
    private void OnDrawGizmos()
    {
        if (tilemap == null) return;

        Vector3 worldA = tilemap.CellToWorld(pointA);
        Vector3 worldB = tilemap.CellToWorld(pointB);

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(worldA, 0.1f);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(worldB, 0.1f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(worldA, worldB);
    }
    public  void OnStomped(CharacterHealth character)
    {
        int dir = character.transform.position.x < transform.position.x ? 1 : -1;
        if (character.IsInvincible()) return;
        heal = heal -1;
        if (heal <= 0)
        {
            var obj2 = ParaticalPool.Instance.Get();
            obj2.gameObject.transform.position = transform.position;
            StartCoroutine(StartDestroy(obj2));
            return;
        }
        string state;
        if(dir == -1)
        {
            state = "shellHit_l";
        }
        else
        {
            state = "shellHit_r";
        }
        PlayAnimationSafe(state);
        SetShell();
    }
    private void PlayAnimationSafe(string stateName)
    {
        Animator anim = GetAnimator();
        int hash = Animator.StringToHash(stateName);

        if (anim.HasState(0, hash))
        {
            anim.Play(hash);
        }
    }
    private void SetShell()
    {
        isShell = true;

    }

    public bool IsShell()
    {
        return isShell;
    }
    private IEnumerator StartDestroy(Transform obj)
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.3f);
        ParaticalPool.Instance.ReturnToPool(obj);
        Destroy(gameObject);
    }


}
