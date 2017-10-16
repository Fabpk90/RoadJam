using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

	public float speed;
	public int damage;

	public bool isEnemy;

	void Start()
	{
		Destroy (this, 10f);
	}

	// Update is called once per frame
	void Update () {
		transform.Translate ((Time.deltaTime * speed), 0.0f, 0.0f);
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		

	}
}
