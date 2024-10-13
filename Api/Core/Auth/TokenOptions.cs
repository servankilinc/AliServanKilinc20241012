﻿namespace Core.Auth;

public class TokenOptions
{
    public string Audience { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string SecurityKey { get; set; } = null!;
    public int AccessTokenExpiration { get; set; }
}
