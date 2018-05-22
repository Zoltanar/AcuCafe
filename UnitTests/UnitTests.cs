using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AcuCafe;

namespace UnitTests
{
	[TestClass]
	public class UnitTests
	{
		[TestMethod]
		public void MakeIceTeaWithMilkSugar()
		{
			var drink = AcuCafe.AcuCafe.OrderDrink("IceTea", true, true,false);
			Assert.AreEqual(typeof(IceTea), drink.GetType());
			Assert.AreEqual(true, drink.IsPrepared);
			Assert.AreEqual(true, drink.HasMilk);
			Assert.AreEqual(true, drink.HasSugar);
		}

		[TestMethod]
		public void MakeEspressoWithMilkSugar()
		{
			var drink = AcuCafe.AcuCafe.OrderDrink("Espresso", true, true,false);
			Assert.AreEqual(typeof(Espresso), drink.GetType());
			Assert.AreEqual(true, drink.IsPrepared);
			Assert.AreEqual(true, drink.HasMilk);
			Assert.AreEqual(true, drink.HasSugar);
		}
		
		[TestMethod]
		public void MakeEspressoWithChocolate()
		{
			var drink = AcuCafe.AcuCafe.OrderDrink("Espresso", false, false, true);
			Assert.AreEqual(typeof(Espresso), drink.GetType());
			Assert.AreEqual(true, drink.IsPrepared);
			Assert.AreEqual(true, drink.HasChocolate);
		}

		[TestMethod]
		public void StopMilkInIceTea()
		{
			var drink = AcuCafe.AcuCafe.OrderDrink("IceTea", true, false, false);
			Assert.AreEqual(false,drink.IsPrepared);
		}

		[TestMethod]
		public void NotifyBaristaAfterMilkInIceTea()
		{
			var drink = AcuCafe.AcuCafe.OrderDrink("IceTea", true, false, false);
			Assert.AreEqual(false, drink.IsPrepared);
			Assert.AreEqual(true, AcuCafe.AcuCafe.Barista.IsNotified);
		}
	}
}
