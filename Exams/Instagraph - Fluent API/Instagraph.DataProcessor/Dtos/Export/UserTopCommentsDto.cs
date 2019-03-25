using System;
using System.Collections.Generic;
using System.Text;

namespace Instagraph.DataProcessor.Dtos.Export
{
    public class UserTopCommentsDto
    {
        public string Username { get; set; }
        public int MostComments { get; set; }
    }
}
