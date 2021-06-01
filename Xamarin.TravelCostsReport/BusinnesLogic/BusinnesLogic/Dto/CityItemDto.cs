using System;
using System.Text.Json;

namespace BusinnesLogic.Dto
{
    public sealed class CityItemDto : IEquatable<CityItemDto>
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
            return Equals(obj as CityItemDto);
        }

        public bool Equals(CityItemDto other)
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
