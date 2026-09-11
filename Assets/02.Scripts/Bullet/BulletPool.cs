using UnityEngine;

public class BulletPool : MonoBehaviour
{
    // 오브젝트 풀링이란: Pool(웅덩이: 창고)
    // 그 창고안에 게임 오브젝트를 미리 필요한 만큼 만들어두고,
    // 필요할 때마다 꺼내서 사용하고 필요가 없으면 반환하는 식으로  (활성화/비활성화)
    // 메모리 할당과(객체의 생성) 해제(파괴)를 최소화해서 성능 Up!

    // 필요 속성
    [Header("총알 프리팹")]
    [SerializeField] private Bullet _bulletPrefab;

    [Header("풀 사이즈")]
    [SerializeField] private int _poolSize;

    // 생성한 촐알을 담아둘 풀
    private Bullet[] _pool;

    private static BulletPool _instance = null;
    public static BulletPool Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;

        // 창고를 창고 크기만큼 만든다.
        _pool = new Bullet[_poolSize];

        // 창고 크기 만큼 총알을 미리 만들어서 집어 넣는다.
        for (int i = 0; i < _poolSize; i++)
        {
            Bullet bullet = Instantiate(_bulletPrefab, gameObject.transform);
            bullet.gameObject.SetActive(false); // 당장 사용할거 아니기에 비활성화
            _pool[i] = bullet;
        }
    }

    public Bullet GetBullet()
    {
        foreach (Bullet bullet in _pool)
        {
            // 비활성화 되어있는 (즉, 누가 빌려가지 않은 ) 총알 반환
            if (bullet.gameObject.activeSelf == false)
            {
                bullet.gameObject.SetActive(true);
                bullet.OnSpawn();
                return bullet;
            }
        }

        return null;
    }
}