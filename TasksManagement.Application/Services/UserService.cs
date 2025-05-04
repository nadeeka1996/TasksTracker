using Microsoft.Extensions.Caching.Memory;
using System.Security.Cryptography;
using System.Text;
using TasksManagement.Application.Interfaces.Services;
using TasksManagement.Application.Interfaces.UnitOfWorks;
using TasksManagement.Application.Models;
using TasksManagement.Application.Models.Responses;
using TasksManagement.Domain.Entities;

namespace TasksManagement.Application.Services;

internal sealed class UserService(
    IUnitOfWork unitOfWork,
    IMemoryCache memoryCache
) : IUserService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMemoryCache _memoryCache = memoryCache;
    private const int CacheDurationMinutes = 5;

    public async Task<Result<UserResponse>> AuthenticateAsync(string email, string password)
    {
        var repository = _unitOfWork.Repository<User>();
        var user = await repository.FirstOrDefaultAsync(
            filter: u => u.Email == email,
            disableTracking: true
        );

        if (user == null || !VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
            return Result<UserResponse>.Failure("Invalid credentials");

        return Result<UserResponse>.Success(new UserResponse(user.Id, user.Name, user.Email));
    }

    public async Task<Result<UserResponse>> RegisterAsync(string name, string email, string password)
    {
        var repository = _unitOfWork.Repository<User>();

        if (await repository.AnyAsync(filter: u => u.Email == email))
            return Result<UserResponse>.Failure("User already exists");

        var (hash, salt) = HashPassword(password);
        var user = User.Create(name, email, hash, salt);

        await repository.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        return Result<UserResponse>.Success(new UserResponse(user.Id, user.Name, user.Email));
    }

    public async Task<Result> IsValidIdAsync(Guid userId)
    {
        if (_memoryCache.TryGetValue(userId, out bool isValid))
            return isValid 
                ? Result.Success() 
                : Result.Failure("Invalid user ID");

        var repository = _unitOfWork.Repository<User>();
        isValid = await repository.AnyAsync(filter: u => u.Id == userId);

        if (isValid)
            _memoryCache.Set(
                userId, 
                isValid, 
                TimeSpan.FromMinutes(CacheDurationMinutes)
            );

        return isValid 
            ? Result.Success() 
            : Result.Failure("Invalid user ID");
    }

    private static (string Hash, string Salt) HashPassword(string password)
    {
        using var hmac = new HMACSHA256();
        var saltBytes = hmac.Key;
        var passwordBytes = Encoding.UTF8.GetBytes(password);

        var hashBytes = hmac.ComputeHash(passwordBytes);

        return (
            Convert.ToBase64String(hashBytes),
            Convert.ToBase64String(saltBytes)
        );
    }

    private static bool VerifyPassword(string password, string storedHash, string storedSalt)
    {
        var saltBytes = Convert.FromBase64String(storedSalt);
        var passwordBytes = Encoding.UTF8.GetBytes(password);

        using var hmac = new HMACSHA256(saltBytes);
        var computedHash = hmac.ComputeHash(passwordBytes);

        return CryptographicOperations.FixedTimeEquals(
            Convert.FromBase64String(storedHash), computedHash
        );
    }
}
