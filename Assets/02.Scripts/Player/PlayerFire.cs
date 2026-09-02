using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    // 목표 : space bar를 누를때마다 총알을 생성해서 발사하고 싶다.
    // 필요속성 : 총알 프리팹
    public GameObject BulletPrefab;
    public GameObject Sub_BulletPrefab;
    
    // 필요속성 2: 생성위치(총구)
    // FirePoint_Left의 Transform을 가져오는 것이다. 네이밍 시 FirePoint_Left_Transform으로 함이 바람직
    public Transform FirePoint_Left;
    public Transform FirePoint_Right;
    public Transform Sub_FirePoint_Left;
    public Transform Sub_FirePoint_Right;
    
    // 쿨타임 변수
    public float CoolDown_time = 5.0f;
    public float Current_time = 0.0f;
    
    // 자동 공격 모드
    private bool isAuto = false;
        
    private void Update()
    {
        // [정리과제 7] '숫자 1' 버튼 누르면 자동모드 시작 
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            isAuto = !isAuto;
        }
        // [정리과제 6] 총알 발사에 있어 쿨타임 적용
        if (Current_time > 0.0f)
        {
            Current_time -= Time.deltaTime;
        }

        if ((isAuto || Input.GetKeyDown(KeyCode.Space)) && Current_time <= 0.0f)
        {
            Fire();
            Current_time = CoolDown_time;
        }
    }

    private void Fire()
    {
        // 1. 스페이스바를 누르면
        if (Input.GetKeyDown(KeyCode.Space) || isAuto)
        {
            // 2. 총알 프리팹을 생성한다.
            // Instantiate는 프리팹을 복사해서 게임 오브젝트를 만들고 씬에 넣어주는 기능
            GameObject bullet1 = Instantiate(BulletPrefab);
            bullet1.transform.position = FirePoint_Left.position; // 생성한 총알의 위치를 총구의 위치로
            
            GameObject bullet2 = Instantiate(BulletPrefab);
            bullet2.transform.position = FirePoint_Right.position;
            // [정리과제 8] 보조 총알 양쪽 발사하기
            GameObject sub_bullet1 = Instantiate(Sub_BulletPrefab);
            sub_bullet1.transform.position = Sub_FirePoint_Left.position;
            
            GameObject sub_bullet2 = Instantiate(Sub_BulletPrefab);
            sub_bullet2.transform.position = Sub_FirePoint_Right.position;

        }
    }
}