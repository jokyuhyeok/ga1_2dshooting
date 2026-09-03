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
        transform.Translate(_moveDirection * MoveSpeed * Time.deltaTime);
    }
}