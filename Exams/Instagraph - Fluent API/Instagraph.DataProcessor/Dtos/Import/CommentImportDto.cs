using System;
using System.Collections.Generic;
using System.Text;

namespace Instagraph.DataProcessor.Dtos.Import
{
    public class CommentImportDto
    {
        public string Content { get; set; }
        public string User { get; set; }
        public string PostId { get; set; }
    }
}
