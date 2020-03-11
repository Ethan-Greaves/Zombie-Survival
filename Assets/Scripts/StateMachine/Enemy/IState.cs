using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState 
{
    //Will be called when the state is first changed
    void Enter();
    //Called in the update loop continuously
    void Execute();
    //Called when another state is called, replacing the current state.
    void Exit();

    //Applies animation. Called in Enter method
    void BeginAnimation();

}
