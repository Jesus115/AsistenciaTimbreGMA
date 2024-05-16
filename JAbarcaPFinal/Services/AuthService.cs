using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;

namespace JAbarcaPFinal.Services
{
	public class AuthService
	{
		private const string AuthStateKey = "AuthState";
		public AuthService()
		{
		}

		public async Task<bool> IsAuthenticateAsync() {
			await Task.Delay(2000);
			var authState = Preferences.Default.Get<bool>(AuthStateKey, false);
			return authState;
		}
		public async void Login(string token) {
            await SecureStorage.SetAsync("AuthToken", token);
            Preferences.Set("AuthToken", token);
            Preferences.Default.Set<bool>(AuthStateKey, true);
		}
        public void Logout()
        {
            Preferences.Default.Remove(AuthStateKey);
        }
    }
}

