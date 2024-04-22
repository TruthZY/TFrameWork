using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectBase;



[System.Serializable]
public class StateMachine
{
    /// <summary>
    /// static map to save each fsm暂时无用
    /// </summary>
    //public static Dictionary<string, StateMachine> stateMachines;
    public string stateMachineName;
    public AIBehaviour AIBehaviour;
    Dictionary<string, StateBase> states;
    Dictionary<string, TransitionBase> transitions;
    string initStateName;
    public string currentStateName;
    bool inSwitchProgress;
    /// <summary>
    /// 暂时无用
    /// </summary>
    public static StateMachine InitStateMachine(string stateName)
    {
        //if (stateMachines == null)
        //{
        //    stateMachines = new Dictionary<string, StateMachine>();
        //}

        //if (stateMachines.ContainsKey(stateName))
        //{
        //    return stateMachines[stateName];
        //}
        //else
        //{
        //    return new StateMachine(stateName);
        //}
        return null;

    }

    public StateMachine(string stateName)
    {

        stateMachineName = stateName;
        states = new Dictionary<string, StateBase>();
        transitions = new Dictionary<string, TransitionBase>();
        initStateName = "";
        currentStateName = "";
        inSwitchProgress = false;
        //stateMachines[stateName] = this;
    }
    public virtual void SetAIBehaviour(AIBehaviour behaviour)
    {
        this.AIBehaviour = behaviour;
    }

    public void AddState(StateBase state)
    {
        states[state.StateName] = state;
    }
    public void AddTransition(TransitionBase trans)
    {
        transitions[trans.transitionName] = trans;
    }
    public string GetCurrentStateName()
    {
        return currentStateName;
    }
    public void SetInitState(string stateName, bool onEnterDelayOneFrame = true)
    {
        if (states.ContainsKey(stateName))
        {
            initStateName = stateName;
            currentStateName = stateName;

            if (onEnterDelayOneFrame)
            {
                //MonoMgr帮助开启协程，目前未添加
                states[initStateName].onEnter();
            }
            else
            {
                states[initStateName].onEnter();
            }
        }
    }
    //协程或多线程更好
    public void SwitchToState(string stateName, bool waitForIEnumaCallbacks = true)
    {
        if (currentStateName == "")
        {
            Debug.LogError("cant switch to" + currentStateName +
                "because the state machine is not initialized yet");
            return;
        }
        string transitionName = TransitionBase.getTransitionName(currentStateName, stateName);
        //if (states.ContainsKey(stateName) && stateName != currentStateName)待定
        if (states.ContainsKey(stateName))
        {
            if (inSwitchProgress) return;
            inSwitchProgress = true;
            StateBase fromState = states[currentStateName];
            fromState.onExit();
            //if (waitForIEnumaCallbacks &&
            //    !fromState.callbackListener.isEmpty())
            if (waitForIEnumaCallbacks &&
                transitions.ContainsKey(transitionName))

            {
                Debug.Log("转换");
                MonoMgr.GetInstance().StartCoroutine(SwitchCoroutine(transitionName, stateName));
            }
            else
            {
                states[stateName].onEnter();
                currentStateName = stateName;
                inSwitchProgress = false;
            }

        }
        else
        {
            Debug.LogError(transitionName + " dont exit");
        }
    }
    IEnumerator SwitchCoroutine(string transitionName, string stateName)
    {
        yield return null;
        TransitionBase curTrans = transitions[transitionName];
        curTrans.onTransition();
        while (curTrans.isTransing)
        {
            yield return null;
        }
        states[stateName].onEnter();
        currentStateName = stateName;
        inSwitchProgress = false;
    }

    public void ResetToInitialState()
    {
        states[currentStateName].onExit();
        currentStateName = initStateName;
        states[currentStateName].onEnter();
    }
    public void OnUpdate()
    {
        if (currentStateName == "") return;
        StateBase curState = states[currentStateName];
        if (curState != null && curState.CanExecuteUpdateLogic())
        {
            curState.onUpdate();
        }
    }

    public void OnEvent(string eventType)
    {
        StateBase curState = states[currentStateName];
        if (curState != null && curState.CanExecuteUpdateLogic())
        {
            curState.onEvent(eventType);
        }
    }


}
public class StateBase
{
    public string StateName;
    protected StateMachine fsm;
    bool canExecuteUpdateLogic;

    public StateBase(string stateName, StateMachine fsm)
    {
        StateName = stateName;
        this.fsm = fsm;
        canExecuteUpdateLogic = true;
    }
    public virtual void onEnter()
    {
        this.canExecuteUpdateLogic = true;
    }
    public bool CanExecuteUpdateLogic() { return this.canExecuteUpdateLogic; }
    public virtual void onUpdate() { }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="eventType">枚举类型，待修改</param>
    public virtual void onEvent(string eventType) { }
    public virtual void onExit()
    {
        this.canExecuteUpdateLogic = false;
    }
}
public class TransitionBase
{
    public string fromStateName;
    public string toStateName;
    public string transitionName;
    StateMachine fsm;
    public bool isTransing = false;

    public TransitionBase(string fromName, string toName, StateMachine fsm)
    {
        fromStateName = fromName;
        toStateName = toName;
        transitionName = getTransitionName(fromName, toName);
        this.fsm = fsm;
        this.fsm.AddTransition(this);//修改

    }
    public static string getTransitionName(string fromName, string toName)
    {
        return fromName + "2" + toName;
    }
    /// <summary>
    /// 需要使用isTransing
    /// </summary>
    /// <returns></returns>
    public virtual IEnumerator Transitioning()
    {
        yield return null;
    }
    public void onTransition()
    {
        isTransing = true;
        MonoMgr.GetInstance().StartCoroutine(Transitioning());
    }


}


public interface AIBehaviour
{
    void Attack();
}