using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _health = 100f;

    public void TakeDamage(float _enemydamage)
    {
        _health -= _enemydamage;
        Debug.Log("으억.. 데미지를 입었다!");
        if (_health <= 0)
        {
            Destroy(gameObject);
            Debug.Log("플레이어 사망!");
        }
    }
}