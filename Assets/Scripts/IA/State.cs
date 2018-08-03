using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State<TFeed> {

    private Dictionary<TFeed, State<TFeed>> transitions = new Dictionary<TFeed, State<TFeed>>();

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual State<TFeed> Update() { return null; } //

    public void AddTransition(TFeed key, State<TFeed> state)
    {
        transitions[key] = state;
    }

    public State<TFeed> Feed(TFeed key) 
    {
        if (transitions.ContainsKey(key)) return transitions[key];
        else return null;
    }
}
