using AutoMapper;
using Hospital.ViewModel.Appointment;
using Hospital.ViewModel.Patient;
using Hospital.ViewModel.PatientRecord;

namespace Hospital.Service.AutoMappe
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<PatientRecordToupdateDTO, PatientRecord>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) 
             .ForMember(dest => dest.RecordDate, opt => opt.Ignore()) 
            .ForMember(dest => dest.DoctorId, opt => opt.Ignore())   
             .ForMember(dest => dest.Doctor, opt => opt.Ignore())     
             .ForMember(dest => dest.Patient, opt => opt.Ignore())
             .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Appointment, AppointmentToReturnDTO>()
                .ForMember(dest => dest.PatientFullName, opt => opt.MapFrom(src => src.Patient.User.FirstName + " " + src.Patient.User.LastName))
                .ForMember(dest => dest.DoctorFullName, opt => opt.MapFrom(src => src.Doctor.User.FirstName + " " + src.Doctor.User.LastName));


            CreateMap<Appointment, AppointmentToViewDTO>()
             .ForMember(dest => dest.PatientFullName,
               opt => opt.MapFrom(src => $"{src.Patient.User.FirstName} {src.Patient.User.LastName}"));

            CreateMap<AppointmentToAddDTO, Appointment>();
            CreateMap<PatientRecord, PatientRecordToupdateDTO>();
            CreateMap<PatientRecordToAddDTO, PatientRecord>();


            CreateMap<Appointment, AppointmentToViewDTO>()
    .ForMember(dest => dest.PatientFullName, opt => opt.MapFrom(src => src.Patient.User.FirstName + " " + src.Patient.User.LastName))
    .ForMember(dest => dest.DoctorFullName, opt => opt.MapFrom(src => src.Doctor.User.FirstName + " " + src.Doctor.User.LastName));

            CreateMap<Appointment, Appointment>();
            CreateMap<Patient, PatientToViewDTO>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(p => p.User.FirstName + " " + p.User.LastName));


        }
    }
}
