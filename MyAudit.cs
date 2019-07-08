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
        internal override List<AuditResult> ExecuteAudits(List<string> courseCodes)
        {
            var auditResults = new List<AuditResult>();
            foreach (var courseCode in courseCodes)
            {
                var start = DateTime.Now;
                // make api call here (/api/v1/courses/:course_id/settings)
                var result = true; // = api_call.GetValue("hide_final_grades");
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
