using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    float timer = 0f;
    public EnemyAttackState(Enemy enemy) : base(enemy)
    {

    }
    public override void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            Shoot();
            timer = ((Flower)enemy).shootCooldown;
        }
    }

    void Shoot()
    {
        Flower f = (Flower)enemy;
        if (f.dir == 1)
            f.GetAnimator().Play("attack_r");
        else f.GetAnimator().Play("attack");
    }
}
