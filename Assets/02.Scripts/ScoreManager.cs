using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // 관리: 특정 데이터에 대한 무결성과 생성, 읽기, 수정, 삭제 등과 관련된 로직bes
    private int _bestScore;
    private int _currentScore;

    // UI 책임 추가 (텍스트메시 프로 참조)
    [SerializeField] private TextMeshProUGUI _bestScoreTextUI;
    [SerializeField] private TextMeshProUGUI _currentScoreTextUI;

    public void AddScore(int score)
    {
        _currentScore += score;
        if (_currentScore > _bestScore)
        {
            _bestScore = _currentScore;
        }
    }

    private void Update()
    {
        Refresh();
    }

    private void Refresh()
    {
        _bestScoreTextUI.text = $"Best Score: {_bestScore}";
        _currentScoreTextUI.text = $"Score: {_currentScore}";
    }
}