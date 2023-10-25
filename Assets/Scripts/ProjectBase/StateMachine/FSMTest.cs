using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FSMTest : MonoBehaviour
{
    public UnityAction actions;
    [SerializeField]
    public TestFSM FSM;
    void Start()
    {
        FSM = new TestFSM("1");

    }

    // Update is called once per frame
    void Update()
    {
        FSM.OnUpdate();
    }
}
[System.Serializable]
public class TestFSM : StateMachine
{
    public TestFSM(string name) : base(name) {
        StandState stand = new StandState("Stand",this);
        RunState run = new RunState("Run", this);
        this.AddState(stand);
        this.AddState(run);
        this.SetInitState("Stand");
    }
}


public class StandState : StateBase
{
    float Timer = 0;
    float CD = 2;
    public StandState(string stateName, StateMachine fsm) : base(stateName, fsm) { }
    public override void onEnter()
    {
        base.onEnter();
        Debug.Log("StartStand");
    }
    public override void onUpdate()
    {
        base.onUpdate();
        Debug.Log("Standng");
        Timer += Time.deltaTime;
        if (Timer >= CD)
        {
            Timer = 0;
            fsm.SwitchToState("Run");
            
        }
    }
    public override void onExit()
    {
        base.onExit();
        Debug.Log("StandExit");
    }
}
public class RunState : StateBase
{
    float Timer = 0;
    float CD = 2;
    public RunState(string stateName, StateMachine fsm) : base(stateName, fsm) { }

    public override void onEnter()
    {
        base.onEnter();
        Debug.Log("StartRun");
    }
    public override void onUpdate()
    {
        base.onUpdate();
        Debug.Log("Running");
        Timer += Time.deltaTime;
        if (Timer >= CD)
        {
            Timer = 0;
            fsm.SwitchToState("Stand");

        }
    }
    public override void onExit()
    {
        base.onExit();
        Debug.Log("RunExit");
    }

}
/// <summary>
/// 暂时无用
/// </summary>
public class Run2WalkTransition : TransitionBase
{
    public Run2WalkTransition(string from, string to, StateMachine fsm) : base(from, to, fsm){ }

    public override void onTransition()
    {
        base.onTransition();

    }
}

