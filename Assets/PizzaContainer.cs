using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PizzaContainer : MonoBehaviour
{
    public PizzaBoxController pizzaBox;
    public bool isHouse = false;
    public UnityEvent onPlaced;
    public int pizzaNum = 0;
    private int actualPizzaNum = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void AddPizza()
    {
        actualPizzaNum++;
        if(actualPizzaNum == pizzaNum)
        {
            onPlaced.Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
