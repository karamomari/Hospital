namespace Hospital.Repositories.Interfaces
{
    public interface IDoctorRepository
    {
        Task<List<Doctor>> GetAllAsync();
        Task<Doctor?> GetByIdAsync(string id);
        Task<Doctor?> GetByUserIdAsync(string id);
        Task<Doctor?> GetByUserIdWithDetalisAsync(string id);
        Task SaveChangesAsync();
    }
}
