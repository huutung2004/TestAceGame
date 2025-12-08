using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : EnemyState
{
    Vector3 target;
    public EnemyPatrolState(Enemy enemy, Vector3 target) : base(enemy)
    {
        this.target = target;
    }
    public override void Enter()
    {
        bool faceLeft = (target.x < enemy.transform.position.x);
            enemy.PlayRun(faceLeft);
    }
    public override void Update()
    {
        enemy.transform.position = Vector3.MoveTowards(
            enemy.transform.position,
            target,
            enemy.speed * Time.deltaTime
        );
        if (Vector3.Distance(enemy.transform.position, target) < 0.05f)
        {
            Vector3 next = (target == enemy.worldA) ? enemy.worldB : enemy.worldA;

            enemy.ChangeState(new EnemyIdleState(enemy, next));
        }
    }

}
