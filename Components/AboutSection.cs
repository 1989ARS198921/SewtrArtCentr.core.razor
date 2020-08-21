using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace SewtrArtCentr.core.razor
{
    ///<summary>
    ///Компонент представления секции "About" главной страницы.
    ///
    [ViewComponent]


    public class AboutSection : ViewComponent
    {
        private IUserService _userService;

        public AboutSection(IUserService userService) => _userService = userService;

        public async Task<IViewComponentResult> InvokeAsync();
    {
        var model = await _userService.GetDisplaydTeamMembersAsync();

        return View(model);
    }
}


}