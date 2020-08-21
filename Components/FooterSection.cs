using System.ComponentModel.Design;

namespace SewtrArtCentr.core.razor.Components
{
    ///<summary>
    ///��������� ������������� ������ ������� ��������.
    ///</summary>
    ///
    [ViewComponent]

    public class FooterSection:ViewComponent
    {
        private IContactsService _contactsService;

        public FooterSection(IContactsService contactsService) => _contactsService = contactsService;

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _contactsService.GetSocialLinksAsync();
            return View(model);
        }
    }
}