using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitStats), typeof(UnitMovement))]
public class Enemy : Unit
{
    [Header("Movement")] [SerializeField] private float _moveRadius = 10f;
    [SerializeField] private float _minMoveDelay = 4f;
    [SerializeField] private float _maxMoveDelay = 12f;
    [SerializeField] float rewardExp;

    List<Character> enemies = new List<Character>();

    private Vector3 _currDestination;
    private float _changePosTime;

    [Header("Behaviour")] [SerializeField] private bool _isAggresive = false;
    [SerializeField] private float _viewDistance = 5f;

    protected override void Start()
    {
        base.Start();
        _changePosTime = Random.Range(_minMoveDelay, _maxMoveDelay);
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _viewDistance);
    }

    protected override void DamageWithCombat(GameObject user)
    {
        base.DamageWithCombat(user);
        Character character = user.GetComponent<Character>();
        if (character != null && !enemies.Contains(character))
            enemies.Add(character);
    }

    protected override void OnAliveUpdate()
    {
        base.OnAliveUpdate();

        if (_focus == null)
        {
            Wandering(Time.deltaTime);
            if (_isAggresive) FindEnemy();
        }
        else
        {
            float distance = Vector3.Distance(_focus.interactionCenter.position, transform.position);
            if (distance > _viewDistance || !_focus.CanInteract)
                RemoveFocus();
            else if (distance <= _focus.radius && !_focus.Interact(gameObject))
                RemoveFocus();
        }
    }

    protected override void Revive()
    {
        base.Revive();
        transform.position = _startPos;
        if (isServer) _unitMover.MoveToPoint(_startPos);
    }

    protected override void Die()
    {
        base.Die();
        if (isServer)
        {
            for (int i = 0; i < enemies.Count; i++)            
                enemies[i].player.progress.AddExp(rewardExp / enemies.Count);            

            enemies.Clear();
        }
    }

    private void Wandering(float deltaTime)
    {
        _changePosTime -= deltaTime;
        if (_changePosTime > 0) return;

        RandomMove();
        _changePosTime = Random.Range(_minMoveDelay, _maxMoveDelay);
    }

    private void RandomMove()
    {
        _currDestination = Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.up) * new Vector3(_moveRadius, 0, 0) +
                           _startPos;
        _unitMover.MoveToPoint(_currDestination);
    }

    private void FindEnemy()
    {
        var colliders = new Collider[10];
        var foundedCollidersAmount = Physics.OverlapSphereNonAlloc(transform.position, _viewDistance, colliders,
            1 << LayerMask.NameToLayer("Player"));

        for (var i = 0; i < foundedCollidersAmount; i++)
        {
            var intractable = colliders[i].GetComponent<Interactable>();
            if (intractable == null || !intractable.CanInteract) continue;
            SetFocus(intractable);
            break;
        }
    }

    public override bool Interact(GameObject user)
    {
        if (!base.Interact(user)) return false;

        SetFocus(user.GetComponent<Interactable>());
        return true;
    }
}