using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class VanController : MonoBehaviour
{
	public float maxSpeed = 20f;    
	public float turnSpeed = 500f;
	public GameObject compass;

	public XRKnob wheel;
	public XRSlider throttle;
	private float wheelVal, throttleVal;
	private Animator vanAnimator;
	public List<PizzaContainer> PizzaContainers;

	private Rigidbody carRigidbody;
	private float steerAngle = 0f;
	private AudioSource audioSource;
	public GameObject player,drivingPlayer;
	
	private void Start()
	{
		carRigidbody = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
		vanAnimator = GetComponent<Animator>();
	}

	public void OpenBackDoor()
	{
		vanAnimator.SetTrigger("OpenBackDoor");
	}

	public void StartDriving(bool start)
	{
		vanAnimator.SetTrigger("OpenFontDoor");
		StartCoroutine(StartDrivingCoroutine(start));
	}

	private IEnumerator StartDrivingCoroutine(bool start)
	{
		yield return new WaitForSeconds(vanAnimator.GetCurrentAnimatorStateInfo(0).length);

		player.GetComponent<DrivingPlayer>().SetDrivingPlayer(start);
		vanAnimator.SetTrigger("Close");

	}

	private void FixedUpdate()
	{

		/*
		if (GameManager.instance.houseTarget)
		{
			compass.SetActive(true);
			compass.transform.LookAt(GameManager.instance.houseTarget.transform);
		}
		else {

			compass.SetActive(false);

		}

		*/

		compass.transform.LookAt(GameManager.instance.houseTarget.transform);


		wheelVal = wheel.value;
		throttleVal = throttle.value - 0.5f;
		float speed = 0;
		if (throttleVal <= 0f)
		{
			if (throttleVal < -0.4f)
			{
				throttleVal = (throttle.value - 0.5f)*0.15f;
				speed = maxSpeed * throttleVal;
			}
			else
			{
				throttleVal = 0;
			}

		}
		else
		{
			throttleVal = throttle.value - 0.5f;
			speed = maxSpeed * throttleVal;
		}

		// Calcular la velocidad y dirección del coche
		float turnAngle = 0f;

		if (wheelVal < 0.5f)
		{
			turnAngle = -turnSpeed * (0.5f - wheelVal) * 2f * (Mathf.Abs(throttleVal)); // Girar a la izquierda
		}
		else if (wheelVal > 0.5f)
		{
			turnAngle = turnSpeed * (wheelVal - 0.5f) * 2f * (Mathf.Abs(throttleVal)); // Girar a la derecha
		}

		// Aplicar la velocidad al coche
		Vector3 movement = transform.forward * speed * Time.fixedDeltaTime;
		carRigidbody.MovePosition(carRigidbody.position + movement);

		// Rotar el coche en la dirección del volante
		Quaternion rotation = Quaternion.Euler(0f, turnAngle * Time.fixedDeltaTime, 0f);
		carRigidbody.MoveRotation(carRigidbody.rotation * rotation);

		// Sonido del motor
		if (audioSource != null)
		{
			audioSource.volume = Mathf.Lerp(audioSource.volume, Mathf.Abs(throttleVal)*2, Time.deltaTime * 5f);
			audioSource.pitch = 0.8f + Mathf.Abs(throttleVal);
		}

	}

}
