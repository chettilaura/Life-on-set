using Cinemachine.Examples;
using System;
using System.Collections;
using System.Collections.Generic;
//using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;
using Random = UnityEngine.Random;

public class extrasMovement : MonoBehaviour
{
    internal NavMeshAgent _navMeshAgent;
    [SerializeField] private Collider _groundCollider;
    [SerializeField] private GameObject _player;
    public Animator _animator;
    public Animator PlayerAnimator;
    [SerializeField] internal bool _extra;
    private NpcInteractable _npc;
    [SerializeField] internal GameObject _comparsaSbagliata;
    [SerializeField] internal GameObject _troppeComparse;
    internal GameObject _dialogueBoxClone;


    private FiniteStateMachine<extrasMovement> _stateMachine;
    public float _stoppingDistance = 1f;
    private float _speed = 2;
    [Range(1, 500)] public float walkRadius;
    internal bool _isNear;
    internal bool _follow=false;
    private float _chosenStoppingDistance = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _navMeshAgent = this.GetOrAddComponent<NavMeshAgent>();
        if(_navMeshAgent != null)
        {
            _navMeshAgent.speed = _speed;
        }
        _stateMachine = new FiniteStateMachine<extrasMovement>(this);

        //STATES
        State walkingState = new WalkingState("Walk", this);
        State followState = new FollowState("Follow", this);
        State stopState = new StopState("Stop", this);

 


        //TRANSITIONS
        _stateMachine.AddTransition(walkingState, stopState, () => _isNear && QuestManager.questManager.questList[1].progress == Quest.QuestProgress.ACCEPTED);
        _stateMachine.AddTransition(stopState, walkingState, () => !_isNear);
        _stateMachine.AddTransition(stopState, followState, () => Input.GetKeyDown(KeyCode.E) && _extra && QuestManager.questManager.currentQuest.questObjectiveCount< QuestManager.questManager.currentQuest.questObjectiveRequirement && QuestManager.questManager.questList[1].progress == Quest.QuestProgress.ACCEPTED );
        _stateMachine.AddTransition(followState, stopState, () => QuestManager.questManager.questList[1].progress == Quest.QuestProgress.DONE );

        //START STATE
        _stateMachine.SetState(walkingState);

    }

    void Update()
    {
        if (!QuestManager.questManager.RequestFinishedQuest(1))
        {
            _stateMachine.Tik();
        }
    } 


    public void StopAgent(bool stop) => _navMeshAgent.isStopped = stop;

    public void FollowPlayer()
    {
        _navMeshAgent.SetDestination(_player.transform.position);
        _navMeshAgent.speed = _player.GetComponent<Rigidbody>().velocity.magnitude;
        if(_navMeshAgent.remainingDistance < (_stoppingDistance - 0.3f))
        {
            ChangeAnimation(true);
            StopAgent(true);
        } else
        {
            ChangeAnimation(false);
            StopAgent(false);
        }
    }

    public void SetDestination()
    {
       NavMeshPath path = new NavMeshPath();
       Vector3 RandomPosition = GetRandomPositionOnGround();
        _navMeshAgent.SetDestination(RandomPosition);
    }

    public void Talk()
    {
        Vector3 targetDirection = _player.transform.position - transform.position;
        targetDirection.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 150f * Time.deltaTime);
        ChangeAnimation(true);

    }

    public bool IsTargetWithinDistance(float distance)
    {
        return (_player.transform.position - transform.position).sqrMagnitude <= distance * distance;
    }

    public void ChangeAnimation(bool _isNear)
    {
        if (_isNear)
        {
            _animator.SetBool("isNear", true);

        }
        else
        {
            _animator.SetBool("isNear", false);
        }

    }

    public Vector3 GetRandomPositionOnGround()
    {
        Vector3 min = _groundCollider.bounds.min;
        Vector3 max = _groundCollider.bounds.max;
        return new Vector3(Random.Range(min.x, max.x), 2f, Random.Range(min.z, max.z));
    }
}




public class StopState : State
{
    private extrasMovement _extra;
    public StopState(string name, extrasMovement extra) : base(name)
    {
        _extra = extra;
    }

    public override void Enter()
    {
        _extra.StopAgent(true);
    }

    public override void Tik()
    {
        _extra._isNear = _extra.IsTargetWithinDistance(_extra._stoppingDistance);
        _extra.Talk();
        if (Input.GetKeyDown(KeyCode.E)){
            if (!_extra._extra)
            {
                _extra._dialogueBoxClone = (GameObject)GameObject.Instantiate(_extra._comparsaSbagliata, _extra.transform.localPosition, Quaternion.identity);
            } else if (_extra._extra && QuestManager.questManager.currentQuest.questObjectiveCount == QuestManager.questManager.currentQuest.questObjectiveRequirement)
            {
                _extra._dialogueBoxClone = (GameObject)GameObject.Instantiate(_extra._troppeComparse, _extra.transform.localPosition, Quaternion.identity);
            } 
        }
    }

    public override void Exit()
    {
    }
}

public class WalkingState: State
{
    private extrasMovement _extra;
    public WalkingState(string name, extrasMovement extra) : base(name)
    {
        _extra = extra;
    }

    public override void Enter()
    {
        _extra.StopAgent(false);
        _extra.SetDestination();
        _extra.ChangeAnimation(_extra._isNear);
    }

    public override void Tik()
    {
        if (QuestManager.questManager.questList[1].progress == Quest.QuestProgress.ACCEPTED)
            _extra._isNear = _extra.IsTargetWithinDistance(_extra._stoppingDistance);
        else
            _extra._isNear = false;
        if ((_extra._navMeshAgent.remainingDistance <= _extra._navMeshAgent.stoppingDistance))
        {
            _extra.ChangeAnimation(_extra._isNear);
            _extra.SetDestination();
        }
        else
        {
            _extra.ChangeAnimation(_extra._isNear);
        }
       // _extra.SetDestination();
    }

    public override void Exit()
    {
    }
}

public class FollowState : State
{
    private extrasMovement _extra;
    public FollowState(string name, extrasMovement extra) : base(name)
    {
        _extra = extra;
    }

    public override void Enter()
    {
        //_extra.StopAgent(false);
        QuestManager.questManager.currentQuest.questObjectiveCount++;
    }

    public override void Tik()
    {
        //_extra.ChangeAnimation(false);
        _extra.FollowPlayer();
        if (_extra.PlayerAnimator.GetBool("isSprinting"))
        {
            _extra._animator.SetBool("sprinting", true);
        }
        else
        {
            _extra._animator.SetBool("sprinting", false);
        }
        if(QuestManager.questManager.currentQuest.id ==1 && QuestManager.questManager.currentQuest.questObjectiveCount >= QuestManager.questManager.currentQuest.questObjectiveRequirement)
        {
            QuestManager.questManager.currentQuest.progress = Quest.QuestProgress.COMPLETE;
            Debug.Log("Completed");
        }
    }

    public override void Exit()
    {
    }
}