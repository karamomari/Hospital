﻿

using Hospital.Models;

namespace Hospital.Repositories.Implementations
{
    public class PatientRecordRepository : IPatientRecordRepository
    {
        private readonly MvcHospitalcontext _context;

        public PatientRecordRepository(MvcHospitalcontext context)
        {
            _context = context;
        }

        public async Task<List<PatientRecord>> GetAllAsync()
        {
            return await _context.PatientRecords.Include(p=>p.Patient).ThenInclude(p=>p.User).ToListAsync();
        }

 

        public async Task<PatientRecord?> GetByIdAsync(string RecordId)
        {
            return await _context.PatientRecords.Include(p => p.Patient).Where(p => p.Id == RecordId).FirstOrDefaultAsync();
        }

        public async Task<PatientRecord?> GetByPatientIdAsync(string PatientId)
        {
            return await _context.PatientRecords.Include(p=>p.Patient).Where(p=>p.PatientId== PatientId).FirstOrDefaultAsync();
        }

        public async Task<List<PatientRecord>> GetListByPatientIdAsync(string patientId)
        {
            return await _context.PatientRecords
                .Include(p => p.Patient)
                .Where(p => p.PatientId == patientId)
                .ToListAsync();
        }

        public async Task AddAsync(PatientRecord record)
        {
            await _context.PatientRecords.AddAsync(record);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetMedicalRecordsCountAsync(string patientId)
        {
            return await _context.PatientRecords
                .CountAsync(r => r.PatientId == patientId);
        }

    }
}
