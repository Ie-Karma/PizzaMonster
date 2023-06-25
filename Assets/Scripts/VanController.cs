using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class VanController : MonoBehaviour
{
	public float maxSpeed = 20f;    
	public float turnSpeed = 500f;   

	public XRKnob wheel;
	public XRSlider throttle;
	private float wheelVal, throttleVal;

	private Rigidbody carRigidbody;
	private float steerAngle = 0f;

	private void Start()
	{
		carRigidbody = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		wheelVal = wheel.value;
		throttleVal = throttle.value - 0.5f;

		// Calcular la velocidad y dirección del coche
		float speed = maxSpeed * throttleVal;
		float turnAngle = 0f;

		if (wheelVal < 0.5f)
		{
			turnAngle = -turnSpeed * (0.5f - wheelVal) * 2f; // Girar a la izquierda
		}
		else if (wheelVal > 0.5f)
		{
			turnAngle = turnSpeed * (wheelVal - 0.5f) * 2f; // Girar a la derecha
		}

		// Aplicar la velocidad al coche
		Vector3 movement = transform.forward * speed * Time.fixedDeltaTime;
		carRigidbody.MovePosition(carRigidbody.position + movement);

		// Rotar el coche en la dirección del volante
		Quaternion rotation = Quaternion.Euler(0f, turnAngle * Time.fixedDeltaTime, 0f);
		carRigidbody.MoveRotation(carRigidbody.rotation * rotation);
	}

}
