using AuthorizationActivityServer.Entities;
using Newtonsoft.Json;

namespace AuthorizationActivityServer.Services.Base
{
	public class ServiceBase
	{
#if DEBUG
		protected readonly List<Account> _accounts = JsonConvert.DeserializeObject<List<Account>>(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "bin", "Debug", "net6.0", "Entities", "accounts.json")));
#else
		protected readonly List<Account> _accounts = JsonConvert.DeserializeObject<List<Account>>(File.ReadAllText(Path.Combine(Environment.ProcessPath, "Release", "Entities", "accounts.json")));
#endif
	}
}
