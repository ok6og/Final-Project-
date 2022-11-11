﻿using MediatR;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.Responses;

namespace MovieLibrary.Models.Mediatr.SubscriptionCommands
{
    public record GetAllSubscriptionsCommand : IRequest<HttpResponse<IEnumerable<Subscription>>>
    {
    }
}
