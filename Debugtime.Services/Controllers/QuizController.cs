using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Debugtime.Common.Configurations;
using Debugtime.Common.Dtos;
using Debugtime.Common.Persistence;
using DebugTime.Domain.Model;

namespace Debugtime.Services.Controllers
{
    [RoutePrefix("api/quiz")]
    public class QuizController : ApiController
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;

        public IMapper Mapper => _mapper ?? (_mapper = new AutoMapperProfileConfiguration().EntityMapper);

        [HttpPost]
        [Route("user/answer")]
        public IHttpActionResult SubmitAnswer(UserQuizAnswerDto answerDto)
        {
            _context = new ApplicationDbContext();
            try
            {
                answerDto.IsCorrect = _context.QuizQuestions.Any(
                    q => q.QuizId == answerDto.QuizId && q.Key == answerDto.QuestionKey && q.Answer == answerDto.Answer);

                var userAnswer = Mapper.Map<UserQuizAnswerDto, UserQuizAnswer>(answerDto);
                _context.UserQuizAnswers.Add(userAnswer);
                if (_context.SaveChanges() <= 0)
                    return BadRequest();
                return Ok<object>("Success");
            }
            catch (DbUpdateException)
            {
                return BadRequest();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
