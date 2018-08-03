using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<TFeed> {

    private State<TFeed> actualState;

    public StateMachine(State<TFeed> initState)
    {
        actualState = initState;
    }
    public void Update()
    {
        var nextState = actualState.Update();
        ChangeState(nextState);

    }
    public void ChangeState(State<TFeed> nextState)
    {
        if(nextState != null) //si hay un estado para realizar
        {
            actualState.Exit(); //termina el estado actual
            actualState.Enter(); //entra en el nextState
            actualState = nextState; 
        }
    }
    public void Feed (TFeed a) //no termino de entender que es
    {
        var nextState = actualState.Feed(a);
        ChangeState(nextState);
    }
}
