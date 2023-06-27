using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Ingredient;

[Serializable]
public class BlackboardElement
{
	public TextMeshProUGUI textMeshPro;
	public IngredientType ingredientName;
	public int amount = 1;
	public bool completed = false;
}

public class IngredientBlackboard : MonoBehaviour
{
    public Pizza pizza;
	public List<BlackboardElement> pizzaElements;


	void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
		if (pizza)
		{
			CheckPizzaIngredients();

		}

	}

	public void CheckPizzaIngredients()
	{
		bool allCompleted = true; // Variable para controlar si todos los pizzaElements están completos

		foreach (var BlackboardElement in pizzaElements)
		{
			int count = 0;
			foreach (var ingredient in pizza.ingredients)
			{
				if (ingredient.ingredientName == BlackboardElement.ingredientName || BlackboardElement.ingredientName == IngredientType.Random)
				{
					count++;
				}
			}

			if (count == BlackboardElement.amount)
			{
				BlackboardElement.textMeshPro.color = Color.green;
				BlackboardElement.completed = true; // Marcar como completado
			}
			else
			{
				BlackboardElement.textMeshPro.color = Color.white;
				BlackboardElement.completed = false; // Marcar como no completado
				allCompleted = false; // Al menos un ingredientBlackboard no está completo
			}
		}

		if (allCompleted)
		{
			pizza.CompletePizza();
		}
	}

}
