using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Kitchen : MonoBehaviour
{
    public GameObject kitchenCanvas;

    public TableOrderUIItem tableOrderUIItemPrefab;
    public Transform tableOrderUIItemParent;

    public VerticalLayoutGroup layOutGroup;

    public List<Dish> dishes;
    public List<Table> tables;

    public int numberOrders = 3;
    private List<TableOrder> currentTableorders = new List<TableOrder>();
    private List<Dish> acceptedDishes = new List<Dish>();

    public bool testMode = false;

    public int ValueOfAcceptedDishes
    {
        get
        {
            return acceptedDishes.Sum(d => d.dishPrice);
        }
    }
    public void Awake()
    {
        tables = FindObjectsOfType<Table>().OrderBy(t => t.tableNumber).ToList();
        kitchenCanvas.SetActive(false);

        Cursor.visible = false;

    }
    public void Start()
    {
        ResetKitchen();

    }
    public void ResetAsNeeded()
    {
        if (acceptedDishes.Count == 0)
        {
            ResetKitchen();
        }
    }
    public void ResetKitchen()
    {
        acceptedDishes.Clear();
        foreach (Table table in tables)
        {
            table.Reset();
        }
        RequestOrders();
    }
    public List<Dish> GetCurrentAcceptedDishes()
    {
        List<Dish> shuffledDishes = acceptedDishes.OrderBy(x => Random.value).ToList();
        return acceptedDishes;
    }
    public void DeliverDish(Dish dish)
    {
        acceptedDishes.Remove(dish);
    }
    public void AcceptTableOrder(TableOrder tableOrder)
    {
        currentTableorders.Remove(tableOrder);
        foreach (Dish dish in tableOrder.dishes)
        {
            acceptedDishes.Add(dish);
        }
    }
    public void RequestOrders()
    {
        currentTableorders.Clear();
        foreach (Transform child in tableOrderUIItemParent)
        {
            Destroy(child.gameObject);
        }
        List<Table> shuffledTables = new List<Table>(tables).OrderBy(x => Random.value).ToList();
        if (testMode)
        {
            shuffledTables = new List<Table>(tables);
        }
        for (int i = 0; i < numberOrders; i++)
        {
            TableOrder order = shuffledTables[i].RequestOrder(dishes);
            currentTableorders.Add(order);
            Debug.Log("Order for table " + order.tableNumber + ": " + string.Join(", ", order.dishes.Select(d => d.dishName)));
            TableOrderUIItem tableOrderUIItem = Instantiate(tableOrderUIItemPrefab, tableOrderUIItemParent);
            tableOrderUIItem.Init(order);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player entered the kitchen");
            kitchenCanvas.SetActive(true);
            Cursor.visible = true;
            // layOutGroup.enabled = false;
            // Invoke("ResetLayOutGroup", 1f);

        }
    }
    private void ResetLayOutGroup()
    {
        layOutGroup.enabled = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player left the kitchen");
            kitchenCanvas.SetActive(false);
            Cursor.visible = false;
        }
    }
}
