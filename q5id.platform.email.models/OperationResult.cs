using System;
namespace q5id.platform.email.models
{
	public class OperationResult
	{
		public OperationResult() { }

		public OperationResult(int code, string message)
		{
			Code = code;
			Message = message;
		}

		public int Code { get; set; }

		public string? Message { get; set; } = string.Empty;
	}
}

