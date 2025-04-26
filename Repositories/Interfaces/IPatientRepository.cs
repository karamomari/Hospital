namespace Hospital.Repositories.Interfaces
{
    public interface IPatientRepository
    {
        Patient? GetBySSN(string ssn);
        Task<int> CountAsync();

    }
}
