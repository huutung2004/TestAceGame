using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectTilePool : MonoBehaviour
{
    public static ProjectTilePool Instance { get; private set; }
    [SerializeField] private ProjectTile projectTile;
    private ObjectPool<ProjectTile> pool;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        pool = new ObjectPool<ProjectTile>(projectTile, 20, transform);
    }
    public ProjectTile GetProjectTile()
    {
        return pool.Get();
    }
    public void ReturnProjectTile(ProjectTile proj)
    {
        pool.ReturnToPool(proj);
    }
}
