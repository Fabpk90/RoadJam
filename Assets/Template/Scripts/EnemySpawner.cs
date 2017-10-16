using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour 
{

	public float invocationSpeed = 2f;

	public Transform upMarker;
	public Transform buttomMarker;

	private const float bigScaleSize = 1.3f;

	private float upX;
	private float upY;

	private float buttomX;
	private float buttomY;

	public List<GameObject> enemiesTypes;
	private ArrayList enemiesInstances;

	private bool paused;

	public bool isBossLvl = false;

	void Start () 
	{
		enemiesInstances = new ArrayList ();
	
		upX = upMarker.position.x;
		upY = upMarker.position.y;

		buttomX = buttomMarker.position.x;
		buttomY = buttomMarker.position.y;

		//Spawn des sandwichs en continue
		InvokeRepeating("InvokeEnemySpawn", 1f, invocationSpeed);
	}

	void Update () {
		
	}

	//délai de spawn d'un enemy
	void InvokeEnemySpawn()
	{
		if (!paused)
			Invoke("SpawnEnemy",Random.Range(0f, 1.2f));
	}

	public void DeleteAllEnemies()
	{
		foreach (GameObject e in enemiesInstances)
			Destroy (e);
		enemiesInstances.Clear ();
	}

	void SpawnEnemy()
	{
		int n = Random.Range (0, enemiesTypes.Count - 1);
		GameObject enemy = enemiesTypes [n];

		//spawn au dessus de l'écran sur une longueur aléatoire
		Vector3 spawnPosition = new Vector3(Random.Range(upX, buttomX), Random.Range(upY, buttomY), enemy.transform.position.z);
		GameObject obj = Instantiate (enemy, spawnPosition, enemy.transform.rotation);
		obj.GetComponent<Enemy> ().isBossLvl = isBossLvl;
		enemiesInstances.Add (obj);
	}

	public void SetPaused(bool p)
	{
		paused = p;
	}
}
