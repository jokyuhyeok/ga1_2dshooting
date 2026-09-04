using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // 목적 : 총알을 위로 움직이고 싶다. 
    public float MoveSpeed;
    public float BulletDamage;

    private void Update()
    {
        Vector2 direction = Vector2.up; // new Vector2(1,0);
        transform.Translate(direction * MoveSpeed * Time.deltaTime);
    }

    // 트리거 관련 이벤트
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("충돌 했다!");
        // 나죽고!
        Destroy(this.gameObject);

        // 충돌한 친구가 Enemy일 때만 죽여뿌자.
        if (other.gameObject.CompareTag("Enemy"))
        {
            // GetComponent<타입>() -> 게임 오브젝트가 가지고 있는 컴포넌트를 참조
            Enemy enemy = other.gameObject.GetComponent<Enemy>();

            // 응집도는 높이고, 결합도는 낮춰라
            // 결합도란 묻는거.. 매번 묻는거..
            // 이 데이터를 다루는 로직은 Enemy에 있는것이 좋은 코드
            enemy.TakeDamage(BulletDamage);
        }

        // 충돌 관련 이벤트 (Enter -> Stay -> Exit)
        // 충돌이 시작되면 호출되는 이벤트함수
        //private void OnCollisionEnter2D(Collision2D collision)
        //{
        //    Debug.Log("충돌 했다!");
        // 나죽고!
        //    Destroy(this.gameObject);

        // 충돌한 친구가 Enemy일 때만 죽여뿌자.
        //    if (collision.gameObject.gameObject.CompareTag("Enemy"))
        //    {
        // GetComponent<타입>() -> 게임 오브젝트가 가지고 있는 컴포넌트를 참조
        //        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        //        enemy._health -= BulletDamage;

        // 응집도는 높이고, 결합도는 낮춰라
        // 결합도란 묻는거.. 매번 묻는거..
        // 이 데이터를 다루는 로직은 Enemy에 있는것이 좋은 코드
        //        enemy.TakeDamage(BulletDamage);
        //    }
        //}

        //private void OnCollisionStay2D(Collision2D collision)
        //{
        //Debug.Log("충돌 중이다..!");
        //}

        //private void OnCollisionExit2D(Collision2D collision)
        //{
        //Debug.Log("충돌이 끝났다!");
        //}
    }
}