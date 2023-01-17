using Ardalis.GuardClauses;
using AVIV.Domain.Enums;
using AVIV.SharedKernel.Interface;
using System.ComponentModel.DataAnnotations.Schema;

namespace AVIV.Domain.Entities.Advertisement
{
    public class Advertisement : AuditableEntity, IAggregateRoot
    {
        public string Title { get; private set; }
        public string Description { get; private set; }

        // Use a value object
        public string Localisation { get; private set; }
        
        public PropertyType Type { get; private set; }
        public AdvertisementStatus Status { get; private set; }

        public Advertisement(string title, string description, string localisation, PropertyType type)
        {
            Title = Guard.Against.NullOrEmpty(title, nameof(title));
            Description = Guard.Against.NullOrEmpty(description, nameof(description));
            Localisation = Guard.Against.NullOrEmpty(localisation, nameof(localisation));
            Type = type;

            Status = AdvertisementStatus.VALIDATION_PENDING;
        }

        private Advertisement() { }


        public void UpdateTitle(string title)
        {
            Title = Guard.Against.NullOrEmpty(title, nameof(title));
        }

        public void UpdateDescription(string description)
        {
            Description = Guard.Against.NullOrEmpty(description, nameof(description));
        }


        public void UpdateStatus(AdvertisementStatus status)
        {
            Status = Guard.Against.Null(status, nameof(status));
        }
    }
}
