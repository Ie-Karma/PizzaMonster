using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject[] houses;
    public int acttualLevel = 1;
    public Phone phone;
    private List<IngredientBlackboard> actualBBs;
    private bool completed = false;
    private bool prevCompleted = false;
	public GameObject barrera;
	// Start is called before the first frame update
	void Start()
    {
        actualBBs = new List<IngredientBlackboard>();
        actualBBs.Add(GameManager.instance.blackBoards[0]);
		actualBBs.Add(GameManager.instance.blackBoards[1]);
		GameManager.instance.actualBB = actualBBs[0];

		phone.GetCall(0);

	}

	// Update is called once per frame
	void Update()
    {
        if (actualBBs.Count > 1)
        {
            if (actualBBs[0].isCompleted && !completed)
            {
                actualBBs[0].gameObject.SetActive(false);
				actualBBs[1].gameObject.SetActive(true);
				GameManager.instance.actualBB = actualBBs[1];
                completed = true;

			}
        }
    }

    public void LevelCompleted()
    {

        GameManager.instance.houseTarget = houses[0];
        prevCompleted = true;
		NextLevel();

	}

    public void NextLevel()
    {
        if (prevCompleted) {
			completed = false;
			acttualLevel++;
			GameManager.instance.houseTarget = houses[acttualLevel];
			phone.GetCall(acttualLevel - 1);

			actualBBs.Clear();


			switch (acttualLevel)
			{
				case 2:
					barrera.SetActive(false);
					actualBBs.Add(GameManager.instance.blackBoards[2]);
					break;
				case 3:
					actualBBs.Add(GameManager.instance.blackBoards[3]);
					actualBBs.Add(GameManager.instance.blackBoards[4]);
					break;

			}
			GameManager.instance.actualBB = actualBBs[0];
			prevCompleted = false;
		}

	}
}
