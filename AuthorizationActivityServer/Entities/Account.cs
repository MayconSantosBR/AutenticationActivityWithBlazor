using Newtonsoft.Json;

namespace AuthorizationActivityServer.Entities
{
	public class Account
	{
		[JsonProperty("username")]
		public string Username { get; set; }

		[JsonProperty("password")]
		public string Password { get; set; }

		[JsonProperty("role")]
		public string Role { get; set; }
	}
}
