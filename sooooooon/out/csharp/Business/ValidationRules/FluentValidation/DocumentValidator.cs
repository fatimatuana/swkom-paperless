

using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class DocumentValidator : AbstractValidator<Document>
    {
        public DocumentValidator()
        {
            RuleFor(p => p.Title).Must(EndsWithTxt).WithMessage("document has to end with .txt");
            //RuleFor(p => p.ProductName).MinimumLength(2);
            RuleFor(p => p.Title).Must(StartWithA).WithMessage("documents has to start with A");
        }

        private bool EndsWithTxt(string arg)
        {
            return arg.EndsWith("t");
        }

        private bool StartWithA(string arg)
        {
            return arg.StartsWith("A");
        }
    }
}
