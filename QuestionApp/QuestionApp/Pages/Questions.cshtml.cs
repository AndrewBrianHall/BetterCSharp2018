using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuestionApp.Services;

namespace QuestionApp.Pages
{
    public class QuestionsModel : PageModel
    {
        public List<string> Questions { get; set; }

        private IStorageService _storageService;

        public QuestionsModel(IStorageService storageService)
        {
            _storageService = storageService;
        }

        public async Task OnGetAsync()
        {
            var questions = await _storageService.GetQuestionsAsync();
            this.Questions = new List<string>();
            foreach (var question in questions)
            {
                Questions.Add(question.Question);
            }
        }
    }
}
