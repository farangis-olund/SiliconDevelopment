﻿using Infrastructure.Models;

namespace Presentation.WebApp.ViewModels
{
	public class SignUpViewModel
	{
		public string Title { get; set; } = "Sign up";
		public SignUpModel Form { get; set; } = new SignUpModel();
	}
}
