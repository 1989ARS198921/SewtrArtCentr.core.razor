
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace SewtrArtCentr.core.razor.Areas.Admin
{
    ///<summary>
    ///Абстрактный базовый класс страницы отображения коллекции сущностей типа <typeparamref name="TEntity">.</summary>
    /// </summary>   
    ///<typeparam name="TEntity">Туип данных сущности.</typeparam>
    ///<typeparam name="TKey">Тип данны идентификатора сущности.</typeparam>
    ///


    public abstract class ListPageModelBase<TEntity, Tkey> : PageModel where TEntity : BaseEntity<Tkey>
    {
        ///<summary>
        ///Адрес страницы , которую необхадтимо отобразить после выполнения операции.
        /// </summary>
        public IList<TEntity> Model { get; set; }

        public virtual async Task OnGetAsync() => Model = await PerformGetManyAsync();

        ///<summary>
        ///Асинхронно выполняет опрецию извлечения коллекции сущностей типа <typeparamref name="TEntity"/> из доменной модели.
        ///Абстрактный метод должен быть переопределен в каждом производном классе.
        ///
        /// 
        /// </summary>

        protected abstract Task<IActionResult> OnPostDeleteAsync();

        public virtual async Task<IActionResult> OnPostDeleteAsync(Tkey id)
        {
            if (ModelState.IsValid)
            {
                await PerformDeleteAsync(id);
                this.SetDetails(OperationalStatus.Success, $"The entity of type '{typeof(TEntity)}'with key value '{id}' for " + $"'{nameof(BaseEntity<TKey>.Id)}' deleted successfully.");
            }
            else
            {
                this.SetDetails(OperationStatus.Error, $"Unable to delete entity of type '{typeof(TEntity)}' with key value '{id}'" + $"for '{nameof(BaseEntity<TKey>.Id)}'. Reload the page and try again.");


            }
            return RedirectToPage(RedirectUrl);
        }


        ///<summary>
        ///Асинхронно удаление сущности с заданным значением и дентификатора типа <typeparmref name ="TEntity"/> доменной модели.    
        //////</summary>
        ///<param name="entityId">  Знаяение идентикатора типа <typeparamref name="TKey"/> сущности типа
        ///<typeparamref name="TEntity"/>.</param>
        ///

        protected abstract Task PerformDeleteAsync(TKey entityId);
    }
}

