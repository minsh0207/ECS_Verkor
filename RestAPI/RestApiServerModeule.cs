using Nancy;
using Nancy.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestAPIServer
{
    public class RestApiServerModeule : NancyModule
    {

		private System.Diagnostics.Stopwatch procTime = new System.Diagnostics.Stopwatch();
		private JsonSerializerSettings _jsonSettings = new JsonSerializerSettings
		{
			NullValueHandling = NullValueHandling.Ignore
			//, MissingMemberHandling = MissingMemberHandling.Ignore
		};
		public RestApiServerModeule() 
		{

            Get("/ping", param => GET_ping(param));
			Post("/ecs/sendManualCommand", param => POST_Command(param));
		}

        private dynamic POST_Command(dynamic param)
        {
            try
            {
				procTime.Start();
				// Body에서 \r\n 제거
				string strBody = this.Context.Request.Body.AsString().Replace(Environment.NewLine, "");

				// Recv Body의 JSON string을 class 변수에 할당
				_CommandRequest recvBody = JsonConvert.DeserializeObject<_CommandRequest>(strBody, _jsonSettings);

				_baseResponse responseBody = SetCommand(recvBody);

				// Processing Result
				responseBody.ACTION_ID = recvBody.ACTION_ID;
				responseBody.RESPONSE_TIME = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}";
				responseBody.PROCESSING_TIME = $"{procTime.ElapsedMilliseconds} msec";

				// Response Body class를 JSON string으로 변환
				string strResponse = JsonConvert.SerializeObject(responseBody, Formatting.Indented);
				strResponse = strResponse.Replace(Environment.NewLine, "");

				// Response
				var response = (Response)strResponse;
				response.ContentType = "application/json";
				response.StatusCode = (HttpStatusCode)Int32.Parse(responseBody.RESPONSE_CODE);

				return response;
			}
			catch(Exception ex)
            {
				_LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
				Response response = "";
				response.ContentType = "application/json";
				response.StatusCode = HttpStatusCode.InternalServerError;

				return response;
			}
        }

        private _baseResponse SetCommand(_CommandRequest recvBody)
        {
			_baseResponse responseBody = new _baseResponse();

            try
            {
				// ==========================================================
				// Body Check
				// ----------------------------------------------------------
				if (recvBody.ACTION_ID != "SEND_MANUAL_COMMAND")
				{
					_LOG_($"ACTION_ID ({recvBody.ACTION_ID})", ECSLogger.LOG_LEVEL.ERROR);
					// Processing Result
					responseBody.RESPONSE_CODE = $"{(int)HttpStatusCode.BadRequest}";
					responseBody.RESPONSE_MESSAGE = HttpStatusCode.BadRequest.ToString();
					return responseBody;
				}

				//EQPClient 에서 Command를 써줌.
				if(DataClass.EQP_TYPE != recvBody.EQP_TYPE || DataClass.EQP_ID != recvBody.EQP_ID)
                {
					// 에러
					_LOG_($"[RestApiServer] request body error : {recvBody.ToString()}", ECSLogger.LOG_LEVEL.ERROR);
					// Processing Result
					responseBody.RESPONSE_CODE = $"{(int)HttpStatusCode.BadRequest}";
					responseBody.RESPONSE_MESSAGE = HttpStatusCode.BadRequest.ToString();
					return responseBody;
				}
				if(DataClass.UNIT_ID !=null && DataClass.UNIT_ID.Length > 1)
                {
					if(DataClass.UNIT_ID != recvBody.UNIT_ID)
                    {
						//에러
						_LOG_($"[RestApiServer] request body error : {recvBody.ToString()}", ECSLogger.LOG_LEVEL.ERROR);
						// Processing Result
						responseBody.RESPONSE_CODE = $"{(int)HttpStatusCode.BadRequest}";
						responseBody.RESPONSE_MESSAGE = HttpStatusCode.BadRequest.ToString();
						return responseBody;
					}
                }

				//UInt16 iCommand = GetCommandNumber(recvBody.COMMAND);
				if(recvBody.COMMAND == null || recvBody.COMMAND.Length<1)
                {
					_LOG_($"[RestApiServer] request command error : {recvBody.ToString()}", ECSLogger.LOG_LEVEL.ERROR);

					responseBody.RESPONSE_CODE = $"{(int)HttpStatusCode.BadRequest}";
					responseBody.RESPONSE_MESSAGE = HttpStatusCode.BadRequest.ToString();
					return responseBody;

				}
				UInt16 iCommand = UInt16.Parse(recvBody.COMMAND);
				if (iCommand < 1)
                {
					_LOG_($"[RestApiServer] request body error : {recvBody.ToString()}", ECSLogger.LOG_LEVEL.ERROR);
					// Processing Result
					responseBody.RESPONSE_CODE = $"{(int)HttpStatusCode.BadRequest}";
					responseBody.RESPONSE_MESSAGE = HttpStatusCode.BadRequest.ToString();
					return responseBody;
				}

				if(DataClass.EQPClient.WriteNodeByPath(DataClass.CommandPath+".Command", iCommand)==false)
                {
					//Write Error
					_LOG_($"[RestApiServer] Fail to write Command [{DataClass.CommandPath + ".Command"}:{recvBody.COMMAND}:{iCommand}]", ECSLogger.LOG_LEVEL.ERROR);
					// Processing Result
					responseBody.RESPONSE_CODE = $"{(int)HttpStatusCode.InternalServerError}";
					responseBody.RESPONSE_MESSAGE = HttpStatusCode.InternalServerError.ToString();
					return responseBody;
				}
				_LOG_($"[RestApiServer] Success to write Command [{DataClass.CommandPath + ".Command"}:{recvBody.COMMAND}:{iCommand}] by [{recvBody.ACTION_USER}]");

				// ==========================================================
				// Response
				// ----------------------------------------------------------
				responseBody.RESPONSE_CODE = $"{(int)HttpStatusCode.OK}";
				responseBody.RESPONSE_MESSAGE = HttpStatusCode.OK.ToString();
				return responseBody;

			}
			catch (Exception ex)
            {
				//Write Error
				_LOG_($"[RestApiServer:Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
				// Processing Result
				responseBody.RESPONSE_CODE = $"{(int)HttpStatusCode.InternalServerError}";
				responseBody.RESPONSE_MESSAGE = HttpStatusCode.InternalServerError.ToString();
				return responseBody;
			}

        }

        private UInt16 GetCommandNumber(string command)
        {
            switch(command.ToUpper())
            {
				case "STOP_PROCESS":
					return 1;
				case "RESTART_PROCESS":
					return 2;
				case "PAUSE_PROCESS":
					return 4;
				case "RESUME_PROCESS":
					return 8;
				case "FORCE_OUT_TRAY":
					return 16;
				default:
					return 0;

            }
        }

        private dynamic GET_ping(dynamic param)
		{
			try
			{
				procTime.Start();

				// Body에서 \r\n 제거
				string strBody = this.Context.Request.Body.AsString().Replace(Environment.NewLine, "");

				// Recv Body의 JSON string을 class 변수에 할당
				__baseRequest recvBody = JsonConvert.DeserializeObject<__baseRequest>(strBody, _jsonSettings);

				// Response Body
				procTime.Stop();

				_baseResponse responseBody = new _baseResponse
				{
					//ACTION_ID = recvBody.ACTION_ID;
					ACTION_ID = "PING",
					RESPONSE_TIME = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}",
					RESPONSE_CODE = $"{(int)HttpStatusCode.OK}",
					RESPONSE_MESSAGE = HttpStatusCode.OK.ToString(),
					PROCESSING_TIME = $"{procTime.ElapsedMilliseconds} msec"
				};

				// Response Body class를 JSON string으로 변환
				string responseStr = JsonConvert.SerializeObject(responseBody, Formatting.Indented);
				//_LOG.Info($"{strBody}, {responseStr}");

				// Response
				var response = (Response)responseStr;
				response.ContentType = "application/json";

				return response;
			}
			catch (Exception ex)
			{
				//Write Error
				_LOG_($"[RestApiServer:Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);

				//Console.WriteLine($"[MAIN] Application running error(Err) : {ex.Message}");
				//_LOG.Error($"Exception : {ex.Message}");
				//return Response.AsText($"GET_RecvData() : Exception : {ex.Message}");
				Response response = "";
				response.ContentType = "application/json";
				response.StatusCode = HttpStatusCode.InternalServerError;
				return response;

			}
		}

		#region LogForEQP
		public void _LOG_(string strDescription, ECSLogger.LOG_LEVEL level = ECSLogger.LOG_LEVEL.ALL)
		{
			string caller = (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name;
			//string log = $"{EQPID}, {UNITID}, {level.ToString()},  {caller}, {strDescription}";
			string log = $"{DataClass.EQP_ID}, {DataClass.UNIT_ID}, {caller}, {strDescription}";
			ECSLogger.Logger.WriteLog(log, level, DataClass.UNIT_ID);
		}
		#endregion
	}
}
