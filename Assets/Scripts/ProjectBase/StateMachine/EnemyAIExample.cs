using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIExample : MonoBehaviour,AIBehaviour
{
    public NavMeshAgent agent;
    public Transform Target;
    public float ChaseRadius = 10;
    public float AttackRadius = 5;
    public float ChaseSpeed = 3;
    public float PatrolSpeed = 1.5f;
    public float AttackCD = 0.5f;
    public float BulletSpeed = 5;
    public SimpleEnemyFSM SimpleEnemyFSM;
    public Transform[] PatrolPoints;
    private List<Vector3> patrolPos = new List<Vector3>();
    public int patrolIndex = 0;
    void Start()
    {
        SimpleEnemyFSM = new SimpleEnemyFSM("EnemyExample",this);
        //SimpleEnemyFSM.SetAIBehaviour(this);
        InitPatrolPos();
    }

    void Update()
    {
        SimpleEnemyFSM.OnUpdate();
    }
    #region 行为逻辑
    /// <summary>
    /// 初始化巡逻点位
    /// </summary>
    void InitPatrolPos()
    {
        foreach (Transform t in PatrolPoints)
        {
            patrolPos.Add(t.position);
        }

    }
    /// <summary>
    /// 检查目标是否进入追踪范围内
    /// </summary>
    /// <returns></returns>
    public bool ChaseCheck()
    {
        if (Vector3.Distance(Target.position, transform.position) <= ChaseRadius)
        {
            return true;
        }
        else return false;

    }
    /// <summary>
    /// 检查目标是否进入攻击范围
    /// </summary>
    /// <returns></returns>
    public bool AttackCheck()
    {
        if (Vector3.Distance(Target.position, transform.position) <= AttackRadius)
        {
            return true;
        }
        else return false;
    }
    /// <summary>
    /// 判断是否到达巡逻点
    /// </summary>
    /// <returns></returns>
    public bool PatrolEnd()
    {
        if (Vector3.Distance(transform.position, patrolPos[patrolIndex]) <= 0.1f)
        {
            return true;
        }
        else return false;
    }

    public void FindTarget()
    {
        agent.SetDestination(Target.position);
    }
    /// <summary>
    /// 停止移动
    /// </summary>
    public void StopMove()
    {
        agent.isStopped = true;
    }
    /// <summary>
    /// 继续移动
    /// </summary>
    public void Move()
    {
        agent.isStopped = false;
    }
    /// <summary>
    /// 设置目标
    /// </summary>
    /// <param name="pos"></param>
    public void SetTarget(Vector3 pos)
    {
        agent.SetDestination(pos);
    }
    /// <summary>
    /// 设置速度
    /// </summary>
    /// <param name="speed"></param>
    public void SetSpeed(float speed)
    {
        agent.speed = speed;
    }
    /// <summary>
    /// 设置巡逻点
    /// </summary>
    public void SetPatrolPos() {
        patrolIndex++;
        if (patrolIndex >= patrolPos.Count) patrolIndex = 0;
        agent.SetDestination(patrolPos[patrolIndex]);

    }


    /// <summary>
    /// 对象池射出子弹
    /// </summary>
    public void Attack()
    {
        Debug.Log("攻击");
        PoolMgr.GetInstance().GetObj("Bullet/SampleBullet", (bullet) =>
        {
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = (Target.position - transform.position).normalized * BulletSpeed;
            bullet.transform.position = transform.position;
        });
    }
    #endregion

}
[System.Serializable]
public class SimpleEnemyFSM : StateMachine
{
    public EnemyAIExample EnemyAIExample;
    public SimpleEnemyFSM(string stateName,AIBehaviour behaviour) : base(stateName)
    {
        SetAIBehaviour(behaviour);
        IdleState idle = new IdleState("Idle", this);
        PatrolState patrol = new PatrolState("Patrol", this);
        ChaseState chase = new ChaseState("Chase", this);
        AttackState attack = new AttackState("Attack", this);
        Attack2AttackTransition ReAttack = new Attack2AttackTransition("Attack", "Attack", this);
        this.AddState(idle);
        this.AddState(patrol);
        this.AddState(chase);
        this.AddState(attack);
        this.AddTransition(ReAttack);
        this.SetInitState("Idle");
    }
    public override void SetAIBehaviour(AIBehaviour behaviour)
    {
        base.SetAIBehaviour(behaviour);
        EnemyAIExample = behaviour as EnemyAIExample;
    }
}
public class IdleState : StateBase
{
    SimpleEnemyFSM FSM;
    EnemyAIExample EnemyAIExample;
    float Timer = 0;
    float CD = 2;


    public IdleState(string stateName, StateMachine fsm) : base(stateName, fsm)
    {
        FSM = fsm as SimpleEnemyFSM;
        EnemyAIExample = FSM.EnemyAIExample;
    }

    public override void onEnter()
    {
        base.onEnter();
        EnemyAIExample.StopMove();
        Timer = 0;
    }

    public override void onEvent(string eventType)
    {
        base.onEvent(eventType);
    }

    public override void onExit()
    {
        base.onExit();
        Timer = 0;
    }

    public override void onUpdate()
    {
        base.onUpdate();

        if (EnemyAIExample.AttackCheck())
        {
            fsm.SwitchToState("Attack");
            return;
        }
        if (EnemyAIExample.ChaseCheck())
        {
            fsm.SwitchToState("Chase");
            return;
        }
        Timer += Time.deltaTime;
        
        if (Timer>=CD)
        {
            fsm.SwitchToState("Patrol");
            return;
        }
        



    }
}
public class PatrolState : StateBase
{
    SimpleEnemyFSM FSM;
    EnemyAIExample EnemyAIExample;
    public PatrolState(string stateName, StateMachine fsm) : base(stateName, fsm)
    {
        FSM = fsm as SimpleEnemyFSM;
        EnemyAIExample = FSM.EnemyAIExample;
    }

    public override void onEnter()
    {
        base.onEnter();
        EnemyAIExample.SetPatrolPos();
        EnemyAIExample.SetSpeed(EnemyAIExample.PatrolSpeed);
        EnemyAIExample.Move();

    }

    public override void onEvent(string eventType)
    {
        base.onEvent(eventType);
    }

    public override void onExit()
    {
        base.onExit();
    }

    public override void onUpdate()
    {
        base.onUpdate();
        if (EnemyAIExample.AttackCheck())
        {
            fsm.SwitchToState("Attack");
            return;
        }
        if (EnemyAIExample.ChaseCheck())
        {
            fsm.SwitchToState("Chase");
            return;
        }
        if (EnemyAIExample.PatrolEnd())
        {
            fsm.SwitchToState("Idle");
            return;
        }
    }
}
public class ChaseState : StateBase
{
    SimpleEnemyFSM FSM;
    EnemyAIExample EnemyAIExample;
    public ChaseState(string stateName, StateMachine fsm) : base(stateName, fsm)
    {
        FSM = fsm as SimpleEnemyFSM;
        EnemyAIExample = FSM.EnemyAIExample;
    }

    public override void onEnter()
    {
        base.onEnter();
        EnemyAIExample.SetSpeed(EnemyAIExample.ChaseSpeed);
        EnemyAIExample.Move();
    }

    public override void onEvent(string eventType)
    {
        base.onEvent(eventType);
    }

    public override void onExit()
    {
        base.onExit();

    }

    public override void onUpdate()
    {
        base.onUpdate();
        EnemyAIExample.FindTarget();
        if (EnemyAIExample.AttackCheck())
        {
            fsm.SwitchToState("Attack");
            return;
        }
        if (!EnemyAIExample.ChaseCheck())
        {
            fsm.SwitchToState("Idle");
            return;
        }

    }
}
public class AttackState : StateBase
{
    SimpleEnemyFSM FSM;
    EnemyAIExample EnemyAIExample;
    public AttackState(string stateName, StateMachine fsm) : base(stateName, fsm)
    {
        FSM = fsm as SimpleEnemyFSM;
        EnemyAIExample = FSM.EnemyAIExample;
    }

    public override void onEnter()
    {
        base.onEnter();
        EnemyAIExample.StopMove();
        if (EnemyAIExample.AttackCheck()) {
            EnemyAIExample.Attack();
        }
    }

    public override void onEvent(string eventType)
    {
        base.onEvent(eventType);
    }

    public override void onExit()
    {
        base.onExit();
    }

    public override void onUpdate()
    {
        base.onUpdate();
        if (!EnemyAIExample.AttackCheck())
        {
            if (EnemyAIExample.ChaseCheck())
            {
                fsm.SwitchToState("Chase");
                return;
            }
            else
            {
                fsm.SwitchToState("Idle");
                return;
            }
        }
        fsm.SwitchToState("Attack");
    }
}

public class Attack2AttackTransition : TransitionBase
{
    SimpleEnemyFSM FSM;
    EnemyAIExample EnemyAIExample;
    public Attack2AttackTransition(string from, string to, StateMachine fsm) : base(from, to, fsm) {
        FSM = fsm as SimpleEnemyFSM;
        EnemyAIExample = FSM.EnemyAIExample;
    }

    public override IEnumerator Transitioning()
    {
        Debug.Log("startTrans");
        yield return new WaitForSeconds(EnemyAIExample.AttackCD);
        isTransing = false;
        Debug.Log("EndTrans");
    }
}