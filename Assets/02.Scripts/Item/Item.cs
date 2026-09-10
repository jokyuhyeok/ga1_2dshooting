using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemType _type;
    [SerializeField] private float _value;
    [SerializeField] private AudioClip _getItem;

    private const float WaitTime = 2.0f;
    private float _waitTimer = 0f;
    private const float MoveSpeed = 5f;

    private Player _player = null;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.Play(_type.ToString());

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
            // 심화 과제 1. 퍼사드 패턴 (패턴: 객체지향에서 자주 일어나는 설계 문제를 잘 풀어내도록 경험에의해 정리해논 공식같은거...)
            // 심화 과제 2. 아이템 종류가 조합에의해 폭발적으로 증가할 경우에는 -> 조합 패턴을 사용해라
            // 포트폴리오에서 가장 중요한게 게임 구현 완성도 (코드의 완성도는 가장 후순위)
            case ItemType.Heal:
                {
                    player.Heal((int)(_value));
                    Debug.Log($"플레이어 체력: {player.Health}");
                    //player._health = 34;
                    break;
                }

            case ItemType.MoveSpeedUp:
                {
                    break;
                    // 캡슐화 : 
                    // + 데이터 은닉(Speed 속성 private 처리) 
                    // + 행위를 통한 상태 변경 (SpeedUp 호출)
                    PlayerMove playerMove = player.GetComponent<PlayerMove>();
                    playerMove.SpeedUp(_value);
                    Debug.Log($"플레이어 이동속도: {playerMove.Speed}");
                    break;
                }

            case ItemType.FireRateUp:
                {
                    PlayerFire playerFire = player.GetComponent<PlayerFire>();
                    playerFire.FireRateUp(_value);
                    Debug.Log($"플레이어 공격속도: {playerFire.CoolDown_time}");
                    break;
                }
        }

        AudioSource playerAudio = player.GetComponent<AudioSource>();
        if (playerAudio != null && _getItem != null)
        {
            playerAudio.PlayOneShot(_getItem);
        }

        Destroy(gameObject);
    }
}