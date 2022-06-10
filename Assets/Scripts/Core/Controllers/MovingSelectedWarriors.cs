using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class MovingSelectedWarriors: MonoBehaviour
{
    private const float _point = 0.1f;
    public static Action<bool> endBattle; 
    
    public static void Moving(Transform warrior, Transform victim)
    {
        endBattle?.Invoke(StepController.Step);
        IUnit warriorUnit = warrior.GetComponent<IUnit>();
        int index = 1;
        if (victim.position.x < 0)
        {
            index = -1;
        }
        victim.DOMoveX(_point*index, 0.5f);
        warrior.DOMoveX(_point*index*-1, 0.5f).OnComplete(delegate { warriorUnit.Attack(victim); });
    }

    public static IEnumerator WaitAnimation(float time, Transform warrior, Transform victim, 
        float warriorStartPoint, float victimStartPoint)
    {
        IUnit warriorUnit = warrior.GetComponent<IUnit>();
        IUnit victimUnit = victim.GetComponent<IUnit>();
        yield return new WaitForSeconds(time);
        warriorUnit.ChangeAnimation("Idle");
        victimUnit.ChangeAnimation("Idle");
        warrior.DOMoveX(warriorStartPoint, 0.5f);
        victim.DOMoveX(victimStartPoint, 0.5f).OnComplete(delegate
        {
            StepController.Step = !StepController.Step; 
            endBattle?.Invoke(StepController.Step);
        });
    }
}