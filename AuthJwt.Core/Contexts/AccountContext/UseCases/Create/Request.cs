﻿using MediatR;

namespace AuthJwt.Core.Contexts.AccountContext.UseCases.Create;

public record Request(
    string Name,
    string Email,
    string Password) : IRequest<Response>;
