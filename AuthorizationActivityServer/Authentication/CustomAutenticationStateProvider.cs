﻿using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;

namespace AuthorizationActivityServer.Authentication
{
	public class CustomAutenticationStateProvider : AuthenticationStateProvider
	{
		private readonly ProtectedSessionStorage _sessionStorage;
		private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

		public CustomAutenticationStateProvider(ProtectedSessionStorage sessionStorage)
		{
			_sessionStorage = sessionStorage;
		}

		public override async Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			try
			{
				var userSessionStorage = await _sessionStorage.GetAsync<UserSession>("UserSession");
				var userSession = userSessionStorage.Success ? userSessionStorage.Value : null;

				if (userSession == null)
					return await Task.FromResult(new AuthenticationState(_anonymous));

				var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new[]
				{
					new Claim(ClaimTypes.Name, userSession.Username),
					new Claim(ClaimTypes.Role, userSession.Role)
				}, "CustomAuth"));

				return await Task.FromResult(new AuthenticationState(claimsPrincipal));
			}
			catch
			{
				return await Task.FromResult(new AuthenticationState(_anonymous));
			}
		}

		public async Task UpdateAuthenticationState(UserSession userSession)
		{
			ClaimsPrincipal claimsPrincipal;

			if (userSession == null)
			{
				await _sessionStorage.DeleteAsync("UserSession");
				claimsPrincipal = _anonymous;
			}
			else
			{
				await _sessionStorage.SetAsync("UserSession", userSession);

				claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new[]
				{
					new Claim(ClaimTypes.Name, userSession.Username),
					new Claim(ClaimTypes.Role, userSession.Role)
				}, "CustomAuth"));
			}

			NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
		}
	}
}
