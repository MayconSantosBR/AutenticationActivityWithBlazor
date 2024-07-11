using AuthorizationActivityServer.Entities;
using AuthorizationActivityServer.Services.Base;

namespace AuthorizationActivityServer.Services
{
	public class AccountService : ServiceBase
	{
		public Account GetAccount(string username)
		{
			return _accounts.FirstOrDefault(c => c.Username.Equals(username));
		}
	}
}
