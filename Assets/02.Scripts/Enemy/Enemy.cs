using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Health = 100;
    public float MoveSpeed;

    private void Update()
    {
        Vector2 direction = Vector2.down;
        transform.Translate(direction * MoveSpeed * Time.deltaTime);
    }
}