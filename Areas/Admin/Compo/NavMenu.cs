using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SewtrArtCentr.core.razor.Areas.Admin.Compo
{

    ///<summary>
    ///Добавление представления навигационного меню фдминистративной части сайта.
    ///</summary>
    [ViewComponent]

    public class NavMenu : ViewComponent
    {
        private IMessageService _messageService;

        public NavMenu(IMessageService messageService) => _messageService = messageService;

        public async Task <IViewComponentResult> InvokeAsync()
        {
            // общие сылки для всех пользователей.
            var model = new NavMenuViewModel
            {
                NavLinks = new List<NavLink>
                {
                    new NavLink {Href="/MyProfile", Text = "My Profile"},
                    new NavLink {Href="/Services", Text = "Services"},
                    new NavLink {Href="/Users", Text = "Users"},
                    new NavLink {Href="/Works", Text = "Works"},
                    new NavLink {Href="/Blog", Text = "Blog"},
                    new NavLink {Href="/Brands", Text = "Brands"},
                    new NavLink {Href="/Testimonails", Text = "Testimonails"},


                }
            };

            //Для администратора в навигационном меню отображается две дополнительные ссылки, а так же колличство новых непрочитанных соообений от пользователей.

            if (User.IsInRole("Administrator"))
            {
                model.NavLinks.Add(new NavLink { Href = "/Contacts", Text = "Contacts" });
                model.NavLinks.Add(new NavLink { Href = "/Message ", Text = "Messages" });

                 .Where(_message => !_message.IsRead)
                 .Count();
            }
            return View(model);


        }

    }





}
