using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //For dragging items
    //int slot;
    Slots slts;
    //For the item to be on top of the slots and such
    GameObject temp;
    
    GameObject item_attached;
    GameObject instantiated_item;
    //bool occupied;
    [SerializeField]int amout;
    //Item item;

    //For text
    [SerializeField] Text text;
    Text text_instantiated;
    // Start is called before the first frame update
    void Start()
    {
        //For dragging items
        slts = GameObject.Find("Canvas").GetComponent<Slots>();

        amout = 0;
        text_instantiated = Instantiate(text) as Text;
        text_instantiated.transform.SetParent(gameObject.transform);
        text_instantiated.transform.localPosition = new Vector3(40, 40, 0);
        text_instantiated.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if(amout != 0)
        {
            text_instantiated.text = amout.ToString();
        }
        else
        {
            text_instantiated.text = "";
        }
    }

    public int getAmout()
    {
        return amout;
    }

    public int removeItem()
    {
        if(amout < 0)
        {
            return 2; //Erro
        }
        if(amout == 0)
        {
            return 1;
        }
        if(amout == 1)
        {
            amout--;
            Destroy(instantiated_item);
            //Destroy(item_attached); //Don't destroy the prefab
            return 0;
        }
        amout--;
        return 0;
        
    }
    public int useItem()
    {
        if (amout > 0)
        {
            if(gameObject.GetComponent<ItemEffect>().UseItem(item_attached.name) == 10)
            {
                return 10;
            }
            //amout--;
        }
        return removeItem();
    }

    public bool isOccupied()
    {
        if(amout == 0)
        {
            return false;
        }
            return true;
    }

    public bool sameItem(string item_name)
    {
        if (amout == 0)
        {
            return false;
        }
        if(item_name == item_attached.name)
        {
            return true;
        }
        return false;
    }

    public void incrementItem()
    {
        amout++;
    }
    public void addNewItem(GameObject item)
    {
        item_attached = item;
        instantiated_item = Instantiate(item);
        instantiated_item.transform.SetParent(gameObject.transform);
        instantiated_item.transform.localPosition = new Vector3(0, 0, 0);
        amout = 1;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (instantiated_item != null && amout != 0)
        {
            temp = Instantiate(item_attached);
            temp.transform.SetParent(GameObject.Find("Canvas").transform);
            instantiated_item.transform.localScale = new Vector3(0, 0, 0); //Faz-lo "invisivel"
            //instantiated_item.transform.position = eventData.position;
            temp.transform.position = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (instantiated_item != null && amout != 0)
        {
            //instantiated_item.transform.position = eventData.position;
            
            //"Fail"safe if last item is used
            if(amout == 0)
            {
                Destroy(temp);
                Destroy(instantiated_item);
                item_attached = null;
            }

            temp.transform.position = eventData.position;
            //temp.transform.position = Camera.main.ScreenToWorldPoint( eventData.position);
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (instantiated_item != null && amout != 0)
        {

            
            
            instantiated_item.transform.localScale = new Vector3(1, 1, 1);

            int result = -1;


            for (int i = 0; i < slts.numSlotsHotbar; i++)
            {

                //Bootleg hitbox finding.

                //if it's after the start of the hitbox's x
                if(- slts.GetSlots()[i].GetComponent<RectTransform>().rect.width/2 + slts.GetSlots()[i].transform.position.x < eventData.position.x)
                {
                    //if it's before the end of the hitbox's x
                    if (slts.GetSlots()[i].GetComponent<RectTransform>().rect.width / 2 + slts.GetSlots()[i].transform.position.x > eventData.position.x)
                    {
                        //if it's after the start of the hitbox's y
                        if (-slts.GetSlots()[i].GetComponent<RectTransform>().rect.height / 2 + slts.GetSlots()[i].transform.position.y < eventData.position.y)
                        {
                            //if it's before the end of the hitbox's y
                            if (slts.GetSlots()[i].GetComponent<RectTransform>().rect.height / 2 + slts.GetSlots()[i].transform.position.y > eventData.position.y)
                            {
                                //Debug.Log("Inside slot" + i + ".");
                                result = i;
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < slts.numSlotsInventory; i++)
            {

                //Bootleg hitbox finding.

                //if it's after the start of the hitbox's x
                if (-slts.GetInvSlots()[i].GetComponent<RectTransform>().rect.width / 2 + slts.GetInvSlots()[i].transform.position.x < eventData.position.x)
                {
                    //if it's before the end of the hitbox's x
                    if (slts.GetInvSlots()[i].GetComponent<RectTransform>().rect.width / 2 + slts.GetInvSlots()[i].transform.position.x > eventData.position.x)
                    {
                        //if it's after the start of the hitbox's y
                        if (-slts.GetInvSlots()[i].GetComponent<RectTransform>().rect.height / 2 + slts.GetInvSlots()[i].transform.position.y < eventData.position.y)
                        {
                            //if it's before the end of the hitbox's y
                            if (slts.GetInvSlots()[i].GetComponent<RectTransform>().rect.height / 2 + slts.GetInvSlots()[i].transform.position.y > eventData.position.y)
                            {
                                //Debug.Log("Inside slot" + (i + slts.numSlotsHotbar) + ".");
                                result = i + slts.numSlotsHotbar;
                            }
                        }
                    }
                }
            }
            //Debug.Log("slot: " + slot);
            if (result < slts.numSlotsHotbar && result != -1 ) //To hotbar
            {
                int otherObjAmout = slts.GetSlots()[result].GetComponent<Slot>().getAmout();
                GameObject otherObjItem = slts.GetSlots()[result].GetComponent<Slot>().item_attached;
                //int otherObjSlot = slts.GetSlots()[result].GetComponent<Slot>().slot;


                slts.GetSlots()[result].GetComponent<Slot>().amout = amout;
                
                slts.GetSlots()[result].GetComponent<Slot>().item_attached = item_attached;
                
                if(slts.GetSlots()[result].GetComponent<Slot>().instantiated_item != null)
                {
                    Destroy(slts.GetSlots()[result].GetComponent<Slot>().instantiated_item);
                }

                slts.GetSlots()[result].GetComponent<Slot>().instantiated_item = Instantiate(slts.GetSlots()[result].GetComponent<Slot>().item_attached);
                slts.GetSlots()[result].GetComponent<Slot>().instantiated_item.transform.SetParent(slts.GetSlots()[result].transform);
                
                slts.GetSlots()[result].GetComponent<Slot>().instantiated_item.transform.localPosition = Vector3.zero;
                //slts.GetSlots()[result].GetComponent<Slot>().slot = slot;


                amout = otherObjAmout;
                item_attached = otherObjItem;
                if(instantiated_item != null)
                {
                    Destroy(instantiated_item);
                }

                if (otherObjItem != null)
                {
                    instantiated_item = Instantiate(otherObjItem);
                    instantiated_item.transform.SetParent(transform);
                    instantiated_item.transform.localPosition = Vector3.zero;
                }
                else
                {
                    amout = 0;
                }

                //slot = otherObjSlot;
            }
            else if (result >= slts.numSlotsHotbar && result < slts.numSlotsHotbar + slts.numSlotsInventory) //To inventory
            {
                result -= slts.numSlotsHotbar;
                int otherObjAmout = slts.GetInvSlots()[result].GetComponent<Slot>().getAmout();
                GameObject otherObjItem = slts.GetInvSlots()[result].GetComponent<Slot>().item_attached;
                //int otherObjSlot = slts.GetInvSlots()[result].GetComponent<Slot>().slot;


                slts.GetInvSlots()[result].GetComponent<Slot>().amout = amout;

                slts.GetInvSlots()[result].GetComponent<Slot>().item_attached = item_attached;

                if (slts.GetInvSlots()[result].GetComponent<Slot>().instantiated_item != null)
                {
                    Destroy(slts.GetInvSlots()[result].GetComponent<Slot>().instantiated_item);
                }

                slts.GetInvSlots()[result].GetComponent<Slot>().instantiated_item = Instantiate(slts.GetInvSlots()[result].GetComponent<Slot>().item_attached);
                slts.GetInvSlots()[result].GetComponent<Slot>().instantiated_item.transform.SetParent(slts.GetInvSlots()[result].transform);

                slts.GetInvSlots()[result].GetComponent<Slot>().instantiated_item.transform.localPosition = Vector3.zero;
                //slts.GetInvSlots()[result].GetComponent<Slot>().slot = slot;


                amout = otherObjAmout;
                item_attached = otherObjItem;
                if (instantiated_item != null)
                {
                    Destroy(instantiated_item);
                }

                if (otherObjItem != null)
                {
                    instantiated_item = Instantiate(otherObjItem);
                    instantiated_item.transform.SetParent(transform);
                    instantiated_item.transform.localPosition = Vector3.zero;
                }
                else
                {
                    amout = 0;
                }

                //slot = otherObjSlot;
                //Debug.Log("New slot: " + slot + ".");
            }


            Destroy(temp);
        }
    }
}
