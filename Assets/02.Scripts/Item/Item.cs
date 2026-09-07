using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemType Type;
    public float Value;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Player player = other.GetComponent<Player>();
        if (player == null)
        {
            Debug.LogWarning("플레이어 태그 오브젝트에 플레이어 컴포넌트가 없습니다.");
            return;
        }

        switch (Type)
        {
            case ItemType.Heal:
                {
                    player.Heal((int)(Value));
                    break;
                }

            case ItemType.MoveSpeedUp:
                {
                    // 캡슐화 : 
                    // + 데이터 은닉(Speed 속성 private 처리) 
                    // + 행위를 통한 상태 변경 (SpeedUp 호출)
                    player.GetComponent<PlayerMove>().SpeedUp(Value);
                    break;
                }

            case ItemType.FireRateUp:
                {
                    player.GetComponent<PlayerFire>().FireRateUp(Value);
                    break;
                }
        }

        Destroy(gameObject);
    }
}