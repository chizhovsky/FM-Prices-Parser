using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;

public class Action : MonoBehaviour
{
    [HideInInspector] public InputField inputField;
    [HideInInspector] public Button clearButton;
    [HideInInspector] public Text text;
    private DateTime inputDate;
    public List<Countries> countriesList = new List<Countries>();
    private List<DateTime> firstWindowStartList = new List<DateTime>();
    private List<DateTime> firstWindowEndList = new List<DateTime>();
    private List<DateTime> secondWindowStartList = new List<DateTime>();
    private List<DateTime> secondWindowEndList = new List<DateTime>();
    public List<string> countriesResult = new List<string>();

    private void Start() 
    {
        clearButton.onClick.AddListener(ClearCountries);
        foreach (var country in countriesList)
        {
            string[] firstStartDate = country.firstWindowStart.Split('/');
            firstWindowStartList.Add(new DateTime(2011, int.Parse(firstStartDate[1]), int.Parse(firstStartDate[0])));
            string[] firstEndDate = country.firstWindowEnd.Split('/');
            firstWindowEndList.Add(new DateTime(2011, int.Parse(firstEndDate[1]), int.Parse(firstEndDate[0])));
            if (firstWindowStartList[firstWindowStartList.Count - 1].Month > firstWindowEndList[firstWindowEndList.Count - 1].Month)
            {
                firstWindowEndList[firstWindowEndList.Count - 1] = firstWindowEndList[firstWindowEndList.Count - 1].AddYears(1);
            }
            string[] secondStartDate = country.secondWindowStart.Split('/');
            secondWindowStartList.Add(new DateTime(2011, int.Parse(secondStartDate[1]), int.Parse(secondStartDate[0])));
            string[] secondEndDate = country.secondWindowEnd.Split('/');
            secondWindowEndList.Add(new DateTime(2011, int.Parse(secondEndDate[1]), int.Parse(secondEndDate[0])));
            if (secondWindowStartList[secondWindowStartList.Count - 1].Month > secondWindowEndList[secondWindowEndList.Count - 1].Month)
            {
                secondWindowEndList[secondWindowEndList.Count - 1] = secondWindowEndList[secondWindowEndList.Count - 1].AddYears(1);
            }
        }
        //Debug.Log(secondWindowEndList[0].ToString("dd MMM yyyy", new CultureInfo("en-GB")));
    }

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            string[] dateSplit = inputField.text.Split('/');
            inputDate = new DateTime(2011, int.Parse(dateSplit[1]), int.Parse(dateSplit[0]));
            for (int i = 0; i < countriesList.Count; i++)
            {
                if ((inputDate >= firstWindowStartList[i] && inputDate <= firstWindowEndList[i])||(inputDate >= secondWindowStartList[i] && inputDate <= secondWindowEndList[i]))
                {
                    if (!countriesResult.Contains(countriesList[i].countryName))
                    {
                        countriesResult.Add(countriesList[i].countryName);
                    }
                }
                var tempDate = inputDate.AddYears(1);
                if ((tempDate >= firstWindowStartList[i] && tempDate <= firstWindowEndList[i])||(tempDate >= secondWindowStartList[i] && tempDate <= secondWindowEndList[i]))
                {
                    if (!countriesResult.Contains(countriesList[i].countryName))
                    {
                        countriesResult.Add(countriesList[i].countryName); 
                    }              
                }
            }
            countriesResult.Sort();
            foreach (var country in countriesResult)
            {
                text.text += country + " ";
            }
        }
    }
    private void ClearCountries()
    {
        countriesResult.Clear();
        text.text = "";
    }
}
