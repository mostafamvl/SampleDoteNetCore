using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskService
{
    public class Enums
    {
        public enum StatusResponse
        {
            Success = 'S',
            Duplicated = 'D',
            Failed = 'F',
        }
    }
}
