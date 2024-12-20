using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrderUIItem : MonoBehaviour
{
    public TextMeshProUGUI dishNameText;
   public void Init(Dish dish)
    {
        // Initialize the UI item with the dish information
        dishNameText.text = dish.dishName;
    }
}
