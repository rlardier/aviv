using Ardalis.Specification;
using AVIV.Domain.Entities.Advertisement;

namespace AVIV.Core.Features.Advertisements.Specifications
{
    public class AdvertisementPublishedByIdSpecification : Specification<Advertisement>, ISingleResultSpecification
    {
        public AdvertisementPublishedByIdSpecification(string advertisementId)
        {
            Query
                .Where(a => a.Status == Domain.Enums.AdvertisementStatus.VALIDATED)
                .Where(a => a.Id == advertisementId);
        }
    }
}
