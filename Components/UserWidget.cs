using System.ComponentModel.Design;

namespace SewtrArtCentr.core.razor.Components

{
    ///<summary>
    ///Компонент представления виджета информации о текущем футентифицированном пользователе.
    ///Виджет состоит из фото пользователя , идентификационного имени пользователя и кнопки выхода.
    ///</summary>
    ///

    [ViewComponent]

    public class UserWidget:ViewComponent
    {
        private IUserService _userService;

        public UserWidget(IUserService userService) => _userService = userService;

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userService.GetUserAsync(User?.Identity?.Name);
            return View((object)user.Profile.PhotoFilePath);
        }
    }
}

