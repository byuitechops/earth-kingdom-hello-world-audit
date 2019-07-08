using System;
using System.Collections.Generic;
using System.Text;
using Byui.CourseAudits.Business.Model.AuditBaseClass;
using Byui.CourseAudits.Business.Model.AuditReporters;
using Newtonsoft.JSON;
using Newtonsoft.JSON.Linq;

namespace Byui.CourseAudits.Business.Model.Audits
{
    public class HideFinalGradesAudit : Audit
    {
        public string Name;
        public string Code;
        public string Description;
        private readonly IAuditReporter _auditReporter;
        public HideFinalGradesAudit(IAuditReporter auditReporter) : base(auditReporter)
        {
            Name = "Hide Final Grades Audit";
            Code = "Hide-final-grades-audit";
            Description = "This is a test audit";
        }
        public void ReportAuditResults(List<AuditResult> auditResults);
        public List<AuditResult> RunAudits(List<string> courseCodes);
        public static async Task<string> MakeHTTPRequest(string endPoint, string api_token)
        {
            using (client)
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", api_token);
                // Call asynchronous network methods in a try/catch block to handle exceptions
                try
                {
                    if (endPoint[0] != '/')
                    {
                        endPoint = "/" + endPoint;
                    }
                    string url = "https://byui.instructure.com" + endPoint;
                    string responseBody = await client.GetStringAsync(url);

                    return responseBody;
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("There was an error: " + e.Message);
                    throw;
                }
            }
        }
        internal override List<AuditResult> ExecuteAudits(List<string> courseCodes)
        {
            var auditResults = new List<AuditResult>();
            foreach (var courseCode in courseCodes)
            {
                var start = DateTime.Now;
                // make api call here (/api/v1/courses/:course_id/settings)
                string api_token = System.Environment.GetEnvironmentVariable("CANVAS_API_TOKEN");
                string endPoint = args.Length != 0 ? args[0] : "/api/v1/courses/" + courseCode + "/settings";

                private static readonly HttpClient client = new HttpClient();
                string callData = await HTTPHelper.MakeHTTPRequest(endPoint, api_token);
                
                var result = true; // = api_call.GetValue("hide_final_grades");
                result = callData.GetValue("hide_final_grades");
                var currentAudit = new AuditResult
                {
                    AuditCode = Code,
                    courseCode = courseCode,
                    StartTime = start,
                    EndTime = DateTime.Now,
                    AuditStatus = result
                };
                currentAudit.Messages.Add(new AuditMessage
                {
                    AuditCode = Code,
                    MessageType = "example",
                    Message = $"Made an api call to course {courseCode} settings for \"Is hide_final_grades true?\""
                });
                auditResults.Add(currentAudit);
            }
            return auditResults;
        }
    }
}
