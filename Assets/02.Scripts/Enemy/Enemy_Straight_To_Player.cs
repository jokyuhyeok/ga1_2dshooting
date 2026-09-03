using UnityEngine;

public class Enemy_Straight_To_Player : Enemy
{
    private Vector2 _moveDirection;

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            _moveDirection = (player.transform.position - transform.position).normalized;
        }
        else
        {
            _moveDirection = Vector2.down;
        }
    }

    protected override void Update()
    {
        transform.Translate(_moveDirection * _moveSpeed * Time.deltaTime);
    }
}

// 이동공식
// 1. 방향을 구한다.
// 2. 방향과 속도에 맞게 이동한다.