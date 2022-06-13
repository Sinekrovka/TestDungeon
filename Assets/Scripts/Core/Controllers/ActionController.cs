using System.Collections;
using UnityEngine;

public class ActionController : MonoBehaviour
{
    [SerializeField] private float timeBetweenSteps;
    private bool _attack;
    public static ActionController Instance;

    private Transform _warrior;
    private Transform _victim;
    private bool botStep;

    private void Awake()
    {
        Instance = this;
        _attack = false;
        StepController.GenerateFirstStep();
        botStep = !StepController.Step;
        FindObjectOfType<UIController>().CheckUIButtonStatus(StepController.Step);
    }

    public void SetAim(GameObject aimWarrior)
    {
        if (_victim != null)
        {
            _victim.GetComponent<IUnit>().DeselectUnit();
        }
        _victim = aimWarrior.transform;
    }

    public void SetTarget(GameObject targetWarrior)
    {
        if (_warrior != null)
        {
            _warrior.GetComponent<IUnit>().DeselectUnit();
        }
        _warrior = targetWarrior.transform;
    }

    public void AttackAction()
    {
        if (StepController.Step)
        {
            CheckSelected();
            botStep = true;
            MovingSelectedWarriors.Moving(_warrior, _victim);
            _warrior = null;
            _victim = null;
        }
    }

    public void Next()
    {
        if (StepController.Step)
        {
            StepController.Step = false;
            StartCoroutine(EnemyStep(0));
        }
    }
    

    private void CheckSelected()
    {
        if (_warrior == null)
        {
            GameObject[] findedObjects = GameObject.FindGameObjectsWithTag("Player");
            int index = Random.Range(0, findedObjects.Length);
            _warrior = findedObjects[index].transform;
        }

        if (_victim == null)
        {
            GameObject[] findedObjects = GameObject.FindGameObjectsWithTag("Enemy");
            int index = Random.Range(0, findedObjects.Length);
            _victim = findedObjects[index].transform;
        }
    }

    private void Update()
    {
        if (!StepController.Step && botStep)
        {
            botStep = false;
            if (_warrior != null)
            {
                _warrior.GetComponent<IUnit>().DeselectUnit();
                _warrior = null;
            }

            if (_victim != null)
            {
                _victim.GetComponent<IUnit>().DeselectUnit();
                _victim = null;
            }
            _warrior = null;
            _victim = null;
            StartCoroutine(EnemyStep(timeBetweenSteps));
        }
    }

    private IEnumerator EnemyStep(float time)
    {
        yield return new WaitForSeconds(time);
        CheckSelected();
        MovingSelectedWarriors.Moving(_victim, _warrior);
        _warrior = null;
        _victim = null;
    }
}
