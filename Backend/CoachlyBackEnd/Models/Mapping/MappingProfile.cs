using AutoMapper;
using CoachlyBackEnd.Models.DTOs.Location;
using CoachlyBackEnd.Models.DTOs.Payment;
using CoachlyBackEnd.Models.DTOs.Review;
using CoachlyBackEnd.Models.DTOs.Session;
using CoachlyBackEnd.Models.DTOs.SessionParticipants;
using CoachlyBackEnd.Models.DTOs.Specialization;
using CoachlyBackEnd.Models.DTOs.Subscribtion;
using CoachlyBackEnd.Models.DTOs.TrainerAvailability;
using CoachlyBackEnd.Models.DTOs.TrainerDocument;
using CoachlyBackEnd.Models.DTOs.TrainerDtos;
using CoachlyBackEnd.Models.DTOs.TrainerSpecialization;
using CoachlyBackEnd.Models.DTOs.UserDtos;
using CoachlyBackEnd.Models.DTOs.UserSubscibtion;

namespace CoachlyBackEnd.Models.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        #region User
        
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<User, UserCreateDto>().ReverseMap();
        CreateMap<User, UserUpdateDto>().ReverseMap();
        
        #endregion

        #region Trainer

        CreateMap<Trainer, TrainerRegisterDto>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location));
        
        CreateMap<TrainerRegisterDto, Trainer>()
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
            .ForMember(dest => dest.User, opt => opt.Ignore()); 
        
        CreateMap<Trainer, TrainerDto>().ReverseMap();
        CreateMap<Trainer, TrainerCreateDto>().ReverseMap();
        CreateMap<Trainer, TrainerUpdateDto>().ReverseMap();

        #endregion

        #region Payment

        CreateMap<Payment, PaymentCreateDto>().ReverseMap();
        CreateMap<Payment, PaymentDto>().ReverseMap();
        CreateMap<Payment, PaymentUpdateDto>().ReverseMap();

        #endregion
        
        #region Location
        
        CreateMap<Location, LocationDto>().ReverseMap();
        CreateMap<Location, LocationCreateDto>().ReverseMap();
        CreateMap<Location, LocationUpdateDto>().ReverseMap();
        
        #endregion

        #region Review

        CreateMap<Review, ReviewDto>().ReverseMap();
        CreateMap<Review, ReviewCreateDto>().ReverseMap();
        CreateMap<Review, ReviewUpdateDto>().ReverseMap();

        #endregion

        #region Session

        CreateMap<Session, SessionDto>().ReverseMap();
        CreateMap<Session, SessionCreateDto>().ReverseMap();
        CreateMap<Session, SessionUpdateDto>().ReverseMap();

        #endregion

        #region SessionParticipant

        CreateMap<SessionParticipant, SessionParticipantDto>().ReverseMap();
        CreateMap<SessionParticipant, SessionParticipantCreateDto>().ReverseMap();
        CreateMap<SessionParticipant, SessionParticipantUpdateDto>().ReverseMap();

        #endregion

        #region Specialization

        CreateMap<Specialization, SpecializationDto>().ReverseMap();
        CreateMap<Specialization, SpecializationCreateDto>().ReverseMap();
        CreateMap<Specialization, SpecializationUpdateDto>().ReverseMap();

        #endregion

        #region Subscription

        CreateMap<Subscribtion, SubscribtionDto>().ReverseMap();
        CreateMap<Subscribtion, SubscribtionCreateDto>().ReverseMap();
        CreateMap<Subscribtion, SubscribtionUpdateDto>().ReverseMap();

        #endregion

        #region TrainerAvailability

        CreateMap<TrainerAvailability, TrainerAvailabilityDto>().ReverseMap();
        CreateMap<TrainerAvailability, TrainerAvailabilityCreateDto>().ReverseMap();
        CreateMap<TrainerAvailability, TrainerAvailabilityUpdateDto>().ReverseMap();

        #endregion
        
        #region TrainerDocument 
        
        CreateMap<TrainerDocument, TrainerDocumentDto>().ReverseMap();
        CreateMap<TrainerDocument, TrainerDocumentCreateDto>().ReverseMap();
        CreateMap<TrainerDocument, TrainerDocumentUpdateDto>().ReverseMap();
        
        #endregion

        #region TrainerSpecialization

        CreateMap<TrainerSpecialization, TrainerSpecializationDto>().ReverseMap();  
        CreateMap<TrainerSpecialization, TrainerSpecializationCreateDto>().ReverseMap();  
        CreateMap<TrainerSpecialization, TrainerSpecializationUpdateDto>().ReverseMap();  

        #endregion

        #region UserSubscription

        CreateMap<UserSubscribtion, UserSubscribtionDto>().ReverseMap();
        CreateMap<UserSubscribtion, UserSubscribtionCreateDto>().ReverseMap();
        CreateMap<UserSubscribtion, UserSubscribtionUpdateDto>().ReverseMap();

        #endregion
    }
}