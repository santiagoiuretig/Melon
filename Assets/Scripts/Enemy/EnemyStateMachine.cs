using UnityEngine;
using System.Collections;

public class EnemyStateMachine : MonoBehaviour {

	private EnemyPatrolState patrolState;
	private EnemyActionState actionState;
	private MonoBehaviour currentState;

	void Awake(){
		patrolState = GetComponent<EnemyPatrolState> ();
		actionState = GetComponent<EnemyActionState> ();
	}

	void Start () {
		//Activo estado inicial.
		ActivateState(patrolState);
	}

	public void ActivateState(MonoBehaviour nextState)
	{
		if(currentState != null) currentState.enabled = false;
		currentState = nextState;
		currentState.enabled = true;
	}
}
