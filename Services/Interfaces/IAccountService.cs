using FinanceDashboard.DTOs;
using FinanceDashboard.DTOs.Account;

public interface IAccountService
{
    Task<IEnumerable<AccountResponseDto>> GetAllAsync();
    Task<AccountResponseDto> GetByIdAsync(int id);
    Task<AccountResponseDto> CreateAsync(CreateAccountDto dto);
    Task<AccountResponseDto> UpdateAsync(int id, UpdateAccountDto dto);
    Task DeleteAsync(int id);
}
