
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class SubscriptionModel
{
    [Display(Name = "Email address", Prompt = "Enter your email address")]
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Email address is required")]
    public string Email { get; set; } = null!;
    public Dictionary<string, bool> CheckboxValues { get; set; } = [];
}
