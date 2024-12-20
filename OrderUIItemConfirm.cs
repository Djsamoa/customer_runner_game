using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrderUIItemConfirm : MonoBehaviour
{
     public TextMeshProUGUI dishNameText;

     private Dish dish;
     private Table table;

     public void OnConfirmButtonPressed()
     {
         table.DeliverDish(dish);
         Destroy(gameObject);

     }
   public void Init(Dish dish, Table table)
    {
        // Initialize the UI item with the dish information
        dishNameText.text = dish.dishName;
        this.dish = dish;
        this.table = table;
    }
}
