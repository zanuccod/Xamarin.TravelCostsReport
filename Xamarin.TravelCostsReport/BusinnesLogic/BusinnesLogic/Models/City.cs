using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinnesLogic.Models
{
    public class City : IEquatable<City>
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public IEnumerable<CityItem> CityItems { get; set; }

        public City()
        {
            CityItems = Enumerable.Empty<CityItem>();
        }
        public override bool Equals(object obj)
        {
            if (obj == null || !ReferenceEquals(obj, this))
            {
                return false;
            }
            return Equals(obj as City);
        }
        public bool Equals(City city)
        {
            var result = true;
            result &= Name.Equals(city.Name);

            if (CityItems.Count() != city.CityItems.Count())
                return false;

            for(var idx = 0; idx < CityItems.Count(); idx++)
            {
                result &= CityItems.ElementAt(idx).Equals(city.CityItems.ElementAt(idx));
            }

            return result;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, CityItems);
        }
    }
}
