using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Town : MonoBehaviour
{
    public AudioClip theme;
	private bool actual = false;

	private void OnTriggerEnter(Collider other)
	{

		if (other.tag == "Player" && !actual)
		{
			SoundManager.instance.ChangeTheme(theme);
			actual = true;
		}

	}

	private void OnTriggerExit(Collider other)
	{
		
		if (other.tag == "Player" && actual)
			{
			SoundManager.instance.ChangeTheme(null);
			actual = false;
		}
		
	}
}
