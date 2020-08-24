using System.Dynamic;
using System.Net.NetworkInformation;

namespace SewtrArtCentr.core.razor
{


    ///<summary>
    ///Абстрактный базовый класс страници добавления новой сущности типа <typeparamref name="TEntity"/>
    ///</summary>
    ///<typeparam name="TModel"> Тип данных моделей представления, используемой страницей.</typeparam>
    ///<typeparam name="TEntity">Тип данных добовляемой сущности.</typeparam>
    ///<typeparam name="Tkey"> Тип данных идентификатора добовляемой сущности.</typeparam>
    
    public abstract class AddPageModelBase<TModel , Entity , Tkey>:AddPageModelBase
        where TModel : class, new ()
        where TEntity:BaseEntity<TKey>
    {
        private readonly IUService _userService;

    }
    /// <summary>
    /// Адресс страницы , которую необхадимо отразить после успешного выполнени операции.
    /// 
    /// </summary>
    public string RedrictUrl { get; protected set; }

    /// <summary>
    /// Модели представления используемой страницы.
    /// </summary>
    [BindProperty]public TModel Model { get; set; }
   


    public  AddPageModelBase(IUserService userService) => _userService = userService;

    public void OnGet() => AddPageModelBase = new TModel();
    
    public virtual async Task<IActionResult> OnPostAsync()
    {

        if (!ModelState.IsValid)
            return Page();

        var user = await _userService.GetUserAsync(user?.Identity?.Name);
        var entity = await CreateEntityFromModelAsync(Model, user);
        var result = await PerfomAddAsync(entity);
        this.SetDetails(OperationalStatus.Succsess, $The entity of type '{typeof(TEntity)}' with key value '{result.Id }' for "+" +
            "$"'{nameof(BaseEntity<TKey>.Id}' saved successfully.");
    }

    ///<summary>
    ///Асинхронно создает новый экзэмпляр сущности типа <typeparamref name="TEntity"/>
    ///на основании данных модели представления  типа <typeparamref name="TModel"/>Абстрактынй метод .
    ///Должен быть переопределен в каждом производном классе.
    ///
    /// </summary>
    ///<param name="model">Данные модели представления типа <typeparamref name="TModel"/>.</param>
    ///<param name="creator"> Пользователь домена модели , создающий новую сущность типа <typeparamref name="TEntity"/>.</param>
    ///<returns>Объект созданнной сущности типа <typeparamref name="TEntity"/>.</returns>
    protected abstract Task<TEntity> CreateEntityFromModelAsync(TModel model, DomainUser creator);


    ///<summary>
    ///Асинхронно выполняет операцию добавления сущности в коллекцию сущностей типа <typeparamref name="TEntity"/>.</param>
    ///<returns> Объект добовения сущности типа <typeparamref name="TEntity"/>.</returns>
    protected abstract Task<TEntity> PerformAddAsync(TEntity entity);
}

}
