using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace GetWatch.Services
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IJSRuntime _jsRuntime;

        public CustomAuthenticationStateProvider(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        private ClaimsPrincipal _anonymous => new ClaimsPrincipal(new ClaimsIdentity());

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var email = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authEmail");
            var userId = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authUserId");
            
            

            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(userId))
            {
                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Role, "User")
                }, "CustomAuth");

                var user = new ClaimsPrincipal(identity);
                return new AuthenticationState(user);
            }

            return new AuthenticationState(_anonymous);
        }

        public async Task NotifyUserAuthentication(string email, string userId)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "authEmail", email);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "authUserId", userId);

            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Role, "User")
            }, "CustomAuth");

            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public async Task NotifyUserLogout()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "authEmail");
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "authUserId");
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
        }
    }
}
