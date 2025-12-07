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
    [SerializeField] private GameObject _particalDie;

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
        string stateName = isLeft ? "idle" : "idle_r";
        PlayAnimationSafe(stateName);
    }

    public void PlayRun(bool isLeft)
    {
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
    public void OnStomped(CharacterHealth character)
    {
        if (character.IsInvincible()) return;
        Instantiate(_particalDie, transform.position + new Vector3(0, 0.5f, 0), transform.rotation);
        Destroy(gameObject);
        Debug.Log("Enemy die");
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

}
