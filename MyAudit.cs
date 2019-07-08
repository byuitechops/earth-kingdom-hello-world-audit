using System;
using System.Collections.Generic;
using System.Text;
using Byui.CourseAudits.Business.Model.AuditBaseClass;
using Byui.CourseAudits.Business.Model.AuditReporters;

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
                // our code goes here
            }
        }
    }
}
