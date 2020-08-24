using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SewtrArtCentr.core.razor.Areas.Admin
{
    public static class PageModelExtensions
    {
        ///<summary>
        ///Добавляет информацию об операции в коллекцию <see cref="ITempDataDictionary"/>страницы.
        ///</summary>
        ///<param name="status">Статус операции.</param>
        ///<param name="message">Текст сообщения.</param>
        ///

        public static void SetDetails(this PageModel page, OperationStatus status, string message)
        {
            page.TempData.Set("Details", new OperationDetails
            { 
                Status = status,
                message = message
             });

        }

    }
}
