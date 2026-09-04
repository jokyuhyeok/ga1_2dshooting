using UnityEngine;

// 역할 : 일정 시간마다 적을 생성해주고 싶다.
public class EnemySpawner : MonoBehaviour
{
    // 필요 속성
    // - 타이머
    [Header("스폰 간격")][SerializeField] private float _spawnInterval = 3f;
    private float _timer;

    // - 생성할 프리팹들
    [Header("스폰할 적 프리팹")][SerializeField] private Enemy[] _enemyPrefabs;

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _spawnInterval)
        {
            _timer = 0f;

            _spawnInterval = UnityEngine.Random.Range(1f, 3f); // float : 1~3

            Spawn();
        }
    }

    private void Spawn()
    {
        int enemyPrefabIndex = 0;
        int randomPercent = UnityEngine.Random.Range(0, 100);

        // Todo: Scriptable Object를 사용해서 리팩토링
        // 이유1: 배열을 사용했지만 각 아이템이 어떤 프리팹인지 알 수가 없음.
        // 이유2: 각 에너미 스폰 확률을 매직 넘버로 하드코딩해서 유지보수가 어렵 
        if (randomPercent < 50)
        {
            enemyPrefabIndex = 0;
        }
        else if (randomPercent < 80)
        {
            enemyPrefabIndex = 1;
        }
        else
        {
            enemyPrefabIndex = 2;
        }

        Enemy enemy = Instantiate(_enemyPrefabs[enemyPrefabIndex]);
        enemy.transform.position = transform.position;
    }
}