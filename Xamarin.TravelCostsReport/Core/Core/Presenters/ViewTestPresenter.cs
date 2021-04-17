using System.Collections.Generic;
using BusinnesLogic.Services;
using Core.IViews;

namespace Core.Presenters
{
    public class ViewTestPresenter : IViewTestPresenter
    {
        private readonly ITestService testService;
        private readonly IViewTest testView;

        public ViewTestPresenter(IViewTest view, ITestService service)
        {
            testView = view;
            testService = service;
        }

        public IEnumerable<int> GetItems()
        {
            return testService.GetAll();
        }
    }
}
