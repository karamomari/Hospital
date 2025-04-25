namespace Hospital.Repositories.Interfaces
{
    public interface IPatientRepository
    {
        Patient? GetBySSN(string ssn);
    }
}
