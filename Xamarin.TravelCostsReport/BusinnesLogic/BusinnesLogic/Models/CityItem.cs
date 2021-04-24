using System;
using System.Text.Json;

namespace BusinnesLogic.Models
{
    public class CityItem : IEquatable<CityItem>
    {
        public string Name { get; set; }
        public int Distance { get; set; }

        #region Overridden Methods

        public override bool Equals(object obj)
        {
            if (obj == null || !ReferenceEquals(obj, this))
            {
                return false;
            }
            return Equals(obj as CityItem);
        }

        public bool Equals(CityItem other)
        {
            return other != null &&
                Name.Equals(other.Name) &&
                Distance == other.Distance;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Distance);
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

        #endregion
    }
}
