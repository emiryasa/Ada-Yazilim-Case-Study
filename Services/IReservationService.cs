using CaseStudy.Contracts;

namespace CaseStudy.Services;

public interface IReservationService
{
    ReservationResponse CreateReservation(ReservationRequest request);
}
