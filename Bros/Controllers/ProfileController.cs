using Bros.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bros.Models;
using WebMatrix.WebData;

namespace Bros.Controllers
{
	[Authorize(Roles="User")]
	public class ProfileController : BroController
	{

		public ActionResult Index()
		{
			return View(WebSecurity.CurrentUserId);
		}

		public ActionResult View(int id)
		{
			ViewBag.IsOwner = id == WebSecurity.CurrentUserId;
			User user = new User();
			using (var context = new ModelFirstContainer())
			{
				user = context.Users.Include("Profile.ProfilePhoto").FirstOrDefault(u => u.Id == id);
			}
			return View("Index", user);
		}

		//public ActionResult Circles()
		//{
		//	ActionResult result;

		//	IEnumerable<Circle> circles;
		//	using (ModelFirstContainer context = new ModelFirstContainer())
		//	{
		//		int id = WebSecurity.CurrentUserId;
		//		User user = context.Users.Include("Circles.Members.Profile").SingleOrDefault(u => u.Id == id);

		//		circles = user.Circles.ToList();

		//		result = View(circles);
		//	}

		//	return result;
		//}

		//public ActionResult CreateCircle(string CircleName)
		//{
		//	using (ModelFirstContainer context = new ModelFirstContainer())
		//	{
		//		int id = (int)Session["UserID"];
		//		User user = context.Users.FirstOrDefault(u => u.Id == id);
		//		Circle targetCircle = new Circle();
		//		targetCircle.Name = CircleName;
		//		targetCircle.Owner = user;
		//		user.Circles.Add(targetCircle);

		//		context.SaveChanges();
		//	}

		//	return RedirectToAction("Circles");
		//}

		//public void RenameCircle(int targetCircleId, string newCircleName)
		//{
		//	using (ModelFirstContainer context = new ModelFirstContainer())
		//	{
		//		Circle targetCircle = context.Circles.SingleOrDefault(x => x.Id == targetCircleId);
		//		targetCircle.Name = newCircleName;

		//		context.SaveChanges();
		//	}
		//}

		//public void MoveBroBetweenCircles(int donorCircleId, int recieverCircleId, int targetBroId)
		//{
		//	using (ModelFirstContainer context = new ModelFirstContainer())
		//	{
		//		Circle donorCircle = context.Circles.SingleOrDefault(x => x.Id == donorCircleId);
		//		Circle recieverCircle = context.Circles.SingleOrDefault(x => x.Id == recieverCircleId);
		//		User targetBro = context.Users.SingleOrDefault(x => x.Id == targetBroId);

		//		donorCircle.Members.Remove(targetBro);
		//		recieverCircle.Members.Add(targetBro);

		//		context.SaveChanges();
		//	}
		//}

		//public ActionResult FromEditCircleMoveBroToCircle(int donorCircleId, int recieverCircleId, int targetBroId)
		//{
		//	using (ModelFirstContainer context = new ModelFirstContainer())
		//	{
		//		User user = context.Users.SingleOrDefault(x => x.Id == WebSecurity.CurrentUserId);
		//		Circle donorCircle = context.Circles.SingleOrDefault(x => x.Id == donorCircleId);
		//		Circle recieverCircle = context.Circles.SingleOrDefault(x => x.Id == recieverCircleId);
		//		User targetBro = context.Users.SingleOrDefault(x => x.Id == targetBroId);

		//		ContextlessMoveBroBetweenCircles(donorCircle, recieverCircle, targetBro);

		//		context.SaveChanges();
		//	}

		//	return RedirectToAction("EditCircle", new { circleId = donorCircleId });
		//}

		//public ActionResult LinkRenameCircle(int circleId, string circleName)
		//{
		//	using (ModelFirstContainer context = new ModelFirstContainer())
		//	{
		//		Circle targetCircle = context.Circles.SingleOrDefault(x => x.Id == circleId);
		//		targetCircle.Name = circleName;

		//		context.SaveChanges();
		//	}

		//	return RedirectToAction("EditCircle", new { circleId = circleId });
		//}

		//public ActionResult EditCircle(int circleId)
		//{
		//	ActionResult result;

		//	using (ModelFirstContainer context = new ModelFirstContainer())
		//	{
		//		Circle targetCircle = context.Circles.SingleOrDefault(x => x.Id == circleId);
		//		User user = context.Users.Include("Circles.Members.Profile").SingleOrDefault(x => x.Id == WebSecurity.CurrentUserId);

		//		ViewBag.TargetCircle = targetCircle;
		//		IEnumerable<Circle> circles = user.Circles.Where(x => x.Id != circleId).ToList();
		//		ViewBag.recieverCircleId = new SelectList(circles, "Id", "Name");

		//		result = View(targetCircle);
		//	}

		//	return result;
		//}

		//public ActionResult DeleteCircle(int circleId)
		//{

		//	using (ModelFirstContainer context = new ModelFirstContainer())
		//	{
		//		Circle targetCircle = context.Circles.SingleOrDefault(x => x.Id == circleId);

		//		if (targetCircle.Name != "MyBros")
		//		{
		//			int userId = WebSecurity.CurrentUserId;
		//			User user = context.Users.SingleOrDefault(x => x.Id == userId);

		//			Circle backupCircle = GetCircleByName(user, "MyBros");

		//			foreach (User u in targetCircle.Members)
		//			{
		//				ContextlessMoveBroBetweenCircles(targetCircle, backupCircle, u);
		//			}

		//			context.Circles.Remove(targetCircle);
		//		}

		//		context.SaveChanges();
		//	}

		//	return RedirectToAction("Circles");
		//}

		//public ActionResult AddBroToCircle(int circleId, int targetBroId)
		//{
		//	using (ModelFirstContainer context = new ModelFirstContainer())
		//	{
		//		Circle targetCircle = context.Circles.SingleOrDefault(x => x.Id == circleId);
		//		User targetBro = context.Users.SingleOrDefault(x => x.Id == targetBroId);

		//		if (!targetCircle.Members.Contains(targetBro))
		//		{
		//			targetCircle.Members.Add(targetBro);
		//		}

		//		context.SaveChanges();
		//	}

		//	return RedirectToAction("Circles");
		//}

		//public ActionResult RemoveBroFromCircle(int circleId, int targetBroId)
		//{
		//	using (ModelFirstContainer context = new ModelFirstContainer())
		//	{
		//		Circle targetCircle = context.Circles.SingleOrDefault(x => x.Id == circleId);
		//		User targetBro = context.Users.SingleOrDefault(x => x.Id == targetBroId);

		//		targetCircle.Members.Remove(targetBro);

		//		context.SaveChanges();
		//	}

		//	return RedirectToAction("Circles");
		//}

		[HttpGet]
		public ActionResult Edit()
		{

            bool isFirstTime = Boolean.Parse(Request["firstTime"]?? "false");

            if (isFirstTime) ViewBag.Message = "Please fill out your profile.";
			User thisUser = new User();
			using (var context = new ModelFirstContainer())
			{
				int userId = WebSecurity.CurrentUserId;
				thisUser = context.Users.Include("Profile").FirstOrDefault(u => u.Id == userId);
			}

            ViewBag.SubTitle = "Who are you?";
            ViewBag.Title = "Edit Profile";

			return View(thisUser.Profile);
		}

		[HttpPost]
		public ActionResult Edit(Profile prof)
		{
			int id = WebSecurity.CurrentUserId;
            User user = new User();
			if (ModelState.IsValid)
				using (ModelFirstContainer context = new ModelFirstContainer())
				{
					user = context.Users.Include("Profile").FirstOrDefault(u => u.Id == WebSecurity.CurrentUserId);
					user.Profile.Athleticism = prof.Athleticism;
					user.Profile.Children = prof.Children;
					user.Profile.Drinks = prof.Drinks;
					user.Profile.Drugs = prof.Drugs;
					user.Profile.Education = prof.Education;
					user.Profile.Ethnicity = prof.Ethnicity;
					user.Profile.Job = prof.Job;
					user.Profile.MarriageStatus = prof.MarriageStatus;
					user.Profile.Pets = prof.Pets;
					user.Profile.Religion = prof.Religion;
					user.Profile.SexualOrientation = prof.SexualOrientation;
					user.Profile.Smokes = prof.Smokes;

					context.SaveChanges();
				}

            ViewBag.SubTitle = "Who are you?";
            ViewBag.Message = "Changes have been saved!";

			return View(user.Profile );
		}

		public ActionResult Settings(int id)
		{
			User user = new User();
			using (var context = new ModelFirstContainer())
			{
				user = context.Users.Include("Profile").Include("BlockedBros.Profile").FirstOrDefault(u => u.Id == id);
			}
			return View(user);
		}

	}
}