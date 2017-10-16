using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityStandardAssets.CrossPlatformInput;

public class PlayerControl : MonoBehaviour 
{

	public PlayerMovement playerMovement;
	public GameObject gameManager;
	public GameObject playerLifeController;

	public int maxHealth;
	public int health;

	public List<AudioClip> deathAudios;
	public List<AudioClip> hitAudios;

	public int damageWhenHitByEnemy;

	void Awake()
	{
		health = maxHealth;
	}

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () {

		Vector2 movement = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical")) ;

		playerMovement.Move (movement);

	}

	public void Hit(int amount)
	{
		health -= amount;
		checkIfDead ();
		UpdateHealthBar ();
	}

	private void checkIfDead()
	{
		if (health <= 0) 
		{
			playDeathAudio ();
			Destroy (gameObject);
			gameManager.GetComponent<LevelHandler> ().PlayerIsDead ();
		}
		else 
		{
			playHitAudio ();
		}
	}

	public void UpdateHealthBar()
	{
		playerLifeController.GetComponent<PlayerLife> ().UpdateHealth (maxHealth, health);
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.GetComponent<Enemy> () != null) 
		{
			Hit (damageWhenHitByEnemy);
			Destroy (collision.gameObject);
		}
		//check if the projectile is hitting the player
		else if (collision.gameObject.GetComponent<ProjectileController> () != null) 
		{
			Hit (collision.gameObject.GetComponent<ProjectileController> ().damage);
			Destroy (collision.gameObject);

		}
	}

	void playDeathAudio()
	{
		int n = Random.Range (0, deathAudios.Count - 1);
		AudioSource.PlayClipAtPoint (deathAudios [n], Vector3.zero);
	}

	void playHitAudio()
	{
		int n = Random.Range (0, hitAudios.Count - 1);
		AudioSource.PlayClipAtPoint (hitAudios [n], Vector3.zero);
	}
}
