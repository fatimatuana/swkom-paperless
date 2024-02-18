

using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class DocumentValidator : AbstractValidator<Document>
    {
        public DocumentValidator()
        {
            RuleFor(p => p.DocumentExtension).Must(IsImage).WithMessage("Uploaded file has to be an image.");
        }

        private bool IsImage(string docExtension)
        {
            var docExtensionUpper = docExtension.ToUpper();
            switch (docExtensionUpper)
            {
                case "JPEG":
                case "PNG":
                case "JPG":
                case "TIFF":
                    return true;
                default:
                    return false;
            }
        }
    }
}
