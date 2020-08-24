using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Buffers;
using System.Globalization;
using System.Threading.Tasks;

namespace SewtrArtCentr.core.razor
{
    ///<summary>
    ///Абстрактный базовый класс странциы обновления данных сущности типа <typeparamref name="TEntity"/>.</summary>
    ///
    ///<typeparam name ="TEntity">Тип данных обновляемой сущности .</typeparam>
    ///<typeparm name ="TKey">Тип данных идентификатора оновляемой сущности.</typeparm>

    public abstract class EditPageModelBase<TModel, TEntity, TKey> : PageModel
        where TModel : class
        where TEntity : BaseEntity<TKey>

    {
        private readonly IUserSrevice _userSrevice;

        ///<summary>
        ///Адресс страницы которую необхадимо отобразить после успешного выполнения операции.       ///</summary>

        public string RedrictUrl { get; protected set; }

        ///<summary>
        ///Модель представления используемой страницей.
        ///</summary>
        [BindProperty]

        public TModel Model { get; set; }

        public EditPageModelBase(IUserService userService) => _userSrevice = userService;
        public async Task<IActionResult> OnGetAsync(TKey id)
        {
            var entity = await PerformGetSingleAsync(id);
            if (entity == null)
                return NotFound();
            Model = CreateModelFromEntity(entity);
            return Page();


        }

        ///<summary>
        ///Асинхронно выполняет операцию извлечения сущности типа <typeparamref name="TEntity"/>из коллекции сущностей доменной модели по заданному знацчению идентификатора типа <typeparamref name="TKey"/>.
        ///Абстрактный метод. Должен быть переопределен в каждом производном классе.
        ///</summary>
        ///<param name="entityId">Занчение идентификатора типа<typeparamref name="TKey"/>извлкаемой сущности типа <typeparamref name="TEntity"/></param>
        ///<returns>Объект сущности типа <typeparamref name="TEntity"/> или null, если сущность не найдена .</returns>

        protected abstract Task<TEntity> PerformGetSingleAsync(TKey entityId);



        ///<summary>
        ///Создает модель представления типа <typeparamref name="TEntity"/>.</param>
        ///</summary>
        ///<returns>Объект созданной модели представления типа <typeparam name="TModel"/>.</returns>
        protected abstract TModel CreateModelFromEntity(TEntity entity);


        public virtual async Task<IActionResult> OnPostAsync()
        {
            if (!ModelStateDictionary.IsValid)
                return Page();
            var user = await _userSrevice.GetUserAsync(User?.Identity?.Name);
            var entity await CreateEntityFromModelAsync(Model, user);
            await PerformUpdateASync(entity);
            this.SetDetails(OperationStatus.Success, $"The entity of type '{typeof(TEntity)}' with key value '{entity.Id}' for " +
                $"'{nameof(BaseEntity<TKey>.Id)}' update succsessfully.");
            return RedrictToPage(RedrictUrl);

        }


        ///<summary>
        ///Асинхронно создает новый экзэмпляр сущности типа <typeparamref name="TEntity"/> на основании данных модели представления типа <typeparamref name="TModel"/>. Абстрактный метод должен быть преопредеолен в каждом производном классе .
        ///
        ///</summary>
        ///<param name="model">Данная модель представлени типа <typeparamref name="TModel"/>.</param>
        ///<param name="craetor">Пользователь доменной модели, создающий новую сущность типа <typeparamref name="TEntity"/>.</param>
        ///<returns>Объект созданной сущности типа <typeparamref name="TEntity"/>.</returns>
        protected abstract Task<TEntity> CreateEntityModelAsync(TModel model, DomainUser creator);

        ///<summary>
        ///Асинхронно выполняет операцию обновления данных сущности в коллекции сущностей в коллекции сущностей типа <typeparamref name="TEntity"/>
        ///Данной модели. Абстрактный метод . Должен быть переопределен в каждом производном классе.
        ///
        /// </summary>
        ///<param name="entity">Обновленные данных сущности в коллекции сущностей типа <typeparamref name="TEntity"/>
        ///доменной модели . Абстрактный метод. Должен быть переопределен в каждом производном классе.</param>
        ///<param name="entity"> Обновленные данные сущности типа <typeparamref name="TEntity"/>.</param>

        protected abstract Task PerformUpdateAsync(TEntity entity);
    }
    
    



}