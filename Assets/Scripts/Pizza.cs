using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class Pizza : MonoBehaviour
{
	public List<Ingredient> ingredients;
	public bool isCooked = false;
	private GameObject pizzaSlot;
	private PizzaBoxController newBox;
	private Canvas ingredientCanvas;
	private GameObject ingredientImage;

	private void Start()
	{
		ingredientCanvas = GetComponentInChildren<Canvas>();
		ingredientImage = ingredientCanvas.transform.GetChild(0).gameObject;
		ingredientImage.SetActive(false);
	}


	public void AddIngredient(Ingredient ingredient)
	{
		ingredients.Add(ingredient);
		GameObject newImage = Instantiate(ingredientImage);
		newImage.transform.parent = ingredientCanvas.transform;
		float randomScale = Random.Range(0.5f, 1f);
		//newImage.transform.localScale = Vector3.one * randomScale;
		//newImage.transform.localPosition = Vector3.zero;
		newImage.GetComponent<RawImage>().texture = ingredient.texture;
		newImage.name = ingredient.ingredientName.ToString();

		CopyRectTransform(ingredientImage.GetComponent<RectTransform>(), newImage.GetComponent<RectTransform>());

		newImage.SetActive(true);
	}
	void CopyRectTransform(RectTransform source, RectTransform target)
	{
		target.anchorMin = source.anchorMin;
		target.anchorMax = source.anchorMax;
		target.pivot = source.pivot;
		target.anchoredPosition3D = Vector3.zero;
		target.sizeDelta = source.sizeDelta;
		Vector3 rotation = source.eulerAngles;
		rotation.z += Random.Range(0f, 360f);
		target.eulerAngles = rotation;
		
		float randomScale = Random.Range(0.5f, 1f);
		target.localScale = Vector3.one * randomScale;
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
