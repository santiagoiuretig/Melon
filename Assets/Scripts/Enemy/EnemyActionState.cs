using UnityEngine;
using System.Collections;

public class EnemyActionState : MonoBehaviour {

	private EnemyStateMachine enemyStateMachine;
	private NavMeshController navMeshController;
	private VisionController visionController;
	private EnemyPatrolState enemyPatrolState;

	void Awake(){
		enemyStateMachine = GetComponent<EnemyStateMachine> ();
		navMeshController = GetComponent<NavMeshController> ();
		visionController = GetComponent<VisionController> ();
		enemyPatrolState = GetComponent<EnemyPatrolState> ();
	}

	void Update(){
		RaycastHit hit;
		if (!visionController.CanSeePlayer (out hit, true)) {
			enemyStateMachine.ActivateState (enemyPatrolState);
			return;
		} else if (visionController.CanSeePlayer (out hit, true)) {
			navMeshController.StopNavMeshAgent ();
			Debug.Log ("Shoot");
		}
		navMeshController.UpdateTargetPosition ();
	}
}
