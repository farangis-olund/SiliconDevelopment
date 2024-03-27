﻿using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Services;

public static class PasswordHasherService
{
	public static string GenerateSecurePassword (string password)
	{
		try
		{
			byte[] salt = new byte[16];
			using (var rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes (salt);
			}

			using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256);
			byte[] hash = pbkdf2.GetBytes(20);
			byte[] hashBytes = new byte[36];
			Array.Copy(salt, 0, hashBytes, 0, 16);
			Array.Copy(hash, 0, hashBytes, 16, 20);

			return Convert.ToBase64String (hashBytes);
		}
		catch  { }
		return null!;
	}

	public static bool ValidateSecurePassword(string password, string storedPassword)
	{
		try
		{

			byte[] hashByte = Convert.FromBase64String(storedPassword);
			byte[] salt = new byte[16];
			Array.Copy(hashByte, 0, salt, 0, 16);

			using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256);
			byte[] hash = pbkdf2.GetBytes(20);
			for(int i=0; i < 20; i++)
			{
				if (hashByte[i + 16] != hash[i])
					return false;
			}
			return true;
		}
		catch { }
		return false;
	}
}
