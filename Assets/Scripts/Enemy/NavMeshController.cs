using UnityEngine;
using System.Collections;

public class NavMeshController : MonoBehaviour {
	
	[HideInInspector]
	public Transform player;
	private NavMeshAgent navMeshAgent;

	void Awake(){
		player = GameObject.Find ("Player").transform;
		navMeshAgent = GetComponent<NavMeshAgent> ();
	}

	//Esta funcion actualiza las posiciones de los WayPoints.
	public void UpdateTargetPosition(Vector3 _nextPosition){
		navMeshAgent.destination = _nextPosition;
		navMeshAgent.Resume ();
	}

	//Esta funcion actualiza la posicion del player.
	public void UpdateTargetPosition(){
		if(player != null)
		UpdateTargetPosition (player.transform.position);
	}

	public void StopNavMeshAgent(){
		navMeshAgent.Stop ();
	}

	public bool ArrivedToTargetPosition(){
		return navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !navMeshAgent.pathPending;
	}
}
