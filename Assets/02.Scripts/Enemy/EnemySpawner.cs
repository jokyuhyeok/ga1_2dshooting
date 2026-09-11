using UnityEngine;

// 역할 : 일정 시간마다 적을 생성해주고 싶다.
public class EnemySpawner : MonoBehaviour
{
    // 필요 속성
    // - 타이머
    [Header("스폰 간격")][SerializeField] private float _spawnInterval = 3f;
    private float _timer;

    [SerializeField] private EnemySpawnData[] _spawnDatas;


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
        // Todo: Scriptable Object를 사용해서 리팩토링
        // 이유1: 배열을 사용했지만 각 아이템이 어떤 프리팹인지 알 수가 없음.
        // 이유2: 각 에너미 스폰 확률을 매직 넘버로 하드코딩해서 유지보수가 어렵 

        // 가중치 랜덤 선택
        // 각 아이템에 가중치를 부여하고, 가중치가 클수록 높은 확률로 선택되도록 하는 방식
        // 1. 추정할 수 있는 모든 가중치를 더한다.
        int totalWeight = 0;
        foreach (EnemySpawnData data in _spawnDatas)
        {
            totalWeight += data.Weight;
        }

        // 2. 전체 가중치 범위에서 랜던한 정수를 뽑는다.
        int randomWeight = Random.Range(0, totalWeight);

        // 3. 가중치를 누적하면서 선택된 구간을 찾는다.
        int cumulativeWeight = 0;
        foreach (EnemySpawnData data in _spawnDatas)
        {
            cumulativeWeight += data.Weight; // 누적
            if (randomWeight < cumulativeWeight) // 구간
            {
                GameObject enemy = Instantiate(data.EnemyPrefab);
                enemy.transform.position = transform.position;
                break;
            }
        }
    }
}