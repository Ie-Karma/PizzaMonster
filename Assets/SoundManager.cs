using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.TimeZoneInfo;

public class SoundManager : MonoBehaviour
{

    public AudioSource ambient, theme;
    [Space]
    public AudioClip[] themes;


	private float transitionDuration = 1.0f; // Duraci�n de la transici�n en segundos
	private bool transitioning = false;
	private float transitionTimer = 0.0f;
	private AudioClip currentTheme;
	private AudioClip nextTheme;

	private void Start()
	{
		// Iniciar la reproducci�n del tema actual
		theme.clip = themes[0];
		theme.Play();
	}

	private void Update()
	{
		if (transitioning)
		{
			// Incrementar el temporizador de la transici�n
			transitionTimer += Time.deltaTime;

			// Calcular la mezcla (blend) actual entre los temas basado en el temporizador
			float t = Mathf.Clamp01(transitionTimer / transitionDuration);

			// Aplicar la mezcla utilizando Lerp
			theme.volume = Mathf.Lerp(1.0f, 0.0f, t);

			// Comprobar si la transici�n ha terminado
			if (transitionTimer >= transitionDuration)
			{
				// Establecer el nuevo tema como el actual y restablecer los valores
				theme.clip = nextTheme;
				theme.volume = 1.0f;
				transitioning = false;
			}
		}
	}
	public void ChangeTheme(int themeIndex)
	{
		if (themeIndex >= 0 && themeIndex < themes.Length)
		{
			if (transitioning)
			{
				// Si ya hay una transici�n en progreso, reiniciar el temporizador y asignar el nuevo tema
				transitionTimer = 0.0f;
				nextTheme = themes[themeIndex];
			}
			else
			{
				// Iniciar una nueva transici�n
				transitioning = true;
				currentTheme = theme.clip;
				nextTheme = themes[themeIndex];
				transitionTimer = 0.0f;

				// Detener la reproducci�n actual si hay un tema actualmente reproduci�ndose
				if (theme.isPlaying)
					theme.Stop();

				// Iniciar la reproducci�n del nuevo tema
				theme.clip = nextTheme;
				theme.Play();
			}
		}
		else
		{
			Debug.LogError("�ndice de tema no v�lido: " + themeIndex);
		}
	}

}
