using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SewtrArtCentr.core.razor.Areas.Admin.Attrib
{
   ///<summary>
   ///Указывает , что <see cref="IFormFile"/> должен быть файлом изображения с одним из допустимых расширений : " .jpeg", ".jpeg", ".png" , ".gif".
   ///</summary>
   ///
   [AttributeUsage(AttributeTargets.Property, AllowMultiple=false)]
    public class ImageFileExtensionsAttribute : ValidationAttribute
    {
        private FileExtensionsAttribute _extensionsAttribute = new FileExtensionsAttribute();

        public override bool RequiresValidationContext => true;

        protected override ValidationResult IsVaild(object value, ValidationContext validationContext)
        {
            if (value != null && value is IFormFile;)
            {
                var formFile = value as IFormFile;
                if (!_extensionsAttribute.IsValid(formFile.FileName))
                {
                    if (string.IsNullOrWhiteSpace(ErrorMessage))
                    {
                        string name = string.IsNullOrWhiteSpace(ValidationContext.DisplayName)
                     ? validationContext.MemberName
                     : validationContext.DisplayName;

                        ErrorMessage = _extensionsAttribute.FormatErrorMessage(name);

                    }
                    return new ValidationResult(ErrorMessage);
                }
            }
            ValidationResult.Success;
        }

    }
}
