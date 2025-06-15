using AutoMapper;
using CoachlyBackEnd.Models;
using CoachlyBackEnd.Models.DTOs.UserDtos;
using CoachlyBackEnd.Models.Enums;
using CoachlyBackEnd.Models.Mapping;
using Microsoft.AspNetCore.Identity;


var context = new CoachlyDbContext();

// List<User> users = context.Users.ToList();
// List<Location> locations = context.Locations.ToList();
// List<Payment> payments = context.Payments.ToList();
// List<Session> sessions = context.Sessions.ToList();
// List<SessionParticipant> sessionParticipants = context.SessionParticipants.ToList();
// List<Specialization> specializations = context.Specializations.ToList();
// List<Trainer> trainers = context.Trainers.ToList();
// List<TrainerAvailability> trainerAvailabilities = context.TrainerAvailabilities.ToList();
// List<Subscribtion> subscribtions = context.Subscribtions.ToList();
//
// Console.WriteLine("User:");
// Console.WriteLine(users[0].FirstName + " " + users[0].LastName + " " + users[0].Email + " " + users[0].Phone + " " + users[0].Role);
// Console.WriteLine();
// Console.WriteLine("Location:");
// Console.WriteLine(locations[0].Country + " " + locations[0].City + " " + locations[0].Street + " " +
//                   locations[0].BuildingNumber + " " + locations[0].GymName);
// Console.WriteLine();
// Console.WriteLine("Payment:");
// Console.WriteLine(payments[0].Amount + " " + payments[0].PaymentDate + " " + payments[0].Method + " " + payments[0].Status + " " + payments[0].Currency);
// Console.WriteLine();
// Console.WriteLine("Session:");
// Console.WriteLine(sessions[0].Status + " " + sessions[0].Price + " " + sessions[0].Type + " " + sessions[0].ScheduledAt);
// Console.WriteLine();
// Console.WriteLine("SessionParticipant:");
// Console.WriteLine(sessionParticipants[0].Status + " " + sessionParticipants[0].JoinedAt);
// Console.WriteLine();
// Console.WriteLine("Specialization:");
// Console.WriteLine(specializations[0].Name);
// Console.WriteLine();
// Console.WriteLine("Trainer:");
// Console.WriteLine(trainers[0].Bio + " " + trainers[0].AvgRating);
// Console.WriteLine();
// Console.WriteLine("TrainerAvailability:");
// Console.WriteLine(trainerAvailabilities[0].DayOfWeek + " " + trainerAvailabilities[0].StartTime+ " " + trainerAvailabilities[0].EndTime);
// Console.WriteLine();
// Console.WriteLine("Subscribtion:");
// Console.WriteLine(subscribtions[0].Price + " " + subscribtions[0].ValidityPeriod + " " + subscribtions[0].Conditions);
// Console.WriteLine();

// var config = new MapperConfiguration(cfg =>
// {
//     cfg.AddProfile<MappingProfile>();
// });
//
// IMapper mapper = config.CreateMapper();
//
// var crud = new CrudService<User>(context,mapper );
//
// var hasher = new PasswordHasher<User>();
//
// UserCreateDto uDto = new UserCreateDto()
// {
//     FirstName = "John",
//     LastName = "Doe",
//     Email = "john.doe@example.com",
//     Phone = "+380987654321",
//     PasswordHash = hasher.HashPassword(null!, "koksaer123"),
//     Role = UserRole.Client
// };
//
// User u = mapper.Map<User>(uDto);
//
//
//
// await crud.CreateEntityAsync(u);