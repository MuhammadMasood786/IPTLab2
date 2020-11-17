using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Xml.XPath;

namespace MarkSheet
{
    /// <summary>
    /// Summary description for markSheet
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class markSheet : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public string Calculate()
        {
            string data = HttpContext.Current.Request["SubjectMarks"];

            SubjectInfo[] subjectMarks = JsonConvert.DeserializeObject<SubjectInfo[]>(data);

            SubjectInfo MinSubjectMarks = subjectMarks.First(x => x.ObtainMarks == subjectMarks.Min(y => y.ObtainMarks));
            SubjectInfo MaxSubjectMarks = subjectMarks.First(x => x.ObtainMarks == subjectMarks.Max(y => y.ObtainMarks));

            decimal noOfSubject = subjectMarks.Count();
            decimal totalMarks = 100 * noOfSubject;
            decimal totalObtainMarks = subjectMarks.Sum(x => x.ObtainMarks);
            decimal percent = (totalObtainMarks / totalMarks) * 100;

            Result result = new Result()
            {
                MinMarks = MinSubjectMarks,
                MaxMarks = MaxSubjectMarks,
                Percantage = percent
            };

            return JsonConvert.SerializeObject(result);
        }


        public class SubjectInfo
        {
            public string subject { get; set; }
            public int ObtainMarks { get; set; }

        }

        public class Result
        {
            public SubjectInfo MinMarks { get; set; }
            public SubjectInfo MaxMarks { get; set; }
            public decimal Percantage { get; set; }

        }
    }
 }
