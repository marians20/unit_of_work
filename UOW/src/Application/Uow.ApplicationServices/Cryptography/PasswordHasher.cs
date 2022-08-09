// <copyright file="Repository.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Uow.ApplicationServices.Cryptography;
public static class PasswordHasher
{
    public static string GenerateSalt() => BCrypt.Net.BCrypt.GenerateSalt();

    public static string HashPassword(string password, string salt) => BCrypt.Net.BCrypt.HashPassword($"{salt}{password}");

    public static bool VerifyPassword(string password, string salt, string passwordHash) => BCrypt.Net.BCrypt.Verify($"{salt}{password}", passwordHash);
}