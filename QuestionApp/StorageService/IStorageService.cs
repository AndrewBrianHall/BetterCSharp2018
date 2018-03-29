using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using QuestionApp.Models;

namespace QuestionApp.Services
{
    public interface IStorageService
    {
        Task<IEnumerable<UserQuestion>> GetQuestionsAsync();
        Task AddQuestionAsync(UserQuestion question);
    }
}