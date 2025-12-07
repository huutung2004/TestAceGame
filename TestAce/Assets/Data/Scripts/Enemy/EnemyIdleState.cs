using UnityEngine;

public class EnemyIdleState : EnemyState
{
    float timer;
    Vector3 nextTarget;
    public EnemyIdleState(Enemy enemy, Vector3 nextTarget) : base(enemy)
    {
        this.nextTarget = nextTarget;
    }

    public override void Enter()
    {
        timer = enemy.idleTime;
        bool faceLeft = (nextTarget.x < enemy.transform.position.x);
        enemy.PlayIdle(faceLeft);
    }

    public override void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            enemy.ChangeState(new EnemyPatrolState(enemy, nextTarget));
        }
    }
}
