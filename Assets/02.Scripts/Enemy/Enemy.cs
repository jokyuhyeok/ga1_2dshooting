using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    // private로 원천 차단을 하자. 
    // 유니티가 수정할 수 있는 필드 - SerializeField
    [SerializeField] private int _health = 100;
    [SerializeField] protected float _moveSpeed = 5f;
    [SerializeField] protected int _enemyDamage = 30;
    [SerializeField] private float _dropChance = 30f;

    private Animator _animator;
    private AudioSource _damagedAudioSource;

    [SerializeField] private ItemSpawnDataTableSO _itemDataTable;

    // - 죽을 때 생성할 이펙트 프리팹
    [SerializeField] private GameObject _deathEffectPrefab;

    // 객체가 생성될 때 한 번 실행된다.
    private void Awake()
    {
        // 애니메이터 컴포넌트에 대한 참조를 가져와서 할당한다.
        _animator = GetComponent<Animator>();
        _damagedAudioSource = GetComponent<AudioSource>();
    }

    protected virtual void Update()
    {
        Move();
    }

    protected abstract void Move();

    // 적이 플레이어 오브젝트에 닿았을 때 플레이어의 체력을 깎는다.
    // 플레이어에 닿은 적은 그대로 사라진다.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Player player = other.GetComponent<Player>();
        if (player == null)
        {
            Debug.Log("플레이어가 null입니다.");
            return;
        }

        player.TakeDamage(_enemyDamage);

        Instantiate(_deathEffectPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    private void SpawnItem()
    {
        // Todo: Scriptable Object를 사용해서 리팩토링
        // 이유 1: 배열을 사용했지만 각 아이템이 어떤 프리팹인지 알수가 없음
        // 이유 2: 각 아이템 스폰 확률을 매직 넘버로 하드코딩해서 유지보수가 어렵

        if (Random.Range(0, 100) >= _dropChance) return;

        // item 가중치 랜덤 선택 적용
        // 1. 추정할 수 있는 모든 가중치를 더한다.
        int totalWeight = 0;
        foreach (ItemSpawnData data in _itemDataTable.Datas)
        {
            totalWeight += data.Weight;
        }

        // 2. 전체 가중치 범위에서 랜덤한 정수를 뽑는다.
        int randomWeight = Random.Range(0, totalWeight);

        // 3. 가중치를 누적하면서 선택된 구간을 찾는다.
        int cumulativeWeight = 0;
        foreach (ItemSpawnData data in _itemDataTable.Datas)
        {
            cumulativeWeight += data.Weight;
            if (randomWeight < cumulativeWeight)
            {
                GameObject item = Instantiate(data.ItemPrefab);
                item.transform.position = transform.position;
                break;
            }
        }
    }

    private void SpawnDeathEffect()
    {
        Instantiate(_deathEffectPrefab, transform.position, Quaternion.identity);
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_animator != null)
        {
            _animator.SetTrigger("hit");
        }

        // 총알을 연속으로 맞아도 소리가 끊기지 않고 자연스럽게 겹쳐서 나게 합니다.
        if (_damagedAudioSource != null && _damagedAudioSource.clip != null)
        {
            AudioSource.PlayClipAtPoint(_damagedAudioSource.clip, transform.position);
        }

        if (_health <= 0)
        {
            SpawnDeathEffect();
            SpawnItem();

            ScoreManager.Instance.AddScore(100);

            Destroy(gameObject);
        }
        else
        {
        }
    }
}