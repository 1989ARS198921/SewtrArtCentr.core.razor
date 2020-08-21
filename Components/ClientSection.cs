using System.Threading.Tasks;
using System.Management.Instrumentation;
using Microsoft.AspNetCore.Mvc;
using SewtrArtCentr.core.razor.ViewModels;
using SewtrArtCentr.Domain.Abstractions.Services;



namespace SewtrArtCentr.core.razor

{
    ///<summary>
    ///Компонент представления секции "Clients" главной страницы.
    ///</summary>
    ///
    [ViewComponent]

    public class ClientsSection:ViewComponent
    {
        private IBrandService _brandService;
        private ITestimonialService _testimonialService;


        public ClientsSection(IBrandService brandService , ITestimonialService testimonialService)

        {
            _brandService = brandService;
            _testimonialService = testimonialService;

        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new ClientsSectionViewModel
            {
                Brands = await _brandService.GetBrandsAsync();
            Testimonals = await _testimonialService.GetTestimonialsAsync()
            };
        return(model);
        }
    }

    
}