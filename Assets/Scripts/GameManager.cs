using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	public GameObject CompleteXRPlayer;
	public GameObject houseTarget;

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
		// Aquí puedes realizar cualquier inicialización adicional que necesites
	}

}
