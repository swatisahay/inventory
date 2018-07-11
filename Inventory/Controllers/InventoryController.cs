using Microsoft.AspNetCore.Mvc;
using Inventory.Models;
using System.Collections.Generic;
using System;

namespace Inventory.Controllers
{
  public class InventoryController : Controller
  {

    // [HttpGet("/items")]
    // public ActionResult Index()
    // {
    //     List<Item> allItems = Item.GetAll();
    //     return View(allItems);
    // }
    //
    // [HttpGet("/items/new")]
    // public ActionResult CreateForm()
    // {
    //     return View();
    // }
    // [HttpPost("/items")]
    // public ActionResult Create()
    // {
    //   Item newItem = new Item (Request.Form["newname"], Request.Form["newptype"], int.Parse(Request.Form["newnumber"]));
    //   // Item newItem = new Item ("Charizard", "Fire", 6);
    //   newItem.Save();
    //   List<Item> allItems = Item.GetAll();
    //   return View("Index", allItems);
    // }
    // [HttpGet("/items/find")]
    // public ActionResult Find()
    // {
    //   return View();
    // }
    // [HttpPost("/items/found")]
    // public ActionResult Found()
    // {
    //   Item newItem = new Item("","",0);
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
    //    [HttpPost("/items/{id}/update")]
    //     public ActionResult Update(int id)
    //     {
    //         Item thisItem = Item.Find(id);
    //         thisItem.Edit(Request.Form["updatename"], Request.Form["updateptype"], int.Parse(Request.Form["updatenumber"]));
    //         return RedirectToAction("Index");
    //     }
    //     [HttpGet("/items/{id}/delete")]
    //    public ActionResult Delete(int id)
    //    {
    //        Item thisItem = Item.Find(id);
    //        thisItem.Delete();
    //        return RedirectToAction("Index");
    //    }



    // [HttpPost("/items/delete")]
    // public ActionResult DeleteAll()
    // {
    //     Item.ClearAll();
    //     return View();
    // }
    // [HttpGet("/items/{id}")]
    // public ActionResult Details(int id)
    // {
    //     Item item = Item.Find(id);
    //     return View(item);
    // }
  }
}
