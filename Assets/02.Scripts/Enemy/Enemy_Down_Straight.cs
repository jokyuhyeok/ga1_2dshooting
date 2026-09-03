using UnityEngine;

public class Enemy_Down_Straight : Enemy
{
    protected override void Update()
    {
        Vector2 direction = Vector2.down;
        transform.Translate(direction * MoveSpeed * Time.deltaTime);
    }
}