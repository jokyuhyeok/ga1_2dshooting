using UnityEngine;

public class AimedEnemy : Enemy
{
    private GameObject _player;
    private Vector2 _direction;
    private float _angle;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
        if (_player == null)
        {
            Debug.Log("플레이어 태그를 가진 게임 오브젝트를 찾지 못했습니다.");
            return;
        }

        _direction = _player.transform.position - transform.position;
        _angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, _angle + 90f);
        _direction.Normalize();
    }

    protected override void Move()
    {
        if (_player == null) return;
        // 회전이 적용된 상태이므로, 로컬 좌표계(기본값)가 아닌 
        // 월드 좌표계(Space.World) 기준으로 이동해야 처음 계산한 방향으로 올곧게 날아갑니다.
        transform.Translate(_direction * _moveSpeed * Time.deltaTime, Space.World);
    }
}