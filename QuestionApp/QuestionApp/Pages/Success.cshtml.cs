using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace QuestionApp.Pages
{
    public class SuccessModel : PageModel
    {
        public string Message { get; set; }

        public void OnGet()
        {
            Message = "Your question was recieved";
        }
    }
}
