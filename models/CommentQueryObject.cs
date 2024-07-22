using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.models
{
    public class CommentQueryObject
    {
        public string Symbol { get; set; } = string.Empty;
        public bool IsDescending { get; set; } = true;
    }
}