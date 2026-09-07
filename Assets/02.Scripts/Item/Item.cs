using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemType _type;
    [SerializeField] private float _value;

    private const float WaitTime = 2.0f;
    private float _waitTimer = 0f;
    private const float MoveSpeed = 5f;

    private Player _player = null;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();

        if (_player == null)
        {
            Debug.LogWarning("플레이어를 찾을 수 없습니다.");
            return;
        }
    }

    private void Update()
    {
        _waitTimer += Time.deltaTime;
        if (_waitTimer >= WaitTime)
        {
            FollowPlayer();
        }
    }

    private void FollowPlayer()
    {
        if (_player == null) return;

        Vector2 direction = _player.transform.position - transform.position;
        direction.Normalize();
        transform.Translate(direction * MoveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Player player = other.GetComponent<Player>();
        if (player == null)
        {
            Debug.LogWarning("플레이어 태그 오브젝트에 플레이어 컴포넌트가 없습니다.");
            return;
        }

        switch (_type)
        {
            // 심화 과제 1. 퍼사드 패턴 (패턴: 객체지향에서 자주 일어나는 설계 문제를 잘 풀어내도록 경험에 의해 정리해 놓은 공식같은거)
            case ItemType.Heal:
                {
                    player.Heal((int)(_value));
                    break;
                }

            case ItemType.MoveSpeedUp:
                {
                    // 캡슐화 : 
                    // + 데이터 은닉(Speed 속성 private 처리) 
                    // + 행위를 통한 상태 변경 (SpeedUp 호출)
                    player.GetComponent<PlayerMove>().SpeedUp(_value);
                    break;
                }

            case ItemType.FireRateUp:
                {
                    player.GetComponent<PlayerFire>().FireRateUp(_value);
                    break;
                }
        }

        Destroy(gameObject);
    }
}