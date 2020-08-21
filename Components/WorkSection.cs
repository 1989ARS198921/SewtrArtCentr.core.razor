using System.ComponentModel.Design;

namespace SewtrArtCentr.core.razor.Components

{
	///<summary>
    ///��������� ������������� ������ "Works" ������� ��������.
    ///</summary>
    ///
    [ViewComponent]

    public class WorkSection:ViewComponent
    {
        private IWorkExampleService _workExampleService;

        public WorkSection(IWorkExampleService workExampleService) => _workExampleService = workExampleService;

        public async Task<IViewComponentResult> InvokeAsync()

        {
            var workExample = await _workExampleService.GetWorkExampleAsync();
            var model = new WorkSectionViewModel
            {
                WorkExample = workExample
                .Select(workExample => workExample.Category)
                .Distinct()
                .Prepend("*")
                .ToList()
            };

            return View(model);
        }
    }
}