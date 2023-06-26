using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform telefono, baseTelefono;
    private LineRenderer lineRenderer;
	public int numPuntos = 10;  // Número de puntos intermedios
	public float maxDeformacion = 0.1f;  // Máxima deformación del cable

	void Start()
    {
        
        lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.positionCount = numPuntos + 2;  // +2 para incluir el teléfono y la base

	}

	void Update()
	{
		GenerateCable();
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
