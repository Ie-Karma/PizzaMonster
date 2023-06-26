using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.TimeZoneInfo;

public class SoundManager : MonoBehaviour
{

    public AudioSource ambient, theme;
    [Space]
    public AudioClip[] themes;

	private float transitionDuration = 1.0f; // Duración de la transición en segundos
	private bool transitioning = false;
	private float transitionTimer = 0.0f;
	private AudioClip currentTheme;
	private AudioClip nextTheme;

	public static SoundManager instance;
	public GameObject CompleteXRPlayer;

	private void Awake()
	{
		// Verificar si ya existe una instancia del GameManager
		if (instance != null && instance != this)
		{
			Destroy(gameObject);
			return;
		}

		// Si no existe una instancia, la establece como la instancia actual
		instance = this;

		// No destruir este objeto al cargar una nueva escena
		DontDestroyOnLoad(gameObject);
	}


	private void Start()
	{
		// Iniciar la reproducción del tema actual
		theme.clip = themes[0];
		theme.Play();
	}

	private void Update()
	{
		if (transitioning)
		{
			// Incrementar el temporizador de la transición
			transitionTimer += Time.deltaTime;

			// Calcular la mezcla (blend) actual entre los temas basado en el temporizador
			float t = Mathf.Clamp01(transitionTimer / transitionDuration);

			// Aplicar la mezcla utilizando Lerp
			theme.volume = Mathf.Lerp(1.0f, 0.0f, t);

			// Comprobar si la transición ha terminado
			if (transitionTimer >= transitionDuration)
			{
				// Establecer el nuevo tema como el actual y restablecer los valores
				theme.clip = nextTheme;
				theme.volume = 1.0f;
				transitioning = false;
			}
		}
	}
	public void ChangeTheme(AudioClip nextTheme)
	{
		if (nextTheme)
		{
			theme.clip = nextTheme;
			theme.Play();
		}
		else
		{
			theme.clip = themes[0];
			theme.Play();
		}
		return;


		if (transitioning)
			{
				// Si ya hay una transición en progreso, reiniciar el temporizador y asignar el nuevo tema
				transitionTimer = 0.0f;
			}
			else
			{
				// Iniciar una nueva transición
				transitioning = true;
				currentTheme = theme.clip;
				transitionTimer = 0.0f;

				// Detener la reproducción actual si hay un tema actualmente reproduciéndose
				if (theme.isPlaying)
					theme.Stop();

				// Iniciar la reproducción del nuevo tema
				theme.clip = nextTheme;
				theme.Play();
			}
		

	}

}
