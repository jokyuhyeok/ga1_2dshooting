using UnityEngine;

public class HomingEnemy : Enemy
{
    // 자주 쓸법한 데이터를 가까운 곳에 저장해두고 쓰는것
    private GameObject _player;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
    }

    protected override void Move()
    {
        // 1. 방향을 구한다.
        Vector2 direction = _player.transform.position - transform.position;
        direction.Normalize();

        // 2. 방향과 속도에 맞게 이동한다.
        transform.Translate(direction * _moveSpeed * Time.deltaTime);
    }
}