using Ardalis.Specification;
using AVIV.Domain.Entities.Advertisement;

namespace AVIV.Core.Features.Advertisements.Specifications
{
    public class AllAdvertisementByIdSpecification : Specification<Advertisement>, ISingleResultSpecification
    {
        public AllAdvertisementByIdSpecification(string advertisementId)
        {
            Query
                .Where(a => a.Id == advertisementId);
        }
    }
}
