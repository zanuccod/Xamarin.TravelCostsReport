using System;
using System.Text.Json;

namespace BusinnesLogic.Models
{
    public class CityItem : IEquatable<CityItem>
    {
        public string Name { get; set; }
        public int Distance { get; set; }

        #region Constructor

        public CityItem()
        {
            Name = string.Empty;
        }

        #endregion

        #region Overridden Methods

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is CityItem))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
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
