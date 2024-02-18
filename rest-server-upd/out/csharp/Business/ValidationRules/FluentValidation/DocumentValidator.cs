

using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class DocumentValidator : AbstractValidator<Document>
    {
        public DocumentValidator()
        {
            RuleFor(p => GetExtension(p.Title)).Must(IsImage).WithMessage("Uploaded file has to be an image.");
        }

        private bool IsImage(string docExtension)
        {
            var docExtensionUpper = docExtension.ToUpper();
            switch (docExtensionUpper)
            {
                case ".JPEG":
                case ".PNG":
                case ".JPG":
                case ".TIFF":
                    return true;
                default:
                    return false;
            }
        }


        public static string GetExtension(string name)
        {
            int index = name.LastIndexOf(".");

            if (index == -1)
                return "";

            if (index == name.Length - 1)
                return "";

            return name.Substring(index);
        }
    }
}
