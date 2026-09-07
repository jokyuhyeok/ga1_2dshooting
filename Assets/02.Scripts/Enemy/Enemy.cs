using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    // privvate로 원천 차단을 하자. 
    // 유니티가 수정할 수 있는 필드 - SerializeField
    [SerializeField] private int _health = 100;
    [SerializeField] protected float _moveSpeed = 5f;
    [SerializeField] protected int _enemyDamage = 30;

    private Animator _animator;

    // - 생성할 아이템 프리팹들
    [SerializeField] private Item[] _itemPrefabs;

    // 객체가 생성될 때 한 번 실행된다.
    private void Awake()
    {
        // 애니메이터 컴포넌트에 대한 참조를 가져와서 할당한다.
        _animator = GetComponent<Animator>();
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


        Destroy(gameObject);
    }

    private void SpawnItem()
    {
        if (Random.Range(0, 100) > 30) return;

        // Todo: Scriptable Object를 사용해서 리팩토링
        // 이유 1: 배열을 사용했지만 각 아이템이 어떤 프리팹인지 알수가 없음
        // 이유 2: 각 아이템 스폰 확률을 매직 넘버로 하드코딩해서 유지보수가 어렵
        Instantiate(_itemPrefabs[Random.Range(0, _itemPrefabs.Length)], transform.position, transform.rotation);
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_animator != null)
        {
            _animator.SetTrigger("hit");
        }

        if (_health <= 0)
        {
            Destroy(gameObject);
            SpawnItem();
        }
    }
}