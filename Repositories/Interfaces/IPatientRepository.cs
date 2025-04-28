namespace Hospital.Repositories.Interfaces
{
    public interface IPatientRepository
    {
        Patient? GetBySSN(string ssn);
        Task<Patient?> GetByUserIdAsync(string userId);

        Task<int> CountAsync();

    }
}
