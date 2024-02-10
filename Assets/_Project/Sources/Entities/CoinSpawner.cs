using System.Collections;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private bool _isWorking = true;
    [SerializeField] private Coin _coinPrefab;
    
    [SerializeField] private int _countToSpawn = 4;
    [SerializeField] private int _countToSpawnSpread = 1;
    [SerializeField] private float _spawnTime = 5;
    
    [SerializeField] private float _launchForceX = 1.2f;
    [SerializeField] private float _launchForceY = 2;
    [SerializeField] private float _launchSpreadX = 1;
    
    private WaitForSeconds _waitTime;
    private Vector2 _normalUp = new Vector2(0, 1);
    
    private void Start()
    {
        _waitTime = new WaitForSeconds(_spawnTime);
        StartCoroutine(Wait());
    }

    private void SpawnBurst()
    {
        int count = Utilities.GetSpreadValue(_countToSpawn, _countToSpawnSpread);

        for (int i = 0; i < count; i++)
        {
            var coin = Instantiate(_coinPrefab, transform.position, transform.rotation);
            coin.Launch(GetLaunchDirection());
        }
    }

    private Vector2 GetLaunchDirection()
    {
        return new Vector2(
            Utilities.GetSpreadValue(_normalUp.x, _launchSpreadX) * _launchForceX,
            _normalUp.y * _launchForceY);
    }

    private IEnumerator Wait()
    {
        while (_isWorking)
        {
            SpawnBurst();
            yield return _waitTime;    
        }
    }
}
