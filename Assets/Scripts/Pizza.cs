using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Pizza : MonoBehaviour
{
	public List<Ingredient> ingredients;
	public bool isCooked = false;
	private GameObject pizzaSlot;
	private PizzaBoxController newBox;


	public void AddIngredient(Ingredient ingredient)
	{
		ingredients.Add(ingredient);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name.Equals("PizzaSlot"))
		{
			pizzaSlot = other.gameObject;
			newBox = pizzaSlot.transform.parent.GetComponent<PizzaBoxController>();
			pizzaSlot.GetComponent<MeshRenderer>().enabled = true;
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (pizzaSlot)
		{
			if(other.gameObject == pizzaSlot) {
				pizzaSlot.GetComponent<MeshRenderer>().enabled = false;
				pizzaSlot = null;
				newBox = null;
			}
		}
	}

	public void AttachToSlot()
	{

		if (pizzaSlot && newBox.pizza == null)
		{
			this.GetComponent<Rigidbody>().isKinematic = true;
			this.GetComponent<XRGrabInteractable>().enabled = false;
			this.GetComponent<BoxCollider>().enabled = false;
			this.transform.parent = pizzaSlot.transform.parent;
			this.transform.position = pizzaSlot.transform.position;
			this.transform.rotation = pizzaSlot.transform.rotation;
			newBox.pizza = this;
			pizzaSlot.SetActive(false);

		}
	}

}
