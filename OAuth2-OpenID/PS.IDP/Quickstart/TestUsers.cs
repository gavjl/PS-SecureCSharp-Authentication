// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;
using IdentityServer4;

namespace IdentityServerHost.Quickstart.UI
{
    public class TestUsers
    {
        public static List<TestUser> Users
        {
            get
            {
                return new List<TestUser>
                {
                    new TestUser
                    {
                        SubjectId = "1234",
                        Username = "sam",
                        Password = "password",
                        Claims =
                        {
                            new Claim(JwtClaimTypes.Name, "Sam Smith"),
                            new Claim(JwtClaimTypes.GivenName, "Sam"),
                            new Claim(JwtClaimTypes.FamilyName, "Smith"),
                            new Claim(JwtClaimTypes.Email, "SamSmith@email.com"),
                            new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        }
                    },
                    new TestUser
                    {
                        SubjectId = "2345",
                        Username = "ann",
                        Password = "password",
                        Claims =
                        {
                            new Claim(JwtClaimTypes.Name, "Ann Tilbury"),
                            new Claim(JwtClaimTypes.GivenName, "Ann"),
                            new Claim(JwtClaimTypes.FamilyName, "Tilbury"),
                            new Claim(JwtClaimTypes.Email, "AnnTilbury@email.com"),
                            new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        }
                    }
                };
            }
        }
    }
}