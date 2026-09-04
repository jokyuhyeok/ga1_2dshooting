using UnityEngine;

// 역할 : 일정 시간마다 적을 생성해주고 싶다.
public class EnemySpawner : MonoBehaviour
{
    // 필요 속성
    // - 타이머
    [Header("스폰 간격")][SerializeField] private float _spawnInternal = 3f;
    private float _timer;

    // - 생성할 프리팹
    [Header("스폰할 적 프리팹")][SerializeField] private Enemy _enemyPrefab;

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _spawnInternal)
        {
            _timer = 0f;

            _spawnInternal = UnityEngine.Random.Range(1f, 3f); // float : 1~3
            int randomInt = Random.Range(1, 3); // int : 1~2
            Spawn();
        }
    }

    private void Spawn()
    {
        Enemy enemy = Instantiate(_enemyPrefab);
        enemy.transform.position = transform.position;
    }
}