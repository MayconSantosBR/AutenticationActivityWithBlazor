using AuthorizationActivityServer.Services.Base;

namespace AuthorizationActivityServer.Services
{
	public class AuthenticationService : ServiceBase
	{
		public bool Authenticate(string username, string password)
		{
			try
			{
				return _accounts.Find(c => c.Username.Equals(username)).Password == password;
			}
			catch
			{
				return false;
			}
		}
	}
}
