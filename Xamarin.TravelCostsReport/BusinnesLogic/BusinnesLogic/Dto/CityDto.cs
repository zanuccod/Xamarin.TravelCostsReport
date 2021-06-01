using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.Json;

namespace BusinnesLogic.Dto
{
    public sealed class CityDto : IEquatable<CityDto>
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public IEnumerable<CityItemDto> CityItems { get; set; }

        public ICollection<int> TravelSteps { get; private set; }

        #region Constructor

        public CityDto()
        {
            CityItems = Enumerable.Empty<CityItemDto>();
            TravelSteps = new Collection<int>();
        }

        public CityDto(Collection<int> travelStep)
        {
            CityItems = Enumerable.Empty<CityItemDto>();
            TravelSteps = travelStep;
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

        public void AddTravelStep(int travelStep)
        {
            TravelSteps.Add(travelStep);
        }

        public void RemoveLastTravelStep()
        {
            if (TravelSteps.Count > 0)
            {
                TravelSteps.Remove(TravelSteps.Last());
            }    
        }

        public float GetDistanceTo(string cityName)
        {
            var targetCity = CityItems.FirstOrDefault(x => x.Name.Equals(cityName));
            return targetCity != null
                ? targetCity.Distance
                : 0;
        }

        #endregion
    }
}
