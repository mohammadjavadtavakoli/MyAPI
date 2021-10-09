using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Services
{
    public interface IJwtService
    {
         string Generate(User user);
    }
}
