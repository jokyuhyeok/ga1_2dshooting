using UnityEngine;

public class Enemy : MonoBehaviour
{
    // privvate로 원천 차단을 하자. 
    // 유니티가 수정할 수 있는 필드 - SerializeField
    [SerializeField] private float _health = 100f;
    [SerializeField] protected float _moveSpeed = 5f;

    protected virtual void Update()
    {
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}