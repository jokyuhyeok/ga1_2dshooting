using UnityEngine;

public class Enemy_Follow_Player : Enemy
{
    private Transform _playerTransform;

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            _playerTransform = player.transform;
        }
    }

    protected override void Update()
    {
        if (_playerTransform != null)
        {
            Vector2 direction = _playerTransform.position - transform.position;
            transform.Translate(direction * MoveSpeed * Time.deltaTime);
        }
    }
}