using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour 
{
	public Vector2 speed = new Vector2(10f,10f);
	public Vector2 direction = new Vector2(-1,0);
	private Vector2 movement;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		movement = new Vector2 (
			speed.x * direction.x,
			speed.y * direction.y
		);
		//float newX = transform.position.x + movement.x;
		//float newY = transform.position.y + movement.y;

		Vector3 position = transform.position;
		position.x += movement.x;
		position.y += movement.y;

		transform.position = position;
	}

}
