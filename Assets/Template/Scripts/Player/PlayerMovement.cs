using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour {
	
	public GameObject playerSprite;

	public GameObject projectile;

	private float timeStart = 0.0f;

	[Tooltip("Gère le cooldown du tir")]
	public float cooldown = 50.5f;

	public List<AudioClip> listShots;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (CrossPlatformInputManager.GetButtonDown ("Fire") && Time.time - timeStart >= cooldown) 
		{
			timeStart = Time.time;

			GameObject obj = Instantiate (projectile);

			Vector3 pos = transform.position;
			pos.x += 3.0f;

			obj.transform.position = pos;

			AudioSource.PlayClipAtPoint (getRandomClip (), transform.position);
		}
	}

	public AudioClip getRandomClip()
	{
		return listShots[Random.Range(0	, listShots.Count)];
	}

	public void Move(Vector2 movementVec)
	{
		//playerSprite.transform.position += new Vector3(movementVec.x, movementVec.y, 0);
		GetComponent<Rigidbody2D> ().AddForce (movementVec);
	}

}
