using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using EloBuddy;
using Kindroid.Classes;

namespace Kindroid
{
    class AutoBuy
    {
        public static int totalCostsItems = 0;
        public static int[] ItemListToBuild = { 1314, 3027, 3116 };
        public static List<Kindroid.Classes.Item> ItemsToBuy = new List<Kindroid.Classes.Item>();

        public List<Item> ItemList = new List<Item>();
        public int purchaseDelay = 500;
        public AutoBuy()
        {
            var json = Kindroid.Properties.Resources.items;
            dynamic stuff = JObject.Parse(json);
            foreach (var data in stuff.data.Children())
            {
                Item newItem = new Item();
                //Item Properties
                newItem.Id = Convert.ToInt32(data.Name);
                newItem.Name = data.First.name;
                newItem.Description = data.First.description;
                newItem.Plaintext = data.First.plaintext;
                //Item is Build FROM
                if (data.First.from != null)
                {
                    List<int> from = new List<int>();
                    foreach (var froms in data.First.from.Children())
                    {
                        from.Add(Convert.ToInt32(froms));
                    }
                    newItem.From = from.ToArray();
                }
                //Item can Build INTO
                if (data.First.into != null)
                {
                    List<int> into = new List<int>();
                    foreach (var intos in data.First.into.Children())
                    {
                        into.Add(Convert.ToInt32(intos));
                    }
                    newItem.Into = into.ToArray();
                }
                //Gold
                ItemGold newItemGold = new ItemGold();
                newItemGold.Base = data.First.gold["base"];
                newItemGold.Purchasable = data.First.gold["purchasable"];
                newItemGold.Total = data.First.gold["total"];
                newItemGold.Sell = data.First.gold["sell"];

                newItem.Gold = newItemGold;
                ItemList.Add(newItem);
            }
            foreach (int _ID in ItemListToBuild)
            {
                Kindroid.Classes.Item item = ItemList.FirstOrDefault<Kindroid.Classes.Item>(itm => itm.Id == _ID);
                ItemsToBuy.Add(item);
                totalCostsItems += item.Gold.Total;
            }
            Game.OnTick += Game_OnTick;
            Drawing.OnDraw += OnDraw;
        }

        void OnDraw(EventArgs eventArgs)
        {
            string text = "";
            text += "-------------------------------------";
            text +="\nTotal Costs: " + totalCostsItems + "\n";
            int index = 0;
            int multiplier = 13;
            foreach (Kindroid.Classes.Item _ITEM in ItemsToBuy)
            {
                Drawing.DrawText(35, (index * multiplier) + 85, System.Drawing.Color.Yellow, "\n" + _ITEM.Name + " : " + _ITEM.Gold.Base + "");
                if (_ITEM.From != null)
                {
                    foreach (int _ID in _ITEM.From)
                    {
                        index++;
                        Kindroid.Classes.Item item = ItemList.FirstOrDefault<Kindroid.Classes.Item>(itm => itm.Id == _ID);
                        Drawing.DrawText(35, (index * multiplier) + 85, System.Drawing.Color.White, "\n   >" + item.Name + " : " + item.Gold.Base + "");
                        if (item.From != null)
                        {
                            foreach (int _ID2 in item.From)
                            {
                                index++;
                                Kindroid.Classes.Item item2 = ItemList.FirstOrDefault<Kindroid.Classes.Item>(itm => itm.Id == _ID2);
                                Drawing.DrawText(35, (index * multiplier) + 85, System.Drawing.Color.Gray, "\n   >>" + item2.Name + " : " + item2.Gold.Base + "");
                            }
                        }
                    }
                }
                index++;
            }
            if (Player.Instance.IsDead || Classes.Utility.InFountain(Player.Instance))
            {
                text += "\nSHOPABLE";
            }
            else { text += "\nNOT SHOPABLE"; }
            Drawing.DrawText(35, (index * multiplier) + 105, System.Drawing.Color.Yellow, text);
        }
        void Game_OnTick(EventArgs args)
        {
            if (Player.Instance.IsDead || Classes.Utility.InFountain(Player.Instance) || Shop.CanShop)
            {
                try
                {
                    foreach (int Item in ItemListToBuild)
                    {
                        var hasItem = false;
                        Kindroid.Classes.Item _this = ItemList.FirstOrDefault<Kindroid.Classes.Item>(itm => itm.Id == Item);
                        foreach (InventorySlot IS in Player.Instance.InventoryItems)
                        {
                            if ((int)IS.Id == _this.Id)
                            {
                                hasItem = true;
                                break;
                            }
                        }
                        if (!hasItem)
                        {
                            if (Player.Instance.Gold >= _this.Gold.Base)
                            {
                                Shop.BuyItem(_this.Id);
                            }
                            else if (Player.Instance.Gold >= _this.Gold.Total)
                            {
                                Shop.BuyItem(_this.Id);
                            }
                            Chat.Print("Doesn't have Item [" + _this.Name + "], looking deeper.");
                            //Check if has a child item;
                            foreach (int child in _this.From)
                            {
                                var hasChildItem = false;
                                Kindroid.Classes.Item _thisChild = ItemList.FirstOrDefault<Kindroid.Classes.Item>(itm => itm.Id == child);
                                foreach (InventorySlot IS in Player.Instance.InventoryItems)
                                {
                                    if ((int)IS.Id == _thisChild.Id)
                                    {
                                        hasChildItem = true;
                                        break;
                                    }
                                }
                                if (!hasChildItem)
                                {
                                    Chat.Print("Doesn't have (Child)Item [" + _thisChild.Name + "], looking deeper.");
                                    //Check if child has a child item;
                                    if (Player.Instance.Gold >= _thisChild.Gold.Base)
                                    {
                                        Shop.BuyItem(_thisChild.Id);
                                    }
                                    else if (Player.Instance.Gold >= _thisChild.Gold.Total)
                                    {
                                        Shop.BuyItem(_thisChild.Id);
                                    }
                                    else
                                    {
                                        if (_thisChild.From != null)
                                        {
                                            foreach (int childChild in _thisChild.From)
                                            {
                                                var hasChildChildItem = false;
                                                Kindroid.Classes.Item _thisChildChild = ItemList.FirstOrDefault<Kindroid.Classes.Item>(itm => itm.Id == childChild);
                                                foreach (InventorySlot IS in Player.Instance.InventoryItems)
                                                {
                                                    if ((int)IS.Id == _thisChildChild.Id)
                                                    {
                                                        hasChildChildItem = true;
                                                        break;
                                                    }
                                                }
                                                if (!hasChildChildItem)
                                                {
                                                    Chat.Print("Doesn't have (ChildChild)Item [" + _thisChild.Name + "], should buy..");
                                                    if (Player.Instance.Gold >= _thisChildChild.Gold.Total)
                                                    {
                                                        Shop.BuyItem(_thisChildChild.Id);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (_thisChild.From == null)
                                    {
                                        if (Player.Instance.Gold >= (_this.Gold.Total - _thisChild.Gold.Base))
                                        {
                                            Shop.BuyItem(_this.Id);
                                        }
                                    }
                                    else
                                    {
                                        var childGoldTotal = 0;
                                        foreach (int childChild in _thisChild.From)
                                        {
                                            Kindroid.Classes.Item ChildOmfg = ItemList.FirstOrDefault<Kindroid.Classes.Item>(itm => itm.Id == childChild);
                                            childGoldTotal += ChildOmfg.Gold.Base;
                                        }
                                        if (Player.Instance.Gold >= (_this.Gold.Total - (_thisChild.Gold.Total - childGoldTotal)))
                                        {
                                            Shop.BuyItem(_this.Id);
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Chat.Print(e.Message);
                }
            }
        }
    }
}
