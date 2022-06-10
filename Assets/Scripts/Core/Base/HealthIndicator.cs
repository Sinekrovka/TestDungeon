using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HealthIndicator:MonoBehaviour
{
    private int _startHP;
    private Transform _following;
    private RectTransform _baseObject;
    private Image _healtBar;
    private Camera _mainCamera;

    private void Awake()
    {
        _healtBar = GetComponent<Image>();
        _baseObject = _healtBar.transform.parent.GetComponent<RectTransform>();
        _mainCamera = Camera.main;
    }

    public void SetStartParams(int hp, Color color)
    {
        _startHP = hp;
        _healtBar.color = color;
    }

    public void SetFollowingObject(Transform following)
    {
        _following = following;
    }

    private void Update()
    {
        Vector2 position = _mainCamera.WorldToScreenPoint(_following.transform.position + new Vector3(0, 3f, 0));
        _baseObject.position = position;
    }

    public void GiveDamage(int damage)
    {
        _healtBar.DOFillAmount((damage*1f / _startHP), 0.5f);
    }
}