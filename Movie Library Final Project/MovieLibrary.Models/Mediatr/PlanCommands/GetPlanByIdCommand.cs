﻿using MediatR;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.Responses;

namespace MovieLibrary.Models.Mediatr.PlanCommands
{
    public record GetPlanByIdCommand(int planId) : IRequest<HttpResponse<Plan>>
    {
    }
}
