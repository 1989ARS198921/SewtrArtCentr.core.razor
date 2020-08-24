using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Buffers;
using System.Globalization;
using System.Threading.Tasks;

namespace SewtrArtCentr.core.razor
{
    ///<summary>
    ///����������� ������� ����� �������� ���������� ������ �������� ���� <typeparamref name="TEntity"/>.</summary>
    ///
    ///<typeparam name ="TEntity">��� ������ ����������� �������� .</typeparam>
    ///<typeparm name ="TKey">��� ������ �������������� ���������� ��������.</typeparm>

    public abstract class EditPageModelBase<TModel, TEntity, TKey> : PageModel
        where TModel : class
        where TEntity : BaseEntity<TKey>

    {
        private readonly IUserSrevice _userSrevice;

        ///<summary>
        ///������ �������� ������� ���������� ���������� ����� ��������� ���������� ��������.       ///</summary>

        public string RedrictUrl { get; protected set; }

        ///<summary>
        ///������ ������������� ������������ ���������.
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
        ///���������� ��������� �������� ���������� �������� ���� <typeparamref name="TEntity"/>�� ��������� ��������� �������� ������ �� ��������� ��������� �������������� ���� <typeparamref name="TKey"/>.
        ///����������� �����. ������ ���� ������������� � ������ ����������� ������.
        ///</summary>
        ///<param name="entityId">�������� �������������� ����<typeparamref name="TKey"/>���������� �������� ���� <typeparamref name="TEntity"/></param>
        ///<returns>������ �������� ���� <typeparamref name="TEntity"/> ��� null, ���� �������� �� ������� .</returns>

        protected abstract Task<TEntity> PerformGetSingleAsync(TKey entityId);



        ///<summary>
        ///������� ������ ������������� ���� <typeparamref name="TEntity"/>.</param>
        ///</summary>
        ///<returns>������ ��������� ������ ������������� ���� <typeparam name="TModel"/>.</returns>
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
        ///���������� ������� ����� ��������� �������� ���� <typeparamref name="TEntity"/> �� ��������� ������ ������ ������������� ���� <typeparamref name="TModel"/>. ����������� ����� ������ ���� ������������� � ������ ����������� ������ .
        ///
        ///</summary>
        ///<param name="model">������ ������ ������������ ���� <typeparamref name="TModel"/>.</param>
        ///<param name="craetor">������������ �������� ������, ��������� ����� �������� ���� <typeparamref name="TEntity"/>.</param>
        ///<returns>������ ��������� �������� ���� <typeparamref name="TEntity"/>.</returns>
        protected abstract Task<TEntity> CreateEntityModelAsync(TModel model, DomainUser creator);

        ///<summary>
        ///���������� ��������� �������� ���������� ������ �������� � ��������� ��������� � ��������� ��������� ���� <typeparamref name="TEntity"/>
        ///������ ������. ����������� ����� . ������ ���� ������������� � ������ ����������� ������.
        ///
        /// </summary>
        ///<param name="entity">����������� ������ �������� � ��������� ��������� ���� <typeparamref name="TEntity"/>
        ///�������� ������ . ����������� �����. ������ ���� ������������� � ������ ����������� ������.</param>
        ///<param name="entity"> ����������� ������ �������� ���� <typeparamref name="TEntity"/>.</param>

        protected abstract Task PerformUpdateAsync(TEntity entity);
    }
    
    



}