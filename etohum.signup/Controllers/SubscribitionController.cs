using etohum.signup.Entities;
using etohum.signup.Models;
using etohum.signup.Services;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace etohum.signup.Controllers
{
    // using conviction based routing as set in startup.cs
    public class SubscribitionController : Controller
    {
        private IEtohumRepository _etohumRepository;
        private IMailService _mailService;

        // calling my services using depenceny injection
        public SubscribitionController(IEtohumRepository etohumRepository, IMailService mailService)
        {
            _etohumRepository = etohumRepository;
            _mailService = mailService;
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(SignUpViewModel viewModel)
        {
            // if data binding could not parse the model
            if (viewModel == null)
                return BadRequest();
            // if validation rules are voilated
            if (!ModelState.IsValid)
                return View(viewModel);

            // custom validation for unique email addresses
            if (_etohumRepository.AppUserEmailExists(viewModel.Email))
            {
                ModelState.AddModelError("Email", "That email address already exists.");
                return View(viewModel);
            }

            // creatig the entity 
            var appUser = new AppUser
            {
                Name = viewModel.Name,
                Surname = viewModel.Surname,
                Email = viewModel.Email,
                CreatedAt = DateTimeOffset.Now
            };

            // adding the entity
            _etohumRepository.AddAppUser(appUser);

            // if the user had written a freind's email the invitaion entity will be created and added
            Invitation invitation = null;
            if (!string.IsNullOrEmpty(viewModel.FreindEmail))
            {
                invitation = new Invitation
                {
                    Invitee = viewModel.FreindEmail,
                    InviterId = appUser.Id,
                    InvitedAt = DateTimeOffset.Now
                };
                _etohumRepository.AddInvitation(invitation);
            }

            // changes kept tracked and here we save to database permenantly
            if (!_etohumRepository.SaveChanges())
            {
                throw new Exception("there was an error while saving, SignUp");
            }
           
            // after saving the entities to database i call my mail service and send the message to queue
            _mailService.SendMailToQueue($"Welcome On Board {appUser.Name} {appUser.Surname}",
                "Thanks for siging up to our service, hope you like it!",
                appUser.Email);

            if(invitation != null)
            {
                _mailService.SendMailToQueue("Invitation To A New World!",
                $"You have been invited to our service by your friend {appUser.Name} {appUser.Surname}. We Hope To See You Soon!",
                invitation.Invitee);
            }
            
            // while sending email proccess is working in the backgound, i send the user a welcome page
            return View("SignUpDone", viewModel);
        }

        // error page
        public IActionResult Error()
        {
            return View();
        }

    }
}
