using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParaticalPool : MonoBehaviour
{
    public static ParaticalPool Instance { get; private set; }
    [SerializeField] private Transform _paraticalDie;
    private ObjectPool<Transform> pool;
    private void Awake()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        pool = new ObjectPool<Transform>(_paraticalDie, 20, transform);
    }
    public Transform Get()
    {
        return pool.Get();
    }
    public void ReturnToPool(Transform obj)
    {
        pool.ReturnToPool(obj);
    }

}
