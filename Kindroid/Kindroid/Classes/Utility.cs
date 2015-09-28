using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Utils;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kindroid.Classes
{
    class Utility
    {
        public static bool InFountain(AIHeroClient hero)
        {
            float fountainRange = 562500; //750 * 750
            Vector3 vec3 = (hero.Team == GameObjectTeam.Order) ? new Vector3(363, 426, 182) : new Vector3(14340, 14390, 172);
            var map = EloBuddy.Game.MapId;
            if (map == GameMapId.SummonersRift)
            {
                fountainRange = 1102500; //1050 * 1050
            }
            return hero.IsVisible && hero.Distance(vec3, true) < fountainRange;
        }
    }
}
