
using SupportTicketSystem.Domain.Features.Customers;
using static SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationEnums.TicketPriorityEnum;

namespace SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistration_dtos;

//Add record for practice. It will transfer the data the user input from model to other methods so they can "view" the data through the record instead of the direct modeldata. Not sure why not also include id, status and date. 
public record TicketRegistrationRequest
(
    string TicketTitle,
    string TicketDescription,
    Customer TicketCustomer,
    TicketPriority TicketPriority
);
