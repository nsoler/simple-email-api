using System;
using System.Reflection;
using q5id.platform.email.api.Interfaces;

namespace q5id.platform.email.api.Services
{
	public class AppVersionService : IAppVersionService
	{
		public string Version =>
			Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;

	}
}

