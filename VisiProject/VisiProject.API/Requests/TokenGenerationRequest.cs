﻿using System.ComponentModel.DataAnnotations;
using VisiProject.API.Utils;

namespace VisiProject.API.Requests;

public class TokenGenerationRequest
{
    [Required]
    [RegularExpression(RegexConstants.EmailRegex, ErrorMessage = ErrorMessageConstants.EmailIsInvalidMessage, MatchTimeoutInMilliseconds = RegexConstants.EmailRegexTimeout)]
    public string Email { get; set; }

    [Required]
    [StringLength(RegexConstants.PasswordMaxLength, ErrorMessage = ErrorMessageConstants.PasswordIncorrectLengthMessage, MinimumLength = RegexConstants.PasswordMinLength)]
    [RegularExpression(RegexConstants.PasswordRegex, ErrorMessage = ErrorMessageConstants.PasswordDoesNotContainRequiredCharactersMessage)]
    public string Password { get; set; }

    [Required]
    public string Scope { get; set; }
}