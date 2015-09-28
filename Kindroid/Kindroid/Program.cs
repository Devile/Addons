using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using SharpDX;
using ClipperLib;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace Kindroid
{
    class Program
    {
        static void Main(string[] args)
        {
            Hacks.RenderWatermark = false;
            Loading.OnLoadingComplete += delegate
            {
                Chat.Print("Kindroid - Init [" + Player.Instance.ChampionName + "]");
                AutoLevel AL = new AutoLevel();
                AutoBuy AB = new AutoBuy();
                Menu _menu = MainMenu.AddMenu("Kindroid Bot", "Kindroid");
                _menu.Add("Test", new Slider(""));
            };
        }
    }
}
