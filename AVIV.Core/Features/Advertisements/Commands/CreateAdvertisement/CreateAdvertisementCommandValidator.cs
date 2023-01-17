using FluentValidation;

namespace AVIV.Core.Features.Advertisements.Commands.CreateAdvertisement
{
    public class CreateAdvertisementCommandValidator : AbstractValidator<CreateAdvertisementCommand>
    {
        public CreateAdvertisementCommandValidator()
        {
            // TODO : fichier resource variables translation
            RuleFor(v => v.Title)
                .MaximumLength(200)
                .MinimumLength(2)
                .NotEmpty()
                .WithName("Titre")
                .WithMessage("Le titre est obligatoire et ne doit pas dépasser 200 caractères.");

            // TODO : fichier resource variables translation
            RuleFor(v => v.Description)
                .MaximumLength(200)
                .MinimumLength(15)
                .NotEmpty()
                .WithName("Description")
                .WithMessage("La description est obligatoire et doit comporter entre 15 et 200 caractères.");

            RuleFor(v => v.Localisation)
                .MaximumLength(20)
                .MinimumLength(2)
                .NotEmpty()
                .WithName("Titre")
                .WithMessage("La localisation est obligatoire et doit comporter entre 2 et 20 caractères.");
        }
    }

}
