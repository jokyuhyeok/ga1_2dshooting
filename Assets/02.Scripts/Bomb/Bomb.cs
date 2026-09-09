using UnityEngine;

public class Bomb : MonoBehaviour
{
    // 유지 시간
    [SerializeField] private float _lifeTime = 3f;
    private float _lifeTimer = 0f;

    private void Update()
    {
        _lifeTimer += Time.deltaTime;

        if (_lifeTimer >= _lifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;

        // todo: out, ref, in 키워드 공부
        if (other.TryGetComponent(out Enemy enemy))
        {
            // int의 최대값
            enemy.TakeDamage(int.MaxValue);
        }
    }
}