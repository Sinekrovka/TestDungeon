using Spine.Unity;
using UnityEngine;

public class Miner : MonoBehaviour, IUnit
{
    [SerializeField] private int _maxDamage;
    [SerializeField] private int _HP;
    [SerializeField] private GameObject _healthBar;
    private HealthIndicator _health;
    private SkeletonAnimation _animationScript;
    private Vector3 startPosition;

    private void Awake()
    {
        _health = GetComponentInChildren<HealthIndicator>();
        Canvas canvas = FindObjectOfType<Canvas>();
        _healthBar = Instantiate(_healthBar, canvas.transform);
        _health = _healthBar.transform.GetComponentInChildren<HealthIndicator>();
        _health.SetStartHp(_HP);
        _health.SetFollowingObject(transform);
        _animationScript = GetComponentInChildren<SkeletonAnimation>();
        startPosition = transform.position;
    }
    
    private void OnMouseDown()
    {
        if (transform.CompareTag("Player"))
        {
            ActionController.Instance.SetTarget(gameObject);
        }
        else
        {
            ActionController.Instance.SetAim(gameObject);
        }
    }
    
    public void Attack(IUnit aim)
    {
        aim.Damage(Random.Range(0, _maxDamage));
        _animationScript.AnimationName = "PickaxeCharge";
    }

    public void Damage(int damage)
    {
        if (damage.Equals(0))
        {
            /*уклонение*/
        }
        else
        {
            _HP -= damage;
            if (_HP <= 0)
            {
                Death();
                _HP = 0;
            }
            else
            {
                _animationScript.AnimationName = "Damage";
            }
            _health.GiveDamage(_HP);
        }
        
        
    }

    private void Death()
    {
        transform.tag = "DeadBody";
        _animationScript.AnimationName = "Pull";
    }
}
