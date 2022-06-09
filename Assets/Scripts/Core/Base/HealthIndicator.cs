using DG.Tweening;
using UnityEngine;

public class HealthIndicator:MonoBehaviour
{
    private int _startHP;
    private float _lenght;
    private Transform _following;
    private RectTransform _baseObject;
    private RectTransform _healtBar;
    private Camera _mainCamera;

    private void Awake()
    {
        _healtBar = GetComponent<RectTransform>();
        _baseObject = _healtBar.parent.GetComponent<RectTransform>();
        _lenght = _healtBar.sizeDelta.x;
        _mainCamera = Camera.main;
    }

    public void SetStartHp(int hp)
    {
        _startHP = hp;
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
        _healtBar.DOSizeDelta(new Vector2(_lenght * (damage / _startHP), _healtBar.sizeDelta.y), 0.5f);
    }
}