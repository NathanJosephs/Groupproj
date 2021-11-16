using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Groupproj.Controllers
{
    public class TimetablesController : Controller
    {
        public IActionResult ClassTimetable()
        {
            return View();
        }


        public IActionResult ExamTimetable()
        {
            return View();
        }

        public IActionResult EditExamTimetable()
        {
            return View();
        }
        public IActionResult EditClassTimetable()
        {
            return View();
        }
    }
}
