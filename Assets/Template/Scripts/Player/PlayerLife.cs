using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour {

	public List<Sprite> listImageHealth;

	public GameObject healthSpriteActive;

	void Start()
	{
		//animHealth.pla	
	}

	public void UpdateHealth(int maxHealth, int health)
	{
		healthSpriteActive.GetComponent<Image> ().sprite = listImageHealth [maxHealth - health ];

		print (maxHealth - health);
	}


}
