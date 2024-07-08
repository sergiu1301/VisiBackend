﻿namespace VisiProject.Contracts.Services;

public interface IEncryptDecryptService
{
    string Encrypt(string plainText);
    string Decrypt(string encryptedText);
}