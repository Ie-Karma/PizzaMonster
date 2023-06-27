using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Ingredient : MonoBehaviour
{
	public IngredientType ingredientName;
	public Texture texture;
	private Pizza pizza;
	public enum IngredientType
	{
		Brocoli,
		Cabbage,
		Carrot,
		Garlic,
		Onion,
		Pepper,
		Pineapple,
		Potato,
		Tomato,
		Radicchio,
		Bacon,
		Egg,
		Bone,
		Blood,
		Random

	}
	private void Start()
	{
		this.GetComponent<XRGrabInteractable>().selectExited.AddListener(PlaceOnPizza);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.GetComponent<Pizza>() != null && pizza == null)
		{
			pizza = other.gameObject.GetComponent<Pizza>();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.TryGetComponent<Pizza>(out Pizza piz))
		{
			if(piz == pizza)
			{
				pizza = null;
			}
		}
	}

	private void PlaceOnPizza(SelectExitEventArgs arg0)
	{
		if (pizza)
		{
			pizza.AddIngredient(this);
			this.gameObject.SetActive(false);
		}
	}
}
