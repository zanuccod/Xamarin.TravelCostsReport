using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace BusinnesLogic.Dto
{
    public class CityDto : IEquatable<CityDto>
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public IEnumerable<CityItemDto> CityItems { get; set; }

        #region Constructor

        public CityDto()
        {
            CityItems = Enumerable.Empty<CityItemDto>();
        }

        #endregion

        #region Overridden Methods

        public override bool Equals(object obj)
        {
            if (obj == null || !ReferenceEquals(obj, this))
            {
                return false;
            }
            return Equals(obj as CityDto);
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

        #region Public Methods

        public bool Equals(CityDto other)
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

        public bool IsEmpty()
        {
            return string.IsNullOrEmpty(Name) &&
                Id == 0 &&
                !CityItems.Any();
        }

        #endregion
    }
}
