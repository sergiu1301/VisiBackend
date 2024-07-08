using System.ComponentModel.DataAnnotations;
using VisiProject.API.Utils;

namespace VisiProject.API.Requests;

public class EmailRequest
{
    [Required]
    [RegularExpression(RegexConstants.EmailRegex, ErrorMessage = ErrorMessageConstants.EmailIsInvalidMessage, MatchTimeoutInMilliseconds = RegexConstants.EmailRegexTimeout)]
    public string Email { get; set; }
}