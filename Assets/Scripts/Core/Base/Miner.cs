using Spine.Unity;
using UnityEngine;

public class Miner : MonoBehaviour, IUnit
{
    [SerializeField] private int _maxDamage;
    [SerializeField] private int _HP;
    [SerializeField] private GameObject _healthBar;
    private HealthIndicator _health;
    private SkeletonAnimation _animationScript;
    private float startPosition;

    private void Awake()
    {
        _health = GetComponentInChildren<HealthIndicator>();
        Canvas canvas = FindObjectOfType<Canvas>();
        _healthBar = Instantiate(_healthBar, canvas.transform);
        _health = _healthBar.transform.GetComponentInChildren<HealthIndicator>();
        Color color;
        if (transform.CompareTag("Player"))
        {
            color = new Color(0.15f,0.9f, 0.1f);
        }
        else
        {
            color = new Color(0.9f, 0.2f, 0.1f);
        }
        _health.SetStartParams(_HP, color);
        _health.SetFollowingObject(transform);
        _animationScript = GetComponentInChildren<SkeletonAnimation>();
        startPosition = transform.position.x;
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
    
    public void Attack(Transform aim)
    {
        IUnit aimInterface = aim.GetComponent<IUnit>();
        aimInterface.Damage(Random.Range(0, _maxDamage));
        ChangeAnimation("PickaxeCharge");
        StartCoroutine(MovingSelectedWarriors.WaitAnimation(1.6f, transform, 
            aim, startPosition, aimInterface.StartPosition()));
        
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
            }
            else
            {
                ChangeAnimation("Damage");
            }
            _health.GiveDamage(_HP);
        }
        
        
    }

    private void Death()
    {
        _HP = 0;
        transform.tag = "DeadBody";
        ChangeAnimation("Pull");
    }

    public void ChangeAnimation(string animationName)
    {
        _animationScript.AnimationName = animationName;
    }

    public float StartPosition()
    {
        return startPosition;
    }
}
