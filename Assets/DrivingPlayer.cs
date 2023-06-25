using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class DrivingPlayer : MonoBehaviour
{
	public List<GameObject> interactors;
	public GameObject xrOrigin;
	public Transform drivingTransform,exitTransform;
	
	private TeleportationProvider teleportationProvider;
	private ContinuousTurnProviderBase continuousTurnProvider;
	private DynamicMoveProvider dynamicMoveProvider;
	private CharacterController characterController;


	private void Start()
	{

		teleportationProvider = xrOrigin.GetComponent<TeleportationProvider>();
		continuousTurnProvider = xrOrigin.GetComponent<ContinuousTurnProviderBase>();
		dynamicMoveProvider = xrOrigin.GetComponent<DynamicMoveProvider>();
		characterController = xrOrigin.GetComponent<CharacterController>();

	}
	public void SetDrivingPlayer(bool drive)
	{

		foreach (var interactor in interactors)
		{
			interactor.SetActive(!drive);
		}
		teleportationProvider.enabled = !drive;
		continuousTurnProvider.enabled = !drive;
		dynamicMoveProvider.enabled = !drive;
		characterController.enabled = !drive;

		if (drive)
		{
			this.gameObject.layer = 3;
			this.transform.parent = drivingTransform.parent;
			this.transform.localPosition = drivingTransform.localPosition;
			this.transform.localRotation = drivingTransform.localRotation;
			this.transform.localScale = drivingTransform.localScale;
			xrOrigin.transform.localPosition = new Vector3(0, 0.245f,0);
		}
		else
		{
			this.gameObject.layer = 6;
			this.transform.localPosition = exitTransform.localPosition;
			this.transform.localRotation= exitTransform.localRotation;

			this.transform.parent = null;
			this.transform.localScale = Vector3.one;

		}

	}

}
