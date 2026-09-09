using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    private Material _material;

    private float _offsetY = 0f;
    [SerializeField] private float _scrollSpeed = 0.1f;

    private void Awake()
    {
        _material = GetComponent<SpriteRenderer>().material;
    }

    private void Update()
    {
        _offsetY += Time.deltaTime * _scrollSpeed;
        _material.mainTextureOffset = new Vector2(0, _offsetY);
    }
}