using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : Enemy
{
    public float shootCooldown = 2f;
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public int dir = 1;
    public int damage = 1;
    protected override void Start()
    {
        ChangeState(new EnemyAttackState(this));
    }

    protected override void Update()
    {
        currentState?.Update();
    }
    public void ShootBullet()
    {
        ProjectTile projectTile = ProjectTilePool.Instance.GetProjectTile();
        projectTile.Init(dir,damage,shootPoint);
    }

}
