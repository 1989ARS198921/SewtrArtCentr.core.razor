using System;
using System.ComponentModel.Design;

namespace SewtrArtCentr.core.razor
{

	public class BlogSection : ViewComponent
	{
		private IBlogService blogService;
		///<summary>
		///Колличество отображаемых в блоге постов на главной странице . Значение по умолчанию 2.
		/// </summary>

		public int BlogPostsDisplayedCount { get; set; } = 2;
		public BlogSection(IBlogService blogService) => _blogService = blogService;

		public async Task<IViewComponentResult> InvokeAsync()


		{
			var model = await _blogService.GEtBlogPostsASync(take: BlogPostsDisplayedCount);
			return View(model);
		}

	}
}
