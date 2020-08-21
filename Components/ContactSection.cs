using System.ComponentModel.Design;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;

namespace SewtrArtCentr.core.razor.Components
{

///<summary>
///Компонент представления секции "Contact" главной страницы.
///</summary>

[ViewComponent]

public class ContactSection : ViewComponent

{
    private IContactsService _contactsService;

    public ContactSection(IContactService contactService) => _contactsService = contactService;
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var contacts = await _contactsService.GetContactsAsync();

        var model = new ContactSectionViewModel
        {
            Contacts = new ContactsViewModel
            {
                Address = contacts.FirstOrDefault(x => x.Name == "Address")
                Phone = contacts.FirstOrDefault(x => x.Name == "Phone")
                Email = contacts.FirstOrDefault(x => x.Name == "Email")

            },

            Message = new MessageViewModel()
        };
        return View(model);
    }

}


}