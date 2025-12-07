using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState{
    protected Enemy enemy;
    public EnemyState(Enemy enemy)
    {
        this.enemy = enemy;
    }
    public virtual void Enter(){}
    public virtual void Update(){}
    public virtual void Exit(){}
}
