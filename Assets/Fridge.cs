using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Fridge : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject canvasIngredients, canvasButton;
    private Animator m_Animator;
    public Image[] ingredientesTexts;
    private GameObject player;
    private bool isOpen = false;
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        ingredientesTexts = canvasIngredients.GetComponentsInChildren<Image>();
        player = Camera.main.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Image txt in ingredientesTexts) { 

            txt.transform.LookAt(player.transform.position);

        }
        if (isOpen && Vector3.Distance(this.transform.position, Camera.main.transform.position) > 3)
        {
            OpenClose(false);
		}
    }

    public void OpenClose(bool open)
    {
        m_Animator.SetBool("Open", open);
        isOpen = open;
    }


}
