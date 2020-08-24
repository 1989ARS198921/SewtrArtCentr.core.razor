using System.Dynamic;
using System.Net.NetworkInformation;

namespace SewtrArtCentr.core.razor
{


    ///<summary>
    ///����������� ������� ����� �������� ���������� ����� �������� ���� <typeparamref name="TEntity"/>
    ///</summary>
    ///<typeparam name="TModel"> ��� ������ ������� �������������, ������������ ���������.</typeparam>
    ///<typeparam name="TEntity">��� ������ ����������� ��������.</typeparam>
    ///<typeparam name="Tkey"> ��� ������ �������������� ����������� ��������.</typeparam>
    
    public abstract class AddPageModelBase<TModel , Entity , Tkey>:AddPageModelBase
        where TModel : class, new ()
        where TEntity:BaseEntity<TKey>
    {
        private readonly IUService _userService;

    }
    /// <summary>
    /// ������ �������� , ������� ���������� �������� ����� ��������� ��������� ��������.
    /// 
    /// </summary>
    public string RedrictUrl { get; protected set; }

    /// <summary>
    /// ������ ������������� ������������ ��������.
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
    ///���������� ������� ����� ��������� �������� ���� <typeparamref name="TEntity"/>
    ///�� ��������� ������ ������ �������������  ���� <typeparamref name="TModel"/>����������� ����� .
    ///������ ���� ������������� � ������ ����������� ������.
    ///
    /// </summary>
    ///<param name="model">������ ������ ������������� ���� <typeparamref name="TModel"/>.</param>
    ///<param name="creator"> ������������ ������ ������ , ��������� ����� �������� ���� <typeparamref name="TEntity"/>.</param>
    ///<returns>������ ���������� �������� ���� <typeparamref name="TEntity"/>.</returns>
    protected abstract Task<TEntity> CreateEntityFromModelAsync(TModel model, DomainUser creator);


    ///<summary>
    ///���������� ��������� �������� ���������� �������� � ��������� ��������� ���� <typeparamref name="TEntity"/>.</param>
    ///<returns> ������ ��������� �������� ���� <typeparamref name="TEntity"/>.</returns>
    protected abstract Task<TEntity> PerformAddAsync(TEntity entity);
}

}
