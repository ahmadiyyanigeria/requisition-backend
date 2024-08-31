using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class BankResponse
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public List<BankDTO> Data { get; set; }
    }
}
