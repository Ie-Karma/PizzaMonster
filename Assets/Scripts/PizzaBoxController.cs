using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PizzaBoxController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Transform holder_1, holderEnd;
    private Vector3 holderStart;
    private SkinnedMeshRenderer skMesh;
    private float percentage;
    private bool isClosed = false;
    public Pizza pizza;
    private PizzaContainer container;
    void Start()
    {
        skMesh = GetComponent<SkinnedMeshRenderer>();
		holderStart = holder_1.position;
		this.GetComponent<XRGrabInteractable>().selectExited.AddListener(AttachToSlot);

	}

	private void AttachToSlot(SelectExitEventArgs arg0)
	{

		this.GetComponent<Rigidbody>().isKinematic = true;
		this.transform.parent = container.transform;
		this.transform.localPosition = Vector3.zero;
		this.transform.localRotation = new Quaternion(0,0,0,0);

	}

	// Update is called once per frame
	void Update()
    {
        if (!isClosed)
		{
			float distance = Vector3.Distance(holderStart, holderEnd.position);
			float distanceToHolder1 = Vector3.Distance(holder_1.position, holderEnd.position);
			percentage = distanceToHolder1 / distance;
			skMesh.SetBlendShapeWeight(0, (1 - percentage) * 100);
		}

	}

    

	private void OnTriggerEnter(Collider other)
	{
        if (other.TryGetComponent<PizzaContainer>(out PizzaContainer cont) && container == null) {
        
            container = cont;
            container.GetComponent<MeshRenderer>().enabled = true;
        }
	}

	private void OnTriggerExit(Collider other)
	{
        if (container != null && other.TryGetComponent<PizzaContainer>(out PizzaContainer cont))
        {
			if (cont == container)
			{

                container = null;
				container.GetComponent<MeshRenderer>().enabled = false;

			}
		}
		
	}

	public void CloseBox()
    {
        if(skMesh.GetBlendShapeWeight(0) > 85)
        {
            isClosed = true;
			skMesh.SetBlendShapeWeight(0, 100);
            holder_1.GetComponent<XRGrabInteractable>().enabled = false;
            this.GetComponent<BoxCollider>().enabled = true;
            this.GetComponent<XRGrabInteractable>().enabled = true;
            holder_1.GetComponent<SphereCollider>().enabled = false;
		}
	}
}
