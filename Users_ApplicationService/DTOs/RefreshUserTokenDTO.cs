using Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users_ApplicationService.DTOs
{
    public class RefreshUserTokenDTO : BaseToken
    {
        public int UserId { get; set; }
    }
}
