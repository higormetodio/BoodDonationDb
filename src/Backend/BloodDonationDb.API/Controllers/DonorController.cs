﻿using BloodDonationDb.API.Attributes;
using BloodDonationDb.Application.Commands.Donor.Register;
using BloodDonationDb.Application.Models;
using BloodDonationDb.Application.Models.Donor;
using BloodDonationDb.Application.Queries.Donor.GetDonor;
using BloodDonationDb.Application.Queries.Donor.GetDonorDonationsByEmail;
using BloodDonationDb.Comunication.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationDb.API.Controllers;

[AuthenticatedUser]
public class DonorController : MyBloodDonationDbController
{
    private readonly IMediator _mediator;

    public DonorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("{email}")]
    [ProducesResponseType(typeof(DonorViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorViewModel), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDonor([FromRoute]string email)
    {
        var query = new GetDonorByEmailQuery(email);

        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpGet()]
    [Route("donations/{email}")]
    [ProducesResponseType(typeof(DonorDonationsViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorViewModel), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDonorDonations([FromRoute] string email)
    {
        var query = new GetDonorDonationsByEmailQuery(email);

        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(RegisterDonorViewModel), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorViewModel), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterDonor([FromBody]RegisterDonorCommand command)
    {
        var result = await _mediator.Send(command);

        return Created(string.Empty, result);
    }
}
