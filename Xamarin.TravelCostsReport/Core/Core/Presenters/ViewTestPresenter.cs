using System.Collections.Generic;
using System.Threading.Tasks;
using BusinnesLogic.Dto;
using BusinnesLogic.Services;
using Core.IViews;

namespace Core.Presenters
{
    public class ViewTestPresenter : IViewTestPresenter
    {
        private readonly ICityService testService;
        private readonly IViewTest testView;

        public ViewTestPresenter(IViewTest view, ICityService service)
        {
            testView = view;
            testService = service;
        }

        public Task<IEnumerable<CityDto>> GetItems()
        {
            return testService.FindAllAsync();
        }
    }
}
