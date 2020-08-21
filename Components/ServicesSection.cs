using System.ComponentModel.Design;

namespace SewtrArtCentr.core.razor.Components
{
    ///<summary
    ///��������� ������������� ������ "Services" ������� ��������.
    ///</summary>
    ///
    [ViewComponent]

    public class ServicessSection:ViewComponent
    {
        private IServiceInfoService _serviceInfoService;

        public ServicessSection(IServiceInfoService serviceInfoService) => _serviceInfoService = serviceInfoService;

        public async Task<IViewComponentResult> InvokeAsync()

        {
            var model = await _serviceInfoService.GetServiceInfosAsync();
            return View(model);

        }
    }
}
