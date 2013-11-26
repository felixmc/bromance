using Bros.DataModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bros.Controllers
{
    public class MessagingController : Controller
    {
        //
        // GET: /Messaging/

        public ActionResult MessageHome()
        {
            Bros.DataModel.User thisUser = (Bros.DataModel.User)Session["User"];

            List<Message> allMessages = thisUser.MessagesSent.ToList();
            allMessages.AddRange(thisUser.MessagesReceived.ToList());

            allMessages = allMessages.OrderBy(x => x.DateSent).ToList();
            List<User> uniqueUsers = allMessages.Select(m => m.Receiver).OrderBy(m => m.Id).Distinct().ToList();
            ViewBag.messages = allMessages;
            ViewBag.UserConversations = uniqueUsers;
            return View();
        }

    }
}
