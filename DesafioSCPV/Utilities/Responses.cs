using ViewModels.Response;

namespace Presentation.Utilities
{
    public static class Responses
    {
        public static ResultViewModel ApplicationErrorMessage()
        {
            return new ResultViewModel
            {
                Message = "Ocorreu algum erro interno na aplicação, por favor tente novamente.",
                Success = true,
                Data = null
            };
        }
        
        public static ResultViewModel DomainErrorMessage(string message) {

            return new ResultViewModel
            {
                Message = message,
                Success = true,
                Data = null
            };
        }

        public static ResultViewModel DomainErrorMessage(string message, IReadOnlyCollection<string> errors)
        {
            return new ResultViewModel
            {
                Message = message,
                Success = true,
                Data = errors
            };
        }

        public static ResultViewModel UnauthorizedErrorMessage() {

            return new ResultViewModel
            {
                Message = "A combinação de login e senha está incorreta!",
                Success = true,
                Data = null
            };
        }
    }
}
