using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform telefono, baseTelefono;
    private LineRenderer lineRenderer;
	public int numPuntos = 10;  // N�mero de puntos intermedios
	public float maxDeformacion = 0.1f;  // M�xima deformaci�n del cable

	void Start()
    {
        
        lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.positionCount = numPuntos + 2;  // +2 para incluir el tel�fono y la base

	}

	void Update()
	{
		GenerateCable();
	}

	private void GenerateCable() {

		// Actualizar los puntos del Line Renderer
		lineRenderer.SetPosition(0, telefono.position);  // Establecer el primer punto como la posici�n del tel�fono
		lineRenderer.SetPosition(numPuntos + 1, baseTelefono.position);  // Establecer el �ltimo punto como la posici�n de la base

		// Generar puntos intermedios con deformaci�n hacia abajo
		for (int i = 1; i <= numPuntos; i++)
		{
			float t = i / (float)(numPuntos + 1);  // Calcular el valor t normalizado
			Vector3 punto = Vector3.Lerp(telefono.position, baseTelefono.position, t);  // Interpolar entre el tel�fono y la base

			// Aplicar deformaci�n hacia abajo al punto
			float deformacion = Mathf.Sin(t * Mathf.PI) * maxDeformacion;  // Calcular deformaci�n basada en una funci�n senoidal
			punto += Vector3.down * deformacion;  // Aplicar la deformaci�n hacia abajo

			lineRenderer.SetPosition(i, punto);  // Establecer el punto intermedio
		}
	}
}
