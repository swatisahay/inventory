using Microsoft.AspNetCore.Mvc;
using Inventory.Models;
using System.Collections.Generic;
using System;


namespace Inventory.Controllers
{
  public class DescriptionController : Controller
  {

    [HttpGet("/description")]
    public ActionResult Index()
    {
        List<Description> allDescriptions = Description.GetAll();
        return View(allDescriptions);
    }

    [HttpGet("/description/new")]
    public ActionResult CreateDescription()
    {
        return View();
    }
    [HttpPost("/description")]
    public ActionResult Create()
    {
      Description newDescription = new Description (Request.Form["type"]);
      // Item newItem = new Item ("Charizard", "Fire", 6);
      newDescription.Save();
      List<Description> allDescriptions = Description.GetAll();
      return View("Index", allDescriptions);
    }
    // [HttpGet("/items/find")]
    // public ActionResult Find()
    // {
    //   return View();
    // }
    // [HttpPost("/items/found")]
    // public ActionResult Found()
    // {
    //   Item newItem = new Item("","",0, 0);
    //
    //   newItem = Item.Find(int.Parse(Request.Form["newid"]));
    //
    //   return View(newItem);
    // }
    // [HttpGet("/items/{id}/update")]
    //    public ActionResult UpdateForm(int id)
    //    {
    //        Item thisItem = Item.Find(id);
    //        return View(thisItem);
    //    }
       [HttpGet("/description/{id}/items")]
        public ActionResult Descriptions(int id)
        {
            Description thisItem = Description.Find(id);
            List<Item> allDescriptions = thisItem.GetItems();
            return View(thisItem);
        }
    //     [HttpGet("/items/{id}/delete")]
    //    public ActionResult Delete(int id)
    //    {
    //        Item thisItem = Item.Find(id);
    //        thisItem.Delete();
    //        return RedirectToAction("Index");
    //    }
    //
    //
    //
    // // [HttpPost("/items/delete")]
    // // public ActionResult DeleteAll()
    // // {
    // //     Item.ClearAll();
    // //     return View();
    // // }
    // [HttpGet("/items/{id}")]
    // public ActionResult Details(int id)
    // {
    //     Item item = Item.Find(id);
    //     return View(item);
    // }
  }
}
