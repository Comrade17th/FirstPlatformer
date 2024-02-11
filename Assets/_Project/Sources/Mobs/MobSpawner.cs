using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class MobSpawner : MonoBehaviour
{
    [SerializeField] private Mob _mobPrefab;
    [SerializeField] private float _delay = 2.0f;
    [SerializeField] private float _spawnRate = 2.0f;
    [SerializeField] private Transform _route;

    [SerializeField, Min(1)] private int maxPoolCapacity = 10;

    private WaitForSeconds _wait;
    private List<Mob> _mobsPool;
    private Stack<int> _deadMobs;
    private Coroutine _spawnCoroutine;

    private void Start()
    {
        _wait = new WaitForSeconds(_spawnRate);
        InitMobs();
    }

    private void InitMobs()
    {
        _mobsPool = new List<Mob>();
        _deadMobs = new Stack<int>();
        
        for (int i = 0; i < maxPoolCapacity; i++)
        {
            Vector3 spawnPosition = transform.position;
            Mob mob = Instantiate(_mobPrefab, spawnPosition, transform.rotation);

            _mobsPool.Add(mob);
            mob.SetSpawner(GetComponent<MobSpawner>(), i);
            mob.SetRoute(_route);    
        }
        
    }

    private IEnumerator SpawnFromPool()
    {
        while (_deadMobs.Count > 0)
        {
            yield return _wait;
            int id = _deadMobs.Pop();
            Respawn(id);
        }
            
        yield break;   
    }

    private void Respawn(int id)
    {
        Mob mob = _mobsPool[id];
        mob.SetAlive();
        mob.transform.position = transform.position;
        mob.gameObject.SetActive(true);
    }

    public void MobDead(int id)
    {
        _mobsPool[id].gameObject.SetActive(false);
        _deadMobs.Push(id);
        _spawnCoroutine = StartCoroutine(SpawnFromPool());
    }
}
