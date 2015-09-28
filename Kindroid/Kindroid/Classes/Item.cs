using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kindroid.Classes
{
    class Item
    {
        int id;
        string name;
        string group;
        string description;
        string colloq;
        string plaintext;
        int[] from;
        int[] into;
        ItemImage image;
        ItemGold gold;
        string[] tags;
        ICollection<KeyValuePair<Int32, Boolean>> maps = new Dictionary<Int32, Boolean>();
        ICollection<KeyValuePair<String, Int32>> stats = new Dictionary<String, Int32>();
        int depth;
        bool isChildChild = false;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Group
        {
            get { return group; }
            set { group = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public string Colloq
        {
            get { return colloq; }
            set { colloq = value; }
        }
        public string Plaintext
        {
            get { return plaintext; }
            set { plaintext = value; }
        }
        public int[] From
        {
            get { return from; }
            set { from = value; }
        }
        public int[] Into
        {
            get { return into; }
            set { into = value; }
        }
        public ItemImage Image
        {
            get { return image; }
            set { image = value; }
        }
        public ItemGold Gold
        {
            get { return gold; }
            set { gold = value; }
        }
        public string[] Tags
        {
            get { return tags; }
            set { tags = value; }
        }
        public ICollection<KeyValuePair<Int32, Boolean>> Maps
        {
            get { return maps; }
            set { maps = value; }
        }
        public ICollection<KeyValuePair<String, Int32>> Stats
        {
            get { return stats; }
            set { stats = value; }
        }
        public int Depth
        {
            get { return depth; }
            set { depth = value; }
        }
        public bool IsChildChild
        {
            get { return isChildChild; }
            set { isChildChild = value; }
        }
    }
    class ItemGold
    {
        int _base;
        bool purchasable;
        int total;
        int sell;
        public int Base
        {
            get { return _base; }
            set { _base = value; }
        }
        public bool Purchasable
        {
            get { return purchasable; }
            set { purchasable = value; }
        }
        public int Total
        {
            get { return total; }
            set { total = value; }
        }
        public int Sell
        {
            get { return sell; }
            set { sell = value; }
        }
    }
    class ItemImage
    {
        string full;
        string sprite;
        string group;
        float x;
        float y;
        float w;
        float h;
        public string Full
        {
            get { return full; }
            set { full = value; }
        }
        public string Sprite
        {
            get { return sprite; }
            set { sprite = value; }
        }
        public string Group
        {
            get { return group; }
            set { group = value; }
        }
        public float X
        {
            get { return x; }
            set { x = value; }
        }
        public float Y
        {
            get { return y; }
            set { y = value; }
        }
        public float W
        {
            get { return w; }
            set { w = value; }
        }
        public float H
        {
            get { return h; }
            set { h = value; }
        }
    }
}
