using UnityEngine;

// 목적: 키보드 입력에 따라서 플레이어 이동 처리를 하고 싶다. 
public class PlayerMove : MonoBehaviour
{
    public float Speed; // 플레이어 속도
    public float Speed_ChangeAmount; // 속도 변화 정도

    // 코드 최적화 1 - 매직넘버 없애기: 화면 경계값 변수들
    public float MaxY = 0f;
    public float MinY = -5f;
    public float BoundX = 2.5f;

    // 매 프레임마다 실행
    // 초당 프레임 실행 횟수 : 별다른 설정 없을 경우 가능한 많이 실행
    private void Update()
    {
        Move();
        SpeedChange();
    }

    private void SpeedChange()
    {
        // 3. 키보드 E : 스피드 UP!, 키보드 Q : 스피드 Down!
        // 코드최적화 3 - 버튼을 눌렀을 때 최초 한프레임에만 반응하도록 GetKey => GetKeyDown
        if (Input.GetKeyDown(KeyCode.E))
        {
            Speed += Speed_ChangeAmount;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Speed -= Speed_ChangeAmount;
            if (Speed < 0) // Speed가 0이면 더 내려가지 않는다.
            {
                Speed = 0;
            }
        }
    }

    private void Move()
    {
        // 1. 키보드 입력을 받는다.
        float h = Input.GetAxisRaw("Horizontal"); // 키보드 왼/오른쪽 입력 상태에 따라 -1f ~ 0 ~ 1f
        float v = Input.GetAxisRaw("Vertical"); // 키보드 위/아래 입력 상태에 따라 -1f ~ 0 ~ 1f

        Debug.Log($"h:{h}, v:{v}");

        // 2. 키보드 입력에 따라 방향을 구한다.
        Vector2 direction = new Vector2(h, v).normalized;

        // 3. 방향과 속력에 따라 이동한다.
        transform.Translate(direction * Speed * Time.deltaTime);

        // 1. 일정 영역 안에서만 캐릭터가 이동할 수 있게 한다.
        // 코드 최적화 2 - if문 다이어트 : Mathf.Clamp(현재값, 최소값, 최대값) 함수 사용
        float clampedY = Mathf.Clamp(transform.position.y, MinY, MaxY);
        transform.position = new Vector2(transform.position.x, clampedY);

        // 2. 좌우 이동에 있어 한쪽으로 쭉 이동하면 반대쪽에서 나오게 한다.
        if (transform.position.x > BoundX)
        {
            transform.position = new Vector2(-BoundX, transform.position.y);
        }

        if (transform.position.x < -BoundX)
        {
            transform.position = new Vector2(BoundX, transform.position.y);
        }
    }
}
// 게임에는 벡터라는 타입이 있다. 벡터는 (크기와 방향을 의미한다)
// normalized란? : 벡터의 길이를 1로 만들어주는 것 (즉 방향만 유지한다.)
// 매직 넘버란 : 보는 사람에 따라 의미가 달라질 수 있는 헷갈리는 숫자. 매직넘버는 최대한 없애주자.
// deltaTime : 이전 프레임으로부터 지금 프레임까지 시간이 얼마나 지났는지 MS로 반환
// 새로운 위치 = 현재 위치 + (방향 * 속력 * 시간)
//transform.position = transform.position + (Vector3)direction * Speed * Time.deltaTime;