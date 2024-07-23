using BackEndDevChallenge.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndDevChallenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {
        private readonly ILogger<CalculatorController> _logger;
        private readonly CalculatorContext _context;

        public CalculatorController(ILogger<CalculatorController> logger, CalculatorContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("Add")]
        public async Task<ActionResult<MathResponse>> Add(string? userName, int input1, int input2)
        {
            var response = new MathResponse
            {
                Input1 = input1,
                Input2 = input2
            };

            try
            {
                if(input1 < 0 || input2 < 0)
                {
                    throw new ArgumentException("Input values must be positive");                    
                }

                response.Result = input1 + input2;
                await SaveMathProblem(GetUserName(userName),input1, input2, (int)response.Result, MathOperationType.Addition, ErrorType.None, null);
            }
            catch (ArgumentException ex)
            {
                response.ErrorMessage = ex.Message;
                response.ErrorType = ErrorType.InvalidInput.ToString();
                await SaveMathProblem(GetUserName(userName), input1, input2, null, MathOperationType.Addition, ErrorType.InvalidInput, ex.Message);
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.ErrorType = ErrorType.SystemError.ToString();
                await SaveMathProblem(GetUserName(userName), input1, input2, null, MathOperationType.Addition, ErrorType.SystemError, ex.Message);
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("Subtract")]
        public async Task<ActionResult<MathResponse>> Subtract(string? userName, int input1, int input2)
        {
            var response = new MathResponse
            {
                Input1 = input1,
                Input2 = input2
            };

            try
            {
                if (input1 < 0 || input2 < 0)
                {
                    throw new ArgumentException("Input values must be positive");
                    
                }

                response.Result = input1 - input2;
                await SaveMathProblem(GetUserName(userName), input1, input2, response.Result, MathOperationType.Subtraction, ErrorType.None, null);
            }
            catch (ArgumentException ex)
            {
                response.ErrorMessage = ex.Message;
                response.ErrorType = ErrorType.InvalidInput.ToString();
                await SaveMathProblem(GetUserName(userName), input1, input2, null, MathOperationType.Subtraction, ErrorType.InvalidInput, ex.Message);
                return BadRequest(response);
            }
            
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.ErrorType = ErrorType.SystemError.ToString();
                await SaveMathProblem(GetUserName(userName), input1, input2, null, MathOperationType.Subtraction, ErrorType.SystemError, ex.Message);
                return BadRequest(response);
            }


            return Ok(response);
        }

        [HttpGet("Divide")]
        public async Task<ActionResult<MathResponse>> Divide(string? userName, int input1, int input2)
        {
            var response = new MathResponse
            {
                Input1 = input1,
                Input2 = input2
            };

            try
            {
                if (input2 == 0)
                {
                    throw new ArgumentException("Division by zero is not valid");                    
                }

                response.Result = (double)input1 / input2;
                await SaveMathProblem(GetUserName(userName), input1, input2, response.Result, MathOperationType.Division, ErrorType.None, null);
            }
            catch(ArgumentException ex) {                 
                response.ErrorMessage = ex.Message;
                response.ErrorType = ErrorType.DivisionByZero.ToString();
                await SaveMathProblem(GetUserName(userName), input1, input2, null, MathOperationType.Division, ErrorType.DivisionByZero, ex.Message);
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.ErrorType = ErrorType.SystemError.ToString();
                await SaveMathProblem(GetUserName(userName), input1, input2, null, MathOperationType.Division, ErrorType.SystemError, ex.Message);
                return BadRequest(response);
            }
            


            return Ok(response);
        }

        [HttpGet("Multiply")]
        public async Task<ActionResult<MathResponse>> Multiply(string? userName, int input1, int input2)
        {
            var response = new MathResponse
            {
                Input1 = input1,
                Input2 = input2
            };

            try
            {
                if (input1 == 0 || input2 == 0)
                {
                    throw new ArgumentException("Input values cannot be zero. Supply a number greater or less than zero.");                    
                }

                response.Result = input1 * input2;
                await SaveMathProblem(GetUserName(userName), input1, input2, response.Result, MathOperationType.Multiplication, ErrorType.None, null);
            }
            catch (ArgumentException ex)
            {
                response.ErrorMessage = ex.Message;
                response.ErrorType = ErrorType.InvalidInput.ToString();
                await SaveMathProblem(GetUserName(userName), input1, input2, null, MathOperationType.Multiplication, ErrorType.InvalidInput, ex.Message);
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.ErrorType = ErrorType.SystemError.ToString();
                await SaveMathProblem(GetUserName(userName), input1, input2, null, MathOperationType.Multiplication, ErrorType.SystemError, ex.Message);
                return BadRequest(response);
            }


            return Ok(response);
        }

        [HttpGet("UserDataReport")]
        public async Task<ActionResult<IEnumerable<UserData>>> UserDataReport()
        {
            try
            {
                var userReport = await _context.MathProblems
                .GroupBy(m => m.UserName)
                .Select(u => new UserData
                {
                    UserName = u.Key,
                    TotalProblems = u.Count(),
                    ErrorCount = u.Count(m => m.ErrorMessage != null)
                }).ToListAsync();

                return Ok(userReport);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet("ErrorTypes")]
        public async Task<ActionResult<IEnumerable<ErrorTypeData>>> ErrorTypes()
        {
            try
            {
                var errorTypeReport = await _context.MathProblems
                .GroupBy(m => m.ErrorType)
                .Select(e => new ErrorTypeData
                {
                    ErrorType = e.Key.ToString(),
                    ErrorTypeCount = e.Count()
                }).ToListAsync();

                return Ok(errorTypeReport);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("CommonAnswer")]
        public async Task<ActionResult<CommonAnswer>> CommonAnswer()
        {
            try
            {
                var commonAnswer = await _context.MathProblems
                .GroupBy(m => m.Result)
                .OrderByDescending(o => o.Count(e => e.ErrorMessage == null))
                .Select(c => new CommonAnswer{
                    Result = c.Key,
                    Occurrences = c.Count()
                }).FirstOrDefaultAsync();
                

                return Ok(commonAnswer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }




        private async Task SaveMathProblem(string? userName,int input1, int input2, double? result, MathOperationType operationType,ErrorType errorType,string? errorMessage)
        {
            var mathProblem = new MathProblem
            {
                UserName = userName,
                Input1 = input1,
                Input2 = input2,
                Result = result,
                OperationType = operationType,
                ErrorType = errorType,
                ErrorMessage = errorMessage,
                Timestamp = DateTime.Now
            };

            _context.MathProblems.Add(mathProblem);
            await _context.SaveChangesAsync();
        }

        private string? GetUserName(string? userName)
        {
            if (!String.IsNullOrWhiteSpace(userName)){
                return userName;
            }
            else
            {
                return "Legacy";
            }
        }
    }
}
