﻿using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.App_Start
{
    public class MyController : Controller
    {
        protected HMS_DBEntity db = new HMS_DBEntity();
    }
}