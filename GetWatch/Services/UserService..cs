using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using GetWatch.Interfaces.Db;
using GetWatch.Services.Db;


namespace GetWatch.Services
{
    public class UserService
{
    private readonly CustomAuthenticationStateProvider _authProvider;
    private readonly IUnitOfWork _unitOfWork;

    public UserService(CustomAuthenticationStateProvider authProvider, IUnitOfWork unitOfWork)
    {
        _authProvider = authProvider;
        _unitOfWork = unitOfWork;
    }

    public async Task<DbUser?> GetAuthenticatedUserAsync()
    {
        var authState = await _authProvider.GetAuthenticationStateAsync();
        var userPrincipal = authState.User;

        if (userPrincipal?.Identity == null || !userPrincipal.Identity.IsAuthenticated)
            return null;

        var userEmail = userPrincipal.FindFirst(ClaimTypes.Email)?.Value;
        if (string.IsNullOrEmpty(userEmail))
            return null;

        var userRepository = _unitOfWork.GetRepository<DbUser>();
        if (userRepository == null)
            return null;
        return userRepository.GetAll().FirstOrDefault(x => x.Email == userEmail);
    }
}
}