using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using Inventory.Models;

namespace Inventory.Tests
{
  [TestClass]
  public class DescriptionTests : IDisposable
  {
        public void Dispose()
        {
          Item.DeleteAll();
          Description.DeleteAll();
        }
        public DescriptionTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=inventory_test;";
        }

       [TestMethod]
       public void GetAll_DescriptionEmptyAtFirst_0()
       {
         //Arrange, Act
         int result = Description.GetAll().Count;

         //Assert
         Assert.AreEqual(0, result);
       }

      [TestMethod]
      public void Equals_ReturnsTrueForSameName_description()
      {
        //Arrange, Act
        Description firstDescription = new Description("Household chores");
        Description secondDescription = new Description("Household chores");

        //Assert
        Assert.AreEqual(firstDescription, secondDescription);
      }

      [TestMethod]
      public void Save_SavesCategoryToDatabase_DescriptionList()
      {
        //Arrange
        Description testDescription = new Description("Household chores");
        testDescription.Save();

        //Act
        List<Description> result = Description.GetAll();
        List<Description> testList = new List<Description>{testDescription};

        //Assert
        CollectionAssert.AreEqual(testList, result);
      }


     [TestMethod]
     public void Save_DatabaseAssignsIdToDescription_Id()
     {
       //Arrange
       Description testDescription = new Description("Household chores");
       testDescription.Save();

       //Act
       Description savedDescription = Description.GetAll()[0];

       int result = savedDescription.GetId();
       int testId = testDescription.GetId();

       //Assert
       Assert.AreEqual(testId, result);
    }


    [TestMethod]
    public void Find_FindDescriptionInDatabase_Description()
    {
      //Arrange
      Description testDescription = new Description("Household chores");
      testDescription.Save();

      //Act
      Description foundDescription = Description.Find(testDescription.GetId());

      //Assert
      Assert.AreEqual(testDescription, foundDescription);
    }


  }
}
