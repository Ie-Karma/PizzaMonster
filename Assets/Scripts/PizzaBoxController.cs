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
    void Start()
    {
        skMesh = GetComponent<SkinnedMeshRenderer>();
		holderStart = holder_1.position;
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

    public void CloseBox()
    {
        if(((1 - percentage) * 100) < 15)
        {
            isClosed = true;
			skMesh.SetBlendShapeWeight(0, 100);
            holder_1.GetComponent<XRGrabInteractable>().enabled = false;
            this.GetComponent<BoxCollider>().enabled = true;
            this.GetComponent<XRGrabInteractable>().enabled = true;
		}
	}
}
