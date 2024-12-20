using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class Table : MonoBehaviour
{
    public GameObject tableCanvas;

    public OrderUIItemConfirm orderUIItemConfirmPrefab;

    public Transform orderUIItemConfirmParent;

    public TextMeshProUGUI tableNumberText;

    public int guestCount = 2;

    public int tableNumber = 1;

    private TableOrder currentOrder;

    private List<Dish> deliveredDishes = new List<Dish>();

    private List<Dish> correctDishes = new List<Dish>();

    private bool canDeliver = true;

    public void Reset()
    {
        deliveredDishes.Clear();
        currentOrder = null;
        correctDishes.Clear();
        canDeliver = true;
    }


    public TableOrder RequestOrder(List<Dish> dishes)
    {
        List<Dish> selectedDishes = new();
        for (int i = 0; i < guestCount; i++)
        {
            selectedDishes.Add(dishes[Random.Range(0, dishes.Count)]);
        }
        TableOrder order = new TableOrder();
        order.dishes = selectedDishes;
        order.tableNumber = tableNumber;
        currentOrder = order;
        return order;
    }
    public void Awake()
    {
        tableCanvas.SetActive(false);

        Cursor.visible = false;
        tableNumberText.text = "Table " + tableNumber;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (canDeliver == false)
            {
                return;
            }
            Debug.Log("Player entered the table");
            tableCanvas.SetActive(true);
            Cursor.visible = true;

            foreach (Transform child in orderUIItemConfirmParent)
            {
                Destroy(child.gameObject);
            }
            foreach (Dish dish in FindObjectOfType<Kitchen>().GetCurrentAcceptedDishes())
            {
                OrderUIItemConfirm orderUIItemConfirm = Instantiate(orderUIItemConfirmPrefab, orderUIItemConfirmParent);
                orderUIItemConfirm.Init(dish, this);
            }
        }
    }
    public void DeliverDish(Dish dish)
    {
        FindObjectOfType<Kitchen>().DeliverDish(dish);
        deliveredDishes.Add(dish);
        if (deliveredDishes.Count == guestCount)
        {
            canDeliver = false;
            tableCanvas.SetActive(false);
            if (currentOrder == null)
            {
                Debug.Log("Player delivered wrong dish");
                FindObjectOfType<GameController>().SubtractMoney(GetValueOfIncorrectDishes());
                FindObjectOfType<Kitchen>().ResetKitchen();
                return;
            }
          
            foreach (Dish orderDish in currentOrder.dishes)
            {
                string dishName = orderDish.dishName;
                Dish matchingDish = deliveredDishes.Find(d => d.dishName == dishName);
                if (matchingDish != null)
                {
                    correctDishes.Add(matchingDish);
                    deliveredDishes.Remove(matchingDish);
                }
                else
                {
                    Debug.Log("Player delivered wrong dish");
                    FindObjectOfType<GameController>().SubtractMoney(GetValueOfIncorrectDishes());
                    FindObjectOfType<Kitchen>().ResetKitchen();
                    return;
                }

            }
            Debug.Log("Player delivered correct dishes");
            Debug.Log("Number of delivered dishes: " + correctDishes.Count);
            Debug.Log("Value of delivered dishes: " + correctDishes.Sum(d => d.dishPrice));
            FindObjectOfType<GameController>().AddMoney(correctDishes.Sum(d => d.dishPrice));
            FindObjectOfType<Kitchen>().ResetAsNeeded();
        }
    }
    private int GetValueOfIncorrectDishes()
    {
        int value = FindObjectOfType<Kitchen>().ValueOfAcceptedDishes;
        value += deliveredDishes.Sum(d => d.dishPrice);
        value += correctDishes.Sum(d => d.dishPrice);
        return value;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player left the table");
            tableCanvas.SetActive(false);
            Cursor.visible = false;
        }
    }
}
