using BackendPTDetecta.Domain.Entities;

namespace BackendPTDetecta.Domain.Interfaces
{
    public interface IPacienteRepository
    {
        Task<IEnumerable<Paciente>> GetAllAsync();
        Task<Paciente?> GetByIdAsync(int id);
        Task<Paciente?> GetByDniAsync(string dni);
        Task<int> AddAsync(Paciente paciente);
        Task UpdateAsync(Paciente paciente);
    }
}