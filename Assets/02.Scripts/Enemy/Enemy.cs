using System;
using UnityEngine;
using UnityEngine.Rendering;

public abstract class Enemy : MonoBehaviour
{
    // privvate로 원천 차단을 하자. 
    // 유니티가 수정할 수 있는 필드 - SerializeField
    [SerializeField] private float _health = 100f;
    [SerializeField] protected float _moveSpeed = 5f;
    [SerializeField] protected float _enemyDamage = 30f;

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

    public void TakeDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}