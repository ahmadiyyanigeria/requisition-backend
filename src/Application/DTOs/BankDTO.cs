using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class BankDTO
    {
        public string Name { get; set; } = default!;
        public string Slug { get; set; } = default!;
        public string Code { get; set; } = default!;
        public string Longcode { get; set; } = default!;
    }
}
