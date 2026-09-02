using UnityEngine;

// 목적: 키보드 입력에 따라서 플레이어 이동 처리를 하고 싶다. 
public class PlayerMove : MonoBehaviour
{
    public float Speed;                 // 플레이어 속도
    public float Speed_ChangeAmount;    // 속도 변화 정도
    
    // 매 프레임마다 실행
    // 초당 프레임 실행 횟수 : 별다른 설정 없을 경우 가능한 많이 실행
    private void Update()
    {
        // 1. 키보드 입력을 받는다.
        float h = Input.GetAxisRaw("Horizontal");  // 키보드 왼/오른쪽 입력 상태에 따라 -1f ~ 0 ~ 1f
        float v = Input.GetAxisRaw("Vertical");    // 키보드 위/아래 입력 상태에 따라 -1f ~ 0 ~ 1f
        
        Debug.Log($"h:{h}, v:{v}");
        
        // 2. 키보드 입력에 따라 방향을 구한다.
        Vector2 direction = new Vector2(h, v);
        
        // 3. 방향과 속력에 따라 이동한다.
        Vector2 normalizedSpeed = (direction * Speed).normalized; 
        transform.Translate(translation: direction * Speed * Time.deltaTime);

        // 1. 일정 영역 안에서만 캐릭터가 이동할 수 있게 한다. 
        if (transform.position.y > 0)
        {
            transform.position = new Vector2(transform.position.x, 0);
        }
        if (transform.position.y < -5)
        {
            transform.position = new Vector2(transform.position.x, -5);
        }
        // 2. 좌우 이동에 있어 한쪽으로 쭉 이동하면 반대쪽에서 나오게 한다.
        if (transform.position.x > 2.5f)
        {
            transform.position = new Vector2(-2.5f, transform.position.y);
        }
        if (transform.position.x < -2.5f)
        {
            transform.position = new Vector2(2.5f, transform.position.y);
        }

    }
}
        // 게임에는 벡터라는 타입이 있다. 벡터는 (크기와 방향을 의미한다)
        // normalized란? : 벡터의 길이를 1로 만들어주는 것 (즉 방향만 유지한다.)
        // 매직 넘버란 : 보는 사람에 따라 의미가 달라질 수 있는 헷갈리는 숫자. 매직넘버는 최대한 없애주자.
        // deltaTime : 이전 프레임으로부터 지금 프레임까지 시간이 얼마나 지났는지 MS로 반환
        // 새로운 위치 = 현재 위치 + (방향 * 속력 * 시간)
        //transform.position = transform.position + (Vector3)direction * Speed * Time.deltaTime;

