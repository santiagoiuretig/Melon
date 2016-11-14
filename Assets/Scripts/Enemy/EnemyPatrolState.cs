using UnityEngine;
using System.Collections;

public class EnemyPatrolState : MonoBehaviour {

	public Transform[] WayPoints;

	private EnemyStateMachine enemyStateMachine;
	private NavMeshController navMeshController;
	private VisionController visionController;
	private EnemyActionState enemyActionState;
	private int nextWayPoint;

	void Awake(){
		enemyStateMachine = GetComponent<EnemyStateMachine> ();
		navMeshController = GetComponent<NavMeshController> ();
		visionController = GetComponent<VisionController> ();
		enemyActionState = GetComponent<EnemyActionState> ();
	}

	void Update(){
		RaycastHit hit;
		if (visionController.CanSeePlayer (out hit)) {
			navMeshController.player = hit.transform;
			enemyStateMachine.ActivateState (enemyActionState);
			return;
		}

		if (navMeshController.ArrivedToTargetPosition ()) {
			nextWayPoint = (nextWayPoint + 1) % WayPoints.Length;
			UpdateTargetWayPoint ();
		}
	}

	void OnEnable(){
		UpdateTargetWayPoint ();
	}

	private void UpdateTargetWayPoint(){
		navMeshController.UpdateTargetPosition (WayPoints [nextWayPoint].position);
	}
}
