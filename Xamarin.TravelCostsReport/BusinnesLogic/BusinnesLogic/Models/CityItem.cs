using System;
using System.Collections.Generic;
using System.Text;

namespace BusinnesLogic.Models
{
    public class CityItem : IEquatable<CityItem>
    {
        public string Name { get; set; }
        public int Distance { get; set; }
        public override bool Equals(object obj)
        {
            if (obj == null || !ReferenceEquals(obj, this))
            {
                return false;
            }
            return Equals(obj as CityItem);
        }
        public bool Equals(CityItem cityItem)
        {
            return Name.Equals(cityItem.Name)
                && Distance == cityItem.Distance;
        }
    }
}
