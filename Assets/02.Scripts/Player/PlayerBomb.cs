using UnityEngine;

public class PlayerBomb : MonoBehaviour
{
    [SerializeField] private GameObject _bombPrefab;
    [SerializeField] private Vector2 _bombLocation = new Vector3(0, 1.2f);

    [SerializeField] private float _coolTime = 10f;
    private float _coolTimer = 10f;


    private void Update()
    {
        _coolTimer += Time.deltaTime;

        if (_coolTimer >= _coolTime && Input.GetKeyDown(KeyCode.B))
        {
            _coolTimer = 0f;

            Bomb();
        }
    }


    private void Bomb()
    {
        Debug.Log("폭탄 투하!");
        Instantiate(_bombPrefab, _bombLocation, Quaternion.identity);
    }
}