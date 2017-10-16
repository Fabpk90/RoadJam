using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour 
{
	public int life = 5;
	private int currentLife;
	public GameObject playerLifeController;
	public GameObject gameManager;

	public bool isBoss = false;

	public GameObject diyingParticles;

	public Transform leftMarker;
	public Transform rightMarker;

	public GameObject projectile;

	public GameObject scoreText;

	public bool isBossLvl = false;
	public Transform rightMarkerBoss;

	[Tooltip("Gère le cooldown du tire")]
	public float cooldown = 1.5f;
	private float timeStart = 0.0f;

	private float shotX;

	public List<AudioClip> deathAudios;
	public List<AudioClip> hitAudios;

	// Use this for initialization
	void Start () 
	{
		currentLife = life;
		shotX = (isBossLvl)? rightMarkerBoss.position.x : rightMarker.position.x;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (transform.position.x <= shotX - 5)
			Shot ();
	}

	void Shot()
	{
		//Debug.Log (cd);
		if (Time.time - timeStart >= cooldown) 
		{
			timeStart = Time.time;
			GameObject obj = Instantiate (projectile);
			Vector3 pos = transform.position;
			pos.x -= 1.0f;
			obj.transform.position = pos;
		}
	}

	void CheckDeath()
	{
		if (transform.position.x <= leftMarker.position.x) 
		{

			Destroy (gameObject);
		}

		if (currentLife <= 0) {
			ScoreManager.instance.addPoint (life * 100);
			Destroy (gameObject);

			gameManager.GetComponent<LevelHandler> ().BossIsDead ();

			GameObject instance = Instantiate (diyingParticles, transform.position, diyingParticles.transform.rotation);
			Destroy (instance, 1);

			GameObject go = Instantiate (scoreText, transform.position, Quaternion.identity);
			go.GetComponent<TextMesh> ().text = "" + life * 100;

			playDeathAudio ();

			Destroy (go, 1f);

		} 
		else 
		{
			playHitAudio ();
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.GetComponent<ProjectileController> () != null) 
		{
			//if the enemy is hit by a projectile, and it is not his own
			if (!collision.gameObject.GetComponent<ProjectileController> ().isEnemy) 
			{
				currentLife--;

				CheckDeath ();

				Destroy (collision.gameObject);
			}

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
