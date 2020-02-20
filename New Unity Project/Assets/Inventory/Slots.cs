using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Slots : MonoBehaviour
{
    [SerializeField] int selectedSlot;



    public bool inventoryToggled = false;

    [SerializeField] int slotsPerRowInv;
    [SerializeField] int slotsPerColInv;
    public int numSlotsInventory;
    public int numSlotsHotbar;
    int numSlots;
    [SerializeField]GameObject slot;
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject slotHighlight;
    GameObject[] slots;
    GameObject[] InvSlots;
    GameObject[] slotHighlights;
    // Start is called before the first frame update
    void Start()
    {
        numSlots = numSlotsHotbar + numSlotsInventory;
        numSlotsInventory = slotsPerColInv * slotsPerRowInv;
        slots = new GameObject[numSlotsHotbar];
        slotHighlights = new GameObject[numSlotsHotbar];
        for (int i = 0; i < numSlotsHotbar; i++)
        {
            slot.name = "slot" + i;

            slots[i] = Instantiate(slot);
            slots[i].transform.SetParent(canvas.transform);
            slots[i].transform.localPosition = (new Vector3(-800 + 200 * i, -400, 0));
            slotHighlights[i] = Instantiate(slotHighlight);
            slotHighlights[i].transform.SetParent(slots[i].transform);
            slotHighlights[i].transform.localPosition = new Vector3(0, 0, 0);
            slotHighlights[i].SetActive(false);
        }
        slotHighlights[0].SetActive(true);


        InvSlots = new GameObject[numSlotsInventory];
        for(int i = 0; i < numSlotsInventory; i++)
        {
            slot.name = "slot" + (numSlotsHotbar + i);

            InvSlots[i] = Instantiate(slot);
            InvSlots[i].transform.SetParent(canvas.transform);
            InvSlots[i].transform.localPosition = (new Vector3(-800 + 200 * (i % slotsPerRowInv), 300 - 200 * (i / slotsPerRowInv)));
            InvSlots[i].SetActive(inventoryToggled);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject[] GetSlots()
    {
        return slots;
    }

    public GameObject[] GetInvSlots()
    {
        return InvSlots;
    }

    public int FindAndAddItem(GameObject obj_interacted)
    {
        //Get the item
        GameObject item = obj_interacted.GetComponent<Item>().getItem();
        bool stored = false;


        for (int i = 0; i < numSlots; i++)
        {
            if (slots[i].GetComponent<Slot>().sameItem(item.name)) //Se os dois forem o mesmo item e se o slot está ocupado
            {
                slots[i].GetComponent<Slot>().incrementItem();
                stored = true;
                break;
            }
        }
        if (!stored)
        {
            for (int i = 0; i < numSlotsInventory; i++)
            {
                if (InvSlots[i].GetComponent<Slot>().sameItem(item.name)) //Se os dois forem o mesmo item e se o slot está ocupado
                {
                    InvSlots[i].GetComponent<Slot>().incrementItem();
                    stored = true;
                    break;
                }
            }
        }
            if (!stored)
        {
        for (int i = 0; i < numSlots; i++)
            {

                //if(slots[i].GetComponent<Slot>().sameItem(item.name)) //Se os dois forem o mesmo item e se o slot está ocupado
                //{
                //    slots[i].GetComponent<Slot>().incrementItem();
                //    break;
                //}
                //else 
                if (!slots[i].GetComponent<Slot>().isOccupied())
                {
                    slots[i].GetComponent<Slot>().addNewItem(item);
                    return 0;
                }
                else
                {
                    continue;
                }
            }
            for (int i = 0; i < numSlotsInventory; i++)
            {

                //if(slots[i].GetComponent<Slot>().sameItem(item.name)) //Se os dois forem o mesmo item e se o slot está ocupado
                //{
                //    slots[i].GetComponent<Slot>().incrementItem();
                //    break;
                //}
                //else 
                if (!InvSlots[i].GetComponent<Slot>().isOccupied())
                {
                    InvSlots[i].GetComponent<Slot>().addNewItem(item);
                    return 0;
                }
                else
                {
                    continue;
                }
            }
        }
        return 0;
    }

    public int useItem(int sel_slot) //First slot is 0
    {
        return (slots[sel_slot].GetComponent<Slot>().useItem());

    }
    public int getSelectedSlot()
    {
        return selectedSlot;
    }
    public void setSelectedSlot(int a)
    {
        if(a == 0)
        {
            return;
        }
        slotHighlights[selectedSlot - 1].SetActive(false);
        selectedSlot = a;
        slotHighlights[a - 1].SetActive(true);
    }

    public void toggleInventory()
    {
        for(int i = 0; i < numSlotsInventory; i++)
        {
            InvSlots[i].SetActive(!InvSlots[i].activeSelf);
        }
        if (!InvSlots[0].activeSelf)
        {
            Time.timeScale = 1f;
        }
        else
        {
            Time.timeScale = 0f;
        }
    }

}
