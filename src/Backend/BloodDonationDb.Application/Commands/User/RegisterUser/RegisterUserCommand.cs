﻿using BloodDonationDb.Application.Models;
using BloodDonationDb.Application.Models.User;
using MediatR;

namespace BloodDonationDb.Application.Commands.User.RegisterUser;
public class RegisterUserCommand : IRequest<ResultViewModel<ResponseRegisterUser>>
{   
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }

    public Domain.Entities.User ToEntity(string password)
        => new(Name, Email, password);
}
