using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza : MonoBehaviour
{
	public List<Ingredient> ingredients;
	public bool isCooked = false;


	public void AddIngredient(Ingredient ingredient)
	{
		ingredients.Add(ingredient);
	}
}
