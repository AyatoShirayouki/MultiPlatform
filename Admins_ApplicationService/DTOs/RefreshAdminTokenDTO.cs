using Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admins_ApplicationService.DTOs
{
    public class RefreshAdminTokenDTO : BaseToken
    {
        public int AdminId { get; set; }
    }
}
