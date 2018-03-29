using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuestionApp.Models;
using QuestionApp.Services;

namespace QuestionApp.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public UserQuestion Question { get; set; }

        private IStorageService _storageService;

        public IndexModel(IStorageService storageService)
        {
            _storageService = storageService;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _storageService.AddQuestionAsync(this.Question);
            return RedirectToPage("/Success");
        }
    }
}
