namespace Hospital.Service.Interfaces
{
    public interface IPatientService
    {
        Patient? GetBySSN(string ssn);
    }
}
