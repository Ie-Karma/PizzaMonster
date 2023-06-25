using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ingredient : MonoBehaviour
{
	public IngredientType ingredientName;	
	public enum IngredientType
	{
		Apple,
		Banana,
		Cherry,
		Orange,
		Pear,
		Pineapple,
		Strawberry,
		Watermelon
	}

	private void Awake()
	{
		gameObject.name = ingredientName.ToString();
	}

}
