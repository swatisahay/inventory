using Microsoft.AspNetCore.Mvc;
using Inventory.Models;
using System.Collections.Generic;
using System;

namespace Inventory.Controllers
{
  public class InventoryController : Controller
  {

    [HttpGet("/items")]
    public ActionResult Index()
    {
        List<Item> allItems = Item.GetAll();
        return View(allItems);
    }

    [HttpGet("/items/new")]
    public ActionResult CreateForm()
    {
        return View();
    }
    [HttpPost("/items")]
    public ActionResult Create()
    {
      Item newItem = new Item (Request.Form["newname"], Request.Form["newptype"], int.Parse(Request.Form["newnumber"]));
      // Item newItem = new Item ("Charizard", "Fire", 6);
      newItem.Save();
      List<Item> allItems = Item.GetAll();
      return View("Index", allItems);
    }
    [HttpGet("/items/find")]
    public ActionResult Find()
    {
      return View();
    }
    [HttpPost("/items/found")]
    public ActionResult Found()
    {
      Item newItem = new Item("","",0);

      newItem = Item.Find(int.Parse(Request.Form["newid"]));

      return View(newItem);
    }
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
