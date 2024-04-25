using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMe.Shared
{
    public class HumanDetectionResponse
    {
        public bool IsSuccess { get; set; }
        public byte[]? Data { get; set; }
        public string? Message { get; set; }
    }
}
