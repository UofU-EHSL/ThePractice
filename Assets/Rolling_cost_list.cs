using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;


public class Rolling_cost_list : MonoBehaviour {

    [System.Serializable]
    public class Items
    {
        public string Name;
        public float Cost_per;
        public int Number_of;
        public float Total_cost;
    }
    public Text nameText;
    public Text costText;
    public Text numberText;
    public Items[] arrayOfItems;
    public List<Items> listOfItems;

    public void Add_to_list(string name, float cost)
    {
        Items temp = new Items();
        Debug.Log("in the add to list section");
        temp.Name = name;
        temp.Cost_per = cost;
        temp.Number_of = 1;
        temp.Total_cost = cost * temp.Number_of;

        listOfItems.AddRange(arrayOfItems);
        listOfItems.Add(temp);
        arrayOfItems = listOfItems.ToArray();
        listOfItems.Clear();

        makeString(temp);
    }

    public void makeString(Items temp)
    {
        string nameTempString = nameText.text;
        string costTempString = costText.text;
        string numberTempString = numberText.text;

            nameTempString = nameTempString + "\n" + temp.Name;
            costTempString = costTempString + "\n" + "$" + temp.Cost_per.ToString();
            numberTempString = numberTempString + "\n" + temp.Number_of.ToString();

        nameText.text = nameTempString;
        costText.text = costTempString;
        numberText.text = numberTempString;
    }
}
