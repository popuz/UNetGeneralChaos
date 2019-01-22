using UnityEngine;

[RequireComponent(typeof(UnitStats), typeof(UnitMovement))]
public class Enemy : Unit
{
    [Header("Movement")] 
    
    [SerializeField] private float _moveRadius = 10f;
    [SerializeField] private float _minMoveDelay = 4f;
    [SerializeField] private float _maxMoveDelay = 12f;

    private Vector3 _currDestination;
    private float _changePosTime;

    [Header("Behaviour")] 
    
    [SerializeField] private bool _isAggresive = false;
    [SerializeField] private float _viewDistance = 5f;

    protected override void Start()
    {
        base.Start();
        _changePosTime = Random.Range(_minMoveDelay, _maxMoveDelay);        
    }
   
    protected override void OnAliveUpdate()
    {
        base.OnAliveUpdate();
        Wandering(Time.deltaTime);
    }
        protected override void Revive()
        {
            base.Revive();
            transform.position = _startPos;
            if(isServer) _unitMover.MoveToPoint(_startPos);
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
                _currDestination = Quaternion.AngleAxis(Random.Range(0f,360f), Vector3.up)*new Vector3(_moveRadius,0,0) + _startPos;
                _unitMover.MoveToPoint(_currDestination);
            }
}
