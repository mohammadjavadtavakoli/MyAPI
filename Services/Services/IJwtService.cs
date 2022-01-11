using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public interface IJwtService
    {
        Task<AccessToken> Generate(User user);
    }
}
