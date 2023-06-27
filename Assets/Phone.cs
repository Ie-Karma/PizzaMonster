using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Phone : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform telefono, baseTelefono;
    public LineRenderer lineRenderer;
	public int numPuntos = 10;  // Número de puntos intermedios
	public float maxDeformacion = 0.1f;  // Máxima deformación del cable

	public AudioClip call_1, call_2, call_3, wellcome, ring;
	public AudioSource audioSource;
	private bool isCalling = false;
	private int actualCall = 0;
	private bool canPlace,placed = false;
	public GameObject slot;
	public bool debugPlace = false;

	void Start()
    {
        
		lineRenderer.positionCount = numPuntos + 2;  // +2 para incluir el teléfono y la base

		GetCall(0);
		canPlace = true;
		PlacePhone();
		slot.GetComponent<MeshRenderer>().enabled = false;

	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == slot && !placed)
		{
			canPlace = true;
			slot.GetComponent<MeshRenderer>().enabled = true;
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject == slot)
		{

			canPlace = false;
			slot.GetComponent<MeshRenderer>().enabled = false;

		}
	}

	public void PlacePhone()
	{
		if (canPlace)
		{
			this.transform.position = slot.transform.position;
			this.transform.rotation = slot.transform.rotation;
			this.GetComponent<Rigidbody>().isKinematic = true;
			slot.GetComponent<MeshRenderer>().enabled = false;
			placed = true;
		}
	}

	public void GetCall(int i) {

		audioSource.clip = ring; audioSource.Play();
		isCalling = true;
		actualCall = i;
		audioSource.loop = true;
	
	}

	public void AnswerCall()
	{
		if (isCalling)
		{
			audioSource.Stop();
			audioSource.clip = wellcome; audioSource.Play();
			audioSource.loop = false;
			//wait until wllcome clip has finished

			StartCoroutine(AnswerCallCoroutine());

			isCalling = false;
		}
		placed = false;
		this.GetComponent<Rigidbody>().isKinematic = false;
	}

	private IEnumerator AnswerCallCoroutine()
	{

		//wait until wllcome clip has finished

		yield return new WaitForSeconds(wellcome.length);

		switch (actualCall)
			{
			case 0:
				audioSource.clip = call_1; audioSource.Play();
				break;
			case 1:
				audioSource.clip = call_2; audioSource.Play();
				break;
			case 2:
				audioSource.clip = call_3; audioSource.Play();
				break;
		}
	
	}

	void Update()
	{
		GenerateCable();
		this.GetComponent<Rigidbody>().isKinematic = placed;

	}

	private void GenerateCable() {

		// Actualizar los puntos del Line Renderer
		lineRenderer.SetPosition(0, telefono.position);  // Establecer el primer punto como la posición del teléfono
		lineRenderer.SetPosition(numPuntos + 1, baseTelefono.position);  // Establecer el último punto como la posición de la base

		// Generar puntos intermedios con deformación hacia abajo
		for (int i = 1; i <= numPuntos; i++)
		{
			float t = i / (float)(numPuntos + 1);  // Calcular el valor t normalizado
			Vector3 punto = Vector3.Lerp(telefono.position, baseTelefono.position, t);  // Interpolar entre el teléfono y la base

			// Aplicar deformación hacia abajo al punto
			float deformacion = Mathf.Sin(t * Mathf.PI) * maxDeformacion;  // Calcular deformación basada en una función senoidal
			punto += Vector3.down * deformacion;  // Aplicar la deformación hacia abajo

			lineRenderer.SetPosition(i, punto);  // Establecer el punto intermedio
		}
	}
}
