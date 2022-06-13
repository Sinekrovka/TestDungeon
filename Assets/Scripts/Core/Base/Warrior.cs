using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : MonoBehaviour
{
    [SerializeField] private GameObject _healthBar;
    
    private HealthIndicator _health;
    private Transform _selectIndicator;
    
    protected void HealthIndicatorInit(int HP)
    {
        _selectIndicator = transform.Find("SelectIndicator");
        HideSelected();
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
        _health.SetStartParams(HP, color);
        _health.SetFollowingObject(transform);
    }
    
    protected void GetDamage(int HP)
    {
        _health.GiveDamage(HP);
    }

    protected void ShowSelected()
    {
        _selectIndicator.gameObject.SetActive(true);
    }

    protected void HideSelected()
    {
        _selectIndicator.gameObject.SetActive(false);    
    }

    protected void ShowDamage(string damageString)
    {
        Debug.Log("Damage: "+damageString);
    }

    protected void DeadBody()
    {
        _healthBar.SetActive(false);
    }
}
