﻿using System.Web.Mvc;

namespace TicTacToeWeb.Web.Controllers
{
    public class HomeController : TicTacToeWebControllerBase
    {
        public ActionResult Index()
        { 
            return View("~/App/Main/views/layout/layout.cshtml"); //Layout of the angular application.
        }
	}
}