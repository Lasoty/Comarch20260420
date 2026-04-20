using MediatR;

namespace CarSharing.Modules.Reservations.Application.CreateReservation;

public sealed class CreateReservationCommandHandler(
    IReservationWorkflowService reservationWorkflowService)
    : IRequestHandler<CreateReservationCommand, Guid>
{
    public Task<Guid> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        => reservationWorkflowService.CreateAsync(request, cancellationToken);
}
