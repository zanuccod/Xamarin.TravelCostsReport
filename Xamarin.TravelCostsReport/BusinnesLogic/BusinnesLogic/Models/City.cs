using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace BusinnesLogic.Models
{
    public class City : IEquatable<City>
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public IEnumerable<CityItem> CityItems { get; set; }

        #region Constructor

        public City()
        {
            CityItems = Enumerable.Empty<CityItem>();
        }

        #endregion

        #region Overridden Methods

        public override bool Equals(object obj)
        {
            if (obj == null || !ReferenceEquals(obj, this))
            {
                return false;
            }
            return Equals(obj as City);
        }

        public bool Equals(City other)
        {
            if (other == null || CityItems.Count() != other.CityItems.Count())
            {
                return false;
            }

            var result = Name.Equals(other.Name);
            for (var idx = 0; idx < CityItems.Count(); idx++)
            {
                result &= CityItems.ElementAt(idx).Equals(other.CityItems.ElementAt(idx));
            }
            return result;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, CityItems);
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

        #endregion
    }
}
