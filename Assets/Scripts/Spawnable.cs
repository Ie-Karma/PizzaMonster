using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class Spawnable : MonoBehaviour
{
	// Start is called before the first frame update
	private Vector3 originalPosition;
	private Quaternion originalRotation;
	private Vector3 originalScale;
	private GameObject newObject;
	private bool used = false;

	void Start()
	{
		this.GetComponent<Rigidbody>().useGravity = true;
		this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
		originalPosition = this.transform.position;
		originalRotation = this.transform.rotation;
		originalScale = this.transform.localScale;
		//originalMaterial = this.GetComponent<MeshRenderer>().material;

		this.GetComponent<XRGrabInteractable>().firstSelectEntered.AddListener(Grabbed);

	}

	private void Grabbed(SelectEnterEventArgs arg0)
	{
		if (!used)
		{
			this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
			this.GetComponent<Rigidbody>().isKinematic = false;
			StartCoroutine(Spawn());
		}

	}

	private IEnumerator Spawn()
	{
		yield return new WaitForSeconds(.5f);
		if (!used)
		{
			newObject = Instantiate(this.gameObject, originalPosition, originalRotation);
			newObject.transform.localScale = originalScale;
			newObject.GetComponent<Rigidbody>().isKinematic = false;
			//newObject.GetComponent<MeshRenderer>().material = originalMaterial;

			used = true;
		}

	}
}
