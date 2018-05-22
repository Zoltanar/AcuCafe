using System;

namespace AcuCafe
{
	/// <summary>
	/// Allows ordering drinks with certain components
	/// </summary>
	public class AcuCafe
	{
		/// <summary>
		/// This contains an instance of a barista who has responsibility over the Cafe(s).
		/// </summary>
		public static Barista Barista { get; } = new Barista();

		public static Drink OrderDrink(string type, bool hasMilk, bool hasSugar, bool hasChocolate)
		{
			Drink drink;
			switch (type)
			{
				case "Espresso":
					drink = new Espresso();
					break;
				case "HotTea":
					drink = new Tea();
					break;
				case "IceTea":
					drink = new IceTea();
					break;
				default:
					drink = new Drink();
					break;
			}
			try
			{
				drink.HasMilk = hasMilk;
				drink.HasSugar = hasSugar;
				drink.HasChocolate = hasChocolate;
				drink.Prepare();
			}
			catch (Exception ex)
			{
				Console.WriteLine("We are unable to prepare your drink.");
				Console.WriteLine(ex.Message);
				System.IO.File.WriteAllText(@"Error.txt", ex.ToString());
			}
			return drink;
		}
	}

	/// <summary>
	/// An entity responsible for maintaining the cafe.
	/// </summary>
	public class Barista
	{
		public bool IsNotified { get; set; }

		public void Notify()
		{
			IsNotified = true;
			HandleIssue();
		}


		private void HandleIssue()
		{
			//todo the barista would do something at this point then remove the IsNotified flag.
		}
	}

	public class Drink
	{
		public const double MilkCost = 0.5;
		public const double SugarCost = 0.5;

		public bool HasMilk { get; set; }
		public bool HasSugar { get; set; }
		public bool HasChocolate { get; set; }
		public string Description => "Drink";
		public bool? IsPrepared { get; set; }

		public double Cost()
		{
			return 0;
		}

		public virtual void Prepare()
		{
			try
			{
				string message = "We are preparing the following drink for you: " + Description;
				if (HasMilk) message += " with milk";
				else message += " without milk";
				if (HasSugar) message += " with sugar";
				else message += " without sugar";
				Console.WriteLine(message);
				IsPrepared = true;
			}
			catch
			{
				IsPrepared = false;
				throw;
			}
		}
	}

	public class Espresso : Drink
	{
		public new string Description => "Espresso";

		public new double Cost()
		{
			double cost = 1.8;
			if (HasMilk) cost += MilkCost;
			if (HasSugar) cost += SugarCost;
			return cost;
		}

		public override void Prepare()
		{
			try
			{
				string message = "We are preparing the following drink for you: " + Description;
				message += HasMilk ? " with milk" : " without milk";
				message += HasSugar ? " with sugar" : " without sugar";
				message += HasChocolate ? " with chocolate" : " without chocolate";
				Console.WriteLine(message);
				IsPrepared = true;
			}
			catch
			{
				IsPrepared = false;
				throw;
			}
		}
	}

	public class Tea : Drink
	{
		public new string Description => "Hot tea";

		public new double Cost()
		{
			double cost = 1;
			if (HasMilk) cost += MilkCost;
			if (HasSugar) cost += SugarCost;

			return cost;
		}

		public override void Prepare()
		{
			try
			{
				string message = "We are preparing the following drink for you: " + Description;
				message += HasMilk ? " with milk" : " without milk";
				message += HasSugar ? " with sugar" : " without sugar";
				if (HasChocolate) throw new ArgumentException("This drink cannot contain chocolate", nameof(HasChocolate));
				Console.WriteLine(message);
				IsPrepared = true;
			}
			catch
			{
				IsPrepared = false;
				throw;
			}
		}
	}

	public class IceTea : Drink
	{
		public new string Description => "Ice tea";

		public new double Cost()
		{
			double cost = 1.5;
			if (HasMilk) cost += MilkCost;
			if (HasSugar) cost += SugarCost;
			return cost;
		}
		
		public override void Prepare()
		{
			try
			{
				string message = "We are preparing the following drink for you: " + Description;
				if (HasMilk)
				{
					AcuCafe.Barista.Notify();
					throw new ArgumentException("This drink cannot contain milk", nameof(HasMilk));
				}
				message += " without milk";
				message += HasSugar ? " with sugar" : " without sugar";
				if (HasChocolate) throw new ArgumentException("This drink cannot contain chocolate", nameof(HasChocolate));
				Console.WriteLine(message);
				IsPrepared = true;
			}
			catch
			{
				IsPrepared = false;
				throw;
			}
		}
	}
}