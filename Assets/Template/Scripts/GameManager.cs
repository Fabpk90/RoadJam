using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject scoreHUD;

	public static GameManager gameManager;

	private void Awake()
	{
		gameManager = this;
	}

	public static GameManager GetInstance()
	{
		return gameManager;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
